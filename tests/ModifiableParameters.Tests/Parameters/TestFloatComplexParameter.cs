using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Parameters;

namespace TestsModifiableParameters.Parameters
{

    [TestClass]
    public class TestFloatComplexParameter
    {
        public Func<float> GetRandomValueFunc => () => (float)new Random().NextDouble();

        [TestMethod]
        public void CheckBaseValuesAfterInstance()
        {
            CreateAndCheckValue(-10.1f, -2.5f);
            CreateAndCheckValue(-23.005f, 0.5f);
            CreateAndCheckValue(-100, 0);
            CreateAndCheckValue(0, 100);
            CreateAndCheckValue(1.1f, 5.2f);
            CreateAndCheckValue(2.1f, -0.2f);
            CreateAndCheckValue(GetRandomValueFunc(), GetRandomValueFunc());
        }

        [TestMethod]
        public void RunLimitableInterfaceTests()
        {
            var parameter = new FloatComplexParameter(10, 2);
            var limitableTests = new LimitableInterfaceTests<float>();
            limitableTests.RunTests(parameter);
        }

        [TestMethod]
        public void ChangeBaseValue_HandleRecalculateEvent()
        {
            var parameter = new FloatComplexParameter(10, 2);
            bool recalculateHandled = false;
            Action<float> recalculateEventHandler = (newValue) => { recalculateHandled = true; };
            parameter.ParameterRecalculated += recalculateEventHandler;

            parameter.NumericPart.BaseValue = 20;
            Assert.AreEqual(true, recalculateHandled);

            recalculateHandled = false;
            parameter.MultiplierPart.BaseValue = 1;
            Assert.AreEqual(true, recalculateHandled);

            parameter.ParameterRecalculated -= recalculateEventHandler;
        }

        [TestMethod]
        public void ChangeModifier_HandleRecalculateEvent()
        {
            var parameter = new FloatComplexParameter(10, 2);
            var modifier = new FloatModifier(10);
            bool recalculateHandled = false;
            Action<float> recalculateEventHandler = (newValue) => { recalculateHandled = true; };
            parameter.ParameterRecalculated += recalculateEventHandler;

            recalculateHandled = false;
            parameter.NumericPart.AddModifier(modifier);
            Assert.AreEqual(true, recalculateHandled);

            recalculateHandled = false;
            parameter.MultiplierPart.AddModifier(modifier);
            Assert.AreEqual(true, recalculateHandled);

            recalculateHandled = false;
            parameter.NumericPart.RemoveModifier(modifier);
            Assert.AreEqual(true, recalculateHandled);

            recalculateHandled = false;
            parameter.MultiplierPart.RemoveModifier(modifier);
            Assert.AreEqual(true, recalculateHandled);
        }

        private FloatComplexParameter CreateAndCheckValue(float numeric, float multipliar)
        {
            var parameter = new FloatComplexParameter(numeric, multipliar);
            Assert.AreEqual(numeric, parameter.NumericPart.BaseValue);
            Assert.AreEqual(numeric, parameter.NumericPart.CurrentValue);
            Assert.AreEqual(multipliar, parameter.MultiplierPart.BaseValue);
            Assert.AreEqual(multipliar, parameter.MultiplierPart.CurrentValue);
            Assert.AreEqual(numeric * multipliar, parameter.CurrentValue);
            return parameter;
        }
    }
}
