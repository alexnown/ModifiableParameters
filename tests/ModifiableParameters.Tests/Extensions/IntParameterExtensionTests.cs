using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Extensions;
using ModifiableParameters.Limiters;
using ModifiableParameters.Parameters;

namespace TestsModifiableParameters.Extensions
{ 
    [TestClass]
    public class IntParameterExtensionTests
    {
        [TestMethod]
        public void SetMaxTest()
        {
            int firstMaxValue = 5;
            int secondMaxValue = 0;
            var parameter = new IntParameter(10).SetMax(firstMaxValue);
            var maxLimiter = parameter.GetLimiters().OfType<IntMaxValueLimiter>().FirstOrDefault();

            Assert.IsNotNull(maxLimiter);
            Assert.AreEqual(firstMaxValue, maxLimiter.MaxValue);

            parameter.SetMax(secondMaxValue);

            int limitersCoutn = parameter.GetLimiters().OfType<IntMaxValueLimiter>().Count();
            Assert.AreEqual(1, limitersCoutn);
            Assert.AreEqual(secondMaxValue, maxLimiter.MaxValue);
        }

        [TestMethod]
        public void SetMinTest()
        {
            int firstMinValue = 50;
            int secondMinValue = 100;
            var parameter = new IntParameter(0).SetMin(firstMinValue);
            var minLimiter = parameter.GetLimiters().OfType<IntMinValueLimiter>().FirstOrDefault();

            Assert.IsNotNull(minLimiter);
            Assert.AreEqual(firstMinValue, minLimiter.MinValue);

            parameter.SetMin(secondMinValue);

            int limitersCoutn = parameter.GetLimiters().OfType<IntMinValueLimiter>().Count();
            Assert.AreEqual(1, limitersCoutn);
            Assert.AreEqual(secondMinValue, minLimiter.MinValue);
        }

        [TestMethod]
        public void BanValue()
        {
            int bannedValue = 100;
            int correctedValue = 0;
            var parameter = new IntParameter(bannedValue).BanValue(bannedValue, correctedValue);
            var banLimiter = parameter.GetLimiters().OfType<BannedIntLimiter>().FirstOrDefault();

            Assert.IsNotNull(banLimiter);
            Assert.AreEqual(bannedValue, banLimiter.BannedValue);
            Assert.AreEqual(correctedValue, banLimiter.CorrectedValue);
        }

        [TestMethod]
        public void BanModifierValue()
        {
            int bannedValue = 100;
            int correctedValue = 0;
            var parameter = new IntParameter(bannedValue).BanModifierValue(bannedValue, correctedValue);
            var banLimiter = parameter.GetLimiters().OfType<BannedIntModifierLimiter>().FirstOrDefault();

            Assert.IsNotNull(banLimiter);
            Assert.AreEqual(bannedValue, banLimiter.BannedValue);
            Assert.AreEqual(correctedValue, banLimiter.СorrectedValue);
        }
    }
}
