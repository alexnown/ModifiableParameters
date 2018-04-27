using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Parameters;

namespace TestsModifiableParameters.Parameters
{
    [TestClass]
    public class TestFloatParameter : ASimpleParameterTests<FloatParameter, float>
    {
        public override Func<float> GetRandomValueFunc => () => (float)new Random().NextDouble();

        [TestMethod]
        public override void CheckBaseValuesAfterInstance()
        {
            CreateAndCheckValue(float.MinValue);
            CreateAndCheckValue(-500.5f);
            CreateAndCheckValue(0f);
            CreateAndCheckValue(400.44f);
            CreateAndCheckValue(float.MaxValue);
        }
    }
}
