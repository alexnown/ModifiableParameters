using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Limiters;
using ModifiableParameters.Parameters;
using Moq;

namespace TestsModifiableParameters.Parameters
{
    [TestClass]
    public class LimitableParameterInterfaceTests<V>
    {
        public void RunTests(ILimitableParameter<V> parameter, Func<V> getRandomValueFunc)
        {
            bool recalculateOn = parameter.RecalculateOnChangeLimiters;

            parameter.RecalculateOnChangeLimiters = true;
            ChangeLimiter_HandleRecalculateEvent(parameter);
            AddLimiter_CheckEventsOrder(parameter);
            RemoveLimiter_CheckEventsOrder(parameter);

            parameter.RecalculateOnChangeLimiters = false;
            ChangeLimiter_HandleRecalculateEvent(parameter);
            AddLimiter_CheckEventsOrder(parameter);
            RemoveLimiter_CheckEventsOrder(parameter);

            parameter.RecalculateOnChangeLimiters = recalculateOn;
        }

        public void ChangeLimiter_HandleRecalculateEvent(ILimitableParameter<V> parameter)
        {
            bool expectRecalculate = parameter.RecalculateOnChangeLimiters;
            var limiterMock = PrepareLimiterMock();
            var limiter = limiterMock.Object;
            bool recalculateHandled = false;
            Action<V> recalculateEventHandler = (newValue) => { recalculateHandled = true; };
            parameter.ParameterRecalculated += recalculateEventHandler;

            parameter.AddLimiter(limiter);
            Assert.AreEqual(expectRecalculate, recalculateHandled);
            recalculateHandled = false;
            parameter.RemoveLimiter(limiter);
            Assert.AreEqual(expectRecalculate, recalculateHandled);
            if (expectRecalculate)
            {
                limiterMock.Verify(m => m.IsMeetLimit(It.IsAny<IParameter<V>>(), ref It.Ref<V>.IsAny), Times.Once);
            }

            parameter.ParameterRecalculated -= recalculateEventHandler;
        }

        public void AddLimiter_CheckEventsOrder(ILimitableParameter<V> parameter)
        {
            var limiter = PrepareLimiterMock().Object;
            int eventSequence = 0;
            int addEventSequence = 0;
            int recalculateEventSequence = 0;

            Action<V> recalculateEventHandler = (newValue) =>
            {
                eventSequence++;
                recalculateEventSequence = eventSequence;
            };
            Action<IParameterLimiter<V>> limiterChangeHandler = (mLimiter) =>
            {
                eventSequence++;
                addEventSequence = eventSequence;
            };

            parameter.ParameterRecalculated += recalculateEventHandler;
            parameter.LimiterAdded += limiterChangeHandler;

            parameter.AddLimiter(limiter);
            Assert.IsTrue(parameter.ContainsLimiter(limiter));
            Assert.AreEqual(1, addEventSequence);
            int recalculateExpectedSequence = parameter.RecalculateOnChangeLimiters ? 2 : 0;
            Assert.AreEqual(recalculateExpectedSequence, recalculateEventSequence);

            parameter.ParameterRecalculated -= recalculateEventHandler;
            parameter.LimiterAdded -= limiterChangeHandler;
            parameter.RemoveLimiter(limiter);
        }

        public void RemoveLimiter_CheckEventsOrder(ILimitableParameter<V> parameter)
        {
            var limiter = PrepareLimiterMock().Object;
            parameter.AddLimiter(limiter);

            int eventSequence = 0;
            int removeEventSequence = 0;
            int recalculateEventSequence = 0;

            Action<V> recalculateEventHandler = (newValue) =>
            {
                eventSequence++;
                recalculateEventSequence = eventSequence;
            };
            Action<IParameterLimiter<V>> limiterChangeHandler = (mLimiter) =>
            {
                eventSequence++;
                removeEventSequence = eventSequence;
            };

            parameter.ParameterRecalculated += recalculateEventHandler;
            parameter.LimiterRemoved += limiterChangeHandler;

            parameter.RemoveLimiter(limiter);
            Assert.IsFalse(parameter.ContainsLimiter(limiter));
            Assert.AreEqual(1, removeEventSequence);
            int recalculateExpectedSequence = parameter.RecalculateOnChangeLimiters ? 2 : 0;
            Assert.AreEqual(recalculateExpectedSequence, recalculateEventSequence);

            parameter.ParameterRecalculated -= recalculateEventHandler;
            parameter.LimiterRemoved -= limiterChangeHandler;
        }

        private Mock<IParameterLimiter<V>> PrepareLimiterMock()
        {
            var mock = new Mock<IParameterLimiter<V>>();
            mock.Setup(m => m.IsMeetLimit(It.IsAny<IParameter<V>>(), ref It.Ref<V>.IsAny)).Returns(true);
            return mock;
        }
    }
}
