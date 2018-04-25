using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Parameters;

namespace TestsModifiableParameters
{
    [TestClass]
    public class TestFloatComplexParameter : AParameterTestsList <FloatComplexParameter,float>
    {
        [TestMethod]
        public override void TestBaseValueAfterCreate()
        {
            CreateAndCheckValue(-10.1f, -2.5f);
            CreateAndCheckValue(-23.005f, 0.5f);
            CreateAndCheckValue(-100, 0);
            CreateAndCheckValue(0, 100);
            CreateAndCheckValue(1.1f, 5.2f);
            CreateAndCheckValue(2.1f, -0.2f);
        }
        
        [TestMethod]
        public override void SetNullCalculatorException()
        {
            var parameter = new FloatComplexParameter(10);
            try
            {
                parameter.Calculator = null;
                Assert.Fail("Must throw null exception.");
            }
            catch (ArgumentNullException e) { }
        }

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
