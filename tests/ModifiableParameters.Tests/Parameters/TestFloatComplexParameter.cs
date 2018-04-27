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

        /*
        [TestMethod]
        public void RecalculateEventOnBaseValueChanged()
        {
            var parameter = new FloatComplexParameter(10, 2);
            bool recalculateHandled = false;
            parameter.ParameterRecalculated += (newValue) => { recalculateHandled = true; };
            parameter.NumericPart.BaseValue = 20;
            Assert.AreEqual(recalculateHandled, true);
            recalculateHandled = false;
            parameter.MultiplierPart.BaseValue = 1;
            Assert.AreEqual(recalculateHandled, true);
        }

        [TestMethod]
        public override void RecalculateEventOnChangeModifiers()
        {
            var parameter = new FloatComplexParameter(10, 2);
            bool recalculateHandled = false;
            parameter.ParameterRecalculated += (newValue) => { recalculateHandled = true; };
            var modifier = new FloatModifier(10);
            //Add numeric modifier
            recalculateHandled = false;
            parameter.NumericPart.AddModifier(modifier);
            Assert.AreEqual(recalculateHandled, true);
            //Add multiplier modifier
            recalculateHandled = false;
            parameter.MultiplierPart.AddModifier(modifier);
            Assert.AreEqual(recalculateHandled, true);
            //Remove numeric modifier
            recalculateHandled = false;
            parameter.NumericPart.RemoveModifier(modifier);
            Assert.AreEqual(recalculateHandled, true);
            //Remove multiplier modifier
            recalculateHandled = false;
            parameter.MultiplierPart.RemoveModifier(modifier);
            Assert.AreEqual(recalculateHandled, true);
        } */

        private FloatComplexParameter CreateAndCheckValue(float numeric, float multipliar)
        {
            var parameter = new FloatComplexParameter(numeric,multipliar);
            Assert.AreEqual(numeric,parameter.NumericPart.BaseValue);
            Assert.AreEqual(numeric,parameter.NumericPart.CurrentValue);
            Assert.AreEqual(multipliar, parameter.MultiplierPart.BaseValue);
            Assert.AreEqual(multipliar, parameter.MultiplierPart.CurrentValue);
            Assert.AreEqual(numeric * multipliar, parameter.CurrentValue);
            return parameter;
        }
    } 
}
