using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Limiters;
using ModifiableParameters.Parameters;

namespace TestsModifiableParameters.Limiters
{
    [TestClass]
    public class FloatLimiterTests
    {
        [TestMethod]
        public void MaxValueLimiterTest()
        {
            var parameter = new FloatParameter(10);
            int maxValue = 5;
            parameter.AddLimiter(new FloatMaxValueLimiter(maxValue));
            Assert.AreEqual(maxValue,parameter.CurrentValue);
        }

        [TestMethod]
        public void MinValueLimiterTest()
        {
            var parameter = new FloatParameter(10);
            int minValue = 20;
            parameter.AddLimiter(new FloatMinValueLimiter(minValue));
            Assert.AreEqual(minValue, parameter.CurrentValue);
        }

        [TestMethod]
        public void BannedModifierLimiterTest()
        {
            var parameter = new FloatParameter(10);
            float bannedModifierValue = 0;
            float limiterCorrectValue = 0;
            var limiter = new BannedFloatModifierLimiter(bannedModifierValue, limiterCorrectValue);
            var modifier = new FloatModifier(bannedModifierValue);
            parameter.AddModifier(modifier);
            
            Assert.AreEqual(parameter.BaseValue + modifier.Value, parameter.CurrentValue,0.0001);
            parameter.AddLimiter(limiter);
            Assert.AreEqual(limiterCorrectValue, parameter.CurrentValue);
        }

        [TestMethod]
        public void BannedLimiterTest()
        {
            float bannedValue = 10;
            float limiterCorrectValue = 100;
            var parameter = new FloatParameter(bannedValue);
            
            var limiter = new BannedFloatLimiter(bannedValue, limiterCorrectValue);
           
            Assert.AreEqual(parameter.BaseValue, parameter.CurrentValue);
            parameter.AddLimiter(limiter);
            Assert.AreEqual(limiterCorrectValue, parameter.CurrentValue);
        }
    }
}
