using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Extensions;
using ModifiableParameters.Limiters;
using ModifiableParameters.Parameters;

namespace TestsModifiableParameters.Extensions
{
    [TestClass]
    public class FloatParameterExtensionTests
    {
        [TestMethod]
        public void SetMaxTest()
        {
            float firstMaxValue = 5;
            float secondMaxValue = 0;
            var parameter = new FloatParameter(10).SetMax(firstMaxValue);
            var maxLimiter = parameter.GetLimiters().OfType<FloatMaxValueLimiter>().FirstOrDefault();

            Assert.IsNotNull(maxLimiter);
            Assert.AreEqual(firstMaxValue, maxLimiter.MaxValue);

            parameter.SetMax(secondMaxValue);

            int limitersCoutn = parameter.GetLimiters().OfType<FloatMaxValueLimiter>().Count();
            Assert.AreEqual(1, limitersCoutn);
            Assert.AreEqual(secondMaxValue, maxLimiter.MaxValue);
        }

        [TestMethod]
        public void SetMinTest()
        {
            float firstMinValue = 50;
            float secondMinValue = 100;
            var parameter = new FloatParameter(0).SetMin(firstMinValue);
            var maxLimiter = parameter.GetLimiters().OfType<FloatMinValueLimiter>().FirstOrDefault();

            Assert.IsNotNull(maxLimiter);
            Assert.AreEqual(firstMinValue, maxLimiter.MinValue);

            parameter.SetMin(secondMinValue);

            int limitersCoutn = parameter.GetLimiters().OfType<FloatMinValueLimiter>().Count();
            Assert.AreEqual(1, limitersCoutn);
            Assert.AreEqual(secondMinValue, maxLimiter.MinValue);
        }

        [TestMethod]
        public void BanValue()
        {
            float bannedValue = 100;
            float correctedValue = 0;
            float toleranceOffset = 5;
            var parameter = new FloatParameter(bannedValue).BanValue(bannedValue, correctedValue, toleranceOffset);
            var banLimiter = parameter.GetLimiters().OfType<BannedFloatLimiter>().FirstOrDefault();

            Assert.IsNotNull(banLimiter);
            Assert.AreEqual(bannedValue, banLimiter.BannedValue);
            Assert.AreEqual(correctedValue, banLimiter.СorrectedValue);
            Assert.AreEqual(toleranceOffset, banLimiter.ToleranceOffset);
        }

        [TestMethod]
        public void BanModifierValue()
        {
            float bannedValue = 100;
            float correctedValue = 0;
            float toleranceOffset = 5;
            var parameter = new FloatParameter(bannedValue).BanModifierValue(bannedValue, correctedValue, toleranceOffset);
            var banLimiter = parameter.GetLimiters().OfType<BannedFloatModifierLimiter>().FirstOrDefault();

            Assert.IsNotNull(banLimiter);
            Assert.AreEqual(bannedValue, banLimiter.BannedValue);
            Assert.AreEqual(correctedValue, banLimiter.СorrectedValue);
            Assert.AreEqual(toleranceOffset, banLimiter.ToleranceOffset);
        }
    }
}
