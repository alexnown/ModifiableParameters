using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Parameters;

namespace TestsModifiableParameters.Parameters
{
    [TestClass]
    public class TestsIntParameter : ASimpleParameterTests<IntParameter, int>
    {
        public override Func<int> GetRandomValueFunc => () => new Random().Next(int.MinValue, int.MaxValue);

        [TestMethod]
        public override void CheckBaseValuesAfterInstance()
        {
            CreateAndCheckValue(int.MinValue);
            CreateAndCheckValue(-500);
            CreateAndCheckValue(0);
            CreateAndCheckValue(400);
            CreateAndCheckValue(int.MaxValue);
        }
    }
}
