using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Parameters;

namespace TestsModifiableParameters.Parameters
{
    [TestClass]
    public class TestBoolParameter : ASimpleParameterTests<BoolParameter, bool>
    {
        [TestMethod]
        public override void CheckBaseValuesAfterInstance()
        {
            CreateAndCheckValue(true);
            CreateAndCheckValue(false);
        }

        public override Func<bool> GetRandomValueFunc => () => new Random().NextDouble() >= 0.5;
    }
}
