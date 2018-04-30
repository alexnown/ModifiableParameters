using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Extensions;
using ModifiableParameters.Limiters;
using ModifiableParameters.Parameters;

namespace TestsModifiableParameters.Extensions
{
    [TestClass]
    public class LimitersExtensionsTests
    {
        [TestMethod]
        public void RemoveAllLimitersTest()
        {
            var parameter = new IntParameter(0);
            var limitersArrey = new IParameterLimiter<int>[]
            {
                new BannedIntLimiter(-1,0), new BannedIntLimiter(1,0), new IntMaxValueLimiter(5)
            };
            foreach (var limiter in limitersArrey)
            {
                parameter.AddLimiter(limiter);
            }

            var removedLimitersArray = parameter.RemoveAllLimiters();
            Assert.AreEqual(limitersArrey.Length, removedLimitersArray.Length);
            foreach (var limiter in limitersArrey)
            {
                Assert.IsTrue(removedLimitersArray.Contains(limiter));
            }
        }

        [TestMethod]
        public void AddAllLimitersTest()
        {
            var parameter = new IntParameter(0);
            var limitersArrey = new IParameterLimiter<int>[]
            {
                new BannedIntLimiter(-1,0), new BannedIntLimiter(1,0), new IntMaxValueLimiter(5)
            };
            parameter.AddAllLimiters(limitersArrey);

            Assert.AreEqual(limitersArrey.Length, parameter.LimitersCount);
            foreach (var limiter in limitersArrey)
            {
                Assert.IsTrue(parameter.GetLimiters().Contains(limiter));
            }
        }
    }
}
