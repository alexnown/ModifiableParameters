using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Extensions;
using ModifiableParameters.Limiters;
using ModifiableParameters.Parameters;
using Moq;

namespace TestsModifiableParameters.Parameters
{
    public class LimitableInterfaceTests<V>
    {
        public void RunTests(ILimitable<V> parameter)
        {
            ChangeRecalculateFlag(parameter);
            AddNullLimiter_CatchArgumentNullException(parameter);
            RemoveNullLimiter_CatchArgumentNullException(parameter);
            CheckContainsNullLimiter_CatchArgumentNullException(parameter);
            AddAlreadyContainsLimiter_CatchInvalidOparetionException(parameter);
            AddLimiterTests(parameter);
            RemoveLimiterTests(parameter);
            CheckContainsLimiter(parameter);
            CheckLimitersCountUpdate(parameter);
        }

        public void ChangeRecalculateFlag(ILimitable<V> parameter)
        {
            bool recalculateOn = parameter.RecalculateOnChangeLimiters;

            parameter.RecalculateOnChangeLimiters = false;
            Assert.AreEqual(false, parameter.RecalculateOnChangeLimiters);

            parameter.RecalculateOnChangeLimiters = true;
            Assert.AreEqual(true, parameter.RecalculateOnChangeLimiters);

            parameter.RecalculateOnChangeLimiters = recalculateOn;
        }

        public void AddNullLimiter_CatchArgumentNullException(ILimitable<V> parameter)
        {
            try
            {
                parameter.AddLimiter(null);
                Assert.Fail($"{nameof(ArgumentNullException)} not throws on add null limiter.");
            }
            catch (ArgumentNullException) { }
        }

        public void RemoveNullLimiter_CatchArgumentNullException(ILimitable<V> parameter)
        {
            try
            {
                parameter.RemoveLimiter(null);
                Assert.Fail($"{nameof(ArgumentNullException)} not throws on remove null limiter.");
            }
            catch (ArgumentNullException) { }
        }

        public void CheckContainsNullLimiter_CatchArgumentNullException(ILimitable<V> parameter)
        {
            try
            {
                parameter.ContainsLimiter(null);
                Assert.Fail($"{nameof(ArgumentNullException)} not throws when get contains null limiter.");
            }
            catch (ArgumentNullException) { }
        }

        public void AddAlreadyContainsLimiter_CatchInvalidOparetionException(ILimitable<V> parameter)
        {
            var limiter = PrepareLimiterMock().Object;
            parameter.AddLimiter(limiter);
            try
            {
                parameter.AddLimiter(limiter);
                Assert.Fail($"{nameof(InvalidOperationException)} not throws when limiter added second time.");
            }
            catch (InvalidOperationException) { }
            parameter.RemoveLimiter(limiter);
        }

        public void AddLimiterTests(ILimitable<V> parameter)
        {
            bool addedEventHandled = false;
            Action<IParameterLimiter<V>> addEventHandler = parameterModifier => addedEventHandled = true;
            parameter.LimiterAdded += addEventHandler;
            var limiterMock = PrepareLimiterMock();
            var limiter = limiterMock.Object;
            parameter.AddLimiter(limiter);
            Assert.AreEqual(true, addedEventHandled);
            bool isContained = parameter.ContainsLimiter(limiter);
            Assert.AreEqual(true, isContained);
            limiterMock.Verify(m => m.IsMeetLimit(It.IsAny<IParameter<V>>(),ref It.Ref<V>.IsAny),Times.Once);

            parameter.RemoveLimiter(limiter);
            parameter.LimiterRemoved -= addEventHandler;
        }

        public void CheckContainsLimiter(ILimitable<V> parameter)
        {
            var limiter = PrepareLimiterMock().Object;
            AssertLimiterContainedInParameter(false, parameter, limiter);
            parameter.AddLimiter(limiter);
            AssertLimiterContainedInParameter(true, parameter, limiter);
            parameter.RemoveLimiter(limiter);
            AssertLimiterContainedInParameter(false, parameter, limiter);
        }

        public void AssertLimiterContainedInParameter(bool expected, ILimitable<V> parameter, IParameterLimiter<V> limiter)
        {
            bool fromContainedMethod = parameter.ContainsLimiter(limiter);
            Assert.AreEqual(expected, fromContainedMethod);
            bool foundedInList = parameter.GetLimiters().Contains(limiter);
            Assert.AreEqual(expected, foundedInList);
        }

        public void RemoveLimiterTests(ILimitable<V> parameter)
        {
            var limiter = PrepareLimiterMock().Object;
            parameter.AddLimiter(limiter);

            bool removedEventHandled = false;
            Action<IParameterLimiter<V>> removeEventHandler = parameterModifier => removedEventHandled = true;
            parameter.LimiterRemoved += removeEventHandler;

            bool isContained = parameter.ContainsLimiter(limiter);
            Assert.AreEqual(true, isContained);

            parameter.RemoveLimiter(limiter);

            isContained = parameter.ContainsLimiter(limiter);
            Assert.AreEqual(false, isContained);
            Assert.AreEqual(true, removedEventHandled);

            parameter.LimiterRemoved -= removeEventHandler;
        }

        public void CheckLimitersCountUpdate(ILimitable<V> parameter)
        {
            var removedModifiers = parameter.RemoveAllLimiters();
            var limiter = PrepareLimiterMock().Object;

            Assert.AreEqual(0, parameter.LimitersCount);
            parameter.AddLimiter(limiter);
            Assert.AreEqual(1, parameter.LimitersCount);
            parameter.RemoveLimiter(limiter);
            Assert.AreEqual(0, parameter.LimitersCount);

            if (removedModifiers != null)
                parameter.AddAllLimiters(removedModifiers);
        }

        private Mock<IParameterLimiter<V>> PrepareLimiterMock()
        {
            var mock = new Mock<IParameterLimiter<V>>();
            mock.Setup(m => m.IsMeetLimit(It.IsAny<IParameter<V>>(), ref It.Ref<V>.IsAny)).Returns(true);
            return mock;
        }
    }
}
