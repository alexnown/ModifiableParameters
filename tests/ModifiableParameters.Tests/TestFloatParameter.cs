using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Parameters;

namespace TestsModifiableParameters
{
    [TestClass]
    public class TestFloatParameter : ASimpleParameterTests<FloatParameter,float>
    {
        [TestMethod]
        public override void TestBaseValueAfterCreate()
        {
            CreateAndCheckValue(float.MinValue);
            CreateAndCheckValue(-500.5f);
            CreateAndCheckValue(0f);
            CreateAndCheckValue(400.44f);
            CreateAndCheckValue(float.MaxValue);
        }
    }
}
