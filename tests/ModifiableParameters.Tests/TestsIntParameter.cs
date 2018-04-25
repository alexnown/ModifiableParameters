using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Parameters;

namespace TestsModifiableParameters
{
    [TestClass]
    public class TestsIntParameter : ASimpleParameterTests<IntParameter, int>
    {
        [TestMethod]
        public override void TestBaseValueAfterCreate()
        {
            CreateAndCheckValue(int.MinValue);
            CreateAndCheckValue(-500);
            CreateAndCheckValue(0);
            CreateAndCheckValue(400);
            CreateAndCheckValue(int.MaxValue);
        }
    }
}
