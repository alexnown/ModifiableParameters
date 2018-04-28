using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Calculators;
using ModifiableParameters.Parameters;

namespace TestsModifiableParameters.Calculators
{
    [TestClass]
    public class BoolCalculatorsTests
    {
        [TestMethod]
        public void EmptyModifiersRequirementCalculator()
        {
            var calculator = new EmptyModifiersRequirement();
            var modifier = new BoolModifier(true);

            var trueParameter = new BoolParameter(true, calculator);
            Assert.AreEqual(true, trueParameter.CurrentValue);
            trueParameter.AddModifier(modifier);
            Assert.AreEqual(false, trueParameter.CurrentValue);
            trueParameter.RemoveModifier(modifier);
            Assert.AreEqual(true, trueParameter.CurrentValue);

            var falseParameter = new BoolParameter(false, calculator);
            Assert.AreEqual(false, falseParameter.CurrentValue);
            falseParameter.AddModifier(modifier);
            Assert.AreEqual(true, falseParameter.CurrentValue);
            falseParameter.RemoveModifier(modifier);
            Assert.AreEqual(false, falseParameter.CurrentValue);
        }

        [TestMethod]
        public void SameModifiersValueRequirementCalculator()
        {
            var calculator = new SameModifiersValueRequirement();
            var modifier = new BoolModifier(true);

            var parameter = new BoolParameter(true, calculator);
            parameter.AddModifier(modifier);

            modifier.Value = true;
            Assert.AreEqual(true, parameter.CurrentValue);
            modifier.Value = false;
            Assert.AreEqual(false, parameter.CurrentValue);

            parameter.BaseValue = false;

            modifier.Value = true;
            Assert.AreEqual(false, parameter.CurrentValue);
            modifier.Value = false;
            Assert.AreEqual(true, parameter.CurrentValue);
        }

        [TestMethod]
        public void AndGateCalculator()
        {
            var calculator = new AndGateCalculator();
            var modifier = new BoolModifier(true);

            var parameter = new BoolParameter(true, calculator);
            parameter.AddModifier(modifier);

            modifier.Value = true;
            Assert.AreEqual(true, parameter.CurrentValue);
            modifier.Value = false;
            Assert.AreEqual(false, parameter.CurrentValue);

            parameter.BaseValue = false;

            modifier.Value = true;
            Assert.AreEqual(true, parameter.CurrentValue);
            modifier.Value = false;
            Assert.AreEqual(false, parameter.CurrentValue);
        }
        
    }
}
