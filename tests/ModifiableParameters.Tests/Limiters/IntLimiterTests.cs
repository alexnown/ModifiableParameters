using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Limiters;
using ModifiableParameters.Parameters;

namespace TestsModifiableParameters.Limiters
{
    [TestClass]
    public class IntLimiterTests
    {
        [TestMethod]
        public void MaxValueLimiterTest()
        {
            var parameter = new IntParameter(10);
            int maxValue = 5;
            parameter.AddLimiter(new IntMaxValueLimiter(maxValue));
            Assert.AreEqual(maxValue, parameter.CurrentValue);
        }

        [TestMethod]
        public void MinValueLimiterTest()
        {
            var parameter = new IntParameter(10);
            int minValue = 20;
            parameter.AddLimiter(new IntMinValueLimiter(minValue));
            Assert.AreEqual(minValue, parameter.CurrentValue);
        }

        [TestMethod]
        public void BannedModifierLimiterTest()
        {
            var parameter = new IntParameter(10);
            int bannedModifierValue = 0;
            int limiterCorrectValue = 0;
            var limiter = new BannedIntModifierLimiter(bannedModifierValue, limiterCorrectValue);
            var modifier = new IntModifier(bannedModifierValue);
            parameter.AddModifier(modifier);

            Assert.AreEqual(parameter.BaseValue + modifier.Value, parameter.CurrentValue);
            parameter.AddLimiter(limiter);
            Assert.AreEqual(limiterCorrectValue, parameter.CurrentValue);
        }

        [TestMethod]
        public void BannedLimiterTest()
        {
            int bannedValue = 10;
            int limiterCorrectValue = 100;
            var parameter = new IntParameter(bannedValue);

            var limiter = new BannedIntLimiter(bannedValue, limiterCorrectValue);

            Assert.AreEqual(parameter.BaseValue, parameter.CurrentValue);
            parameter.AddLimiter(limiter);
            Assert.AreEqual(limiterCorrectValue, parameter.CurrentValue);
        }
    }
}
