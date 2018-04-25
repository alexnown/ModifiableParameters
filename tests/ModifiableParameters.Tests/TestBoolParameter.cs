using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Parameters;

namespace TestsModifiableParameters
{
    [TestClass]
    public class TestBoolParameter : ASimpleParameterTests<BoolParameter, bool>
    {
        [TestMethod]
        public override void TestBaseValueAfterCreate()
        {
            CreateAndCheckValue(true);
            CreateAndCheckValue(false);
        }
    }
}
