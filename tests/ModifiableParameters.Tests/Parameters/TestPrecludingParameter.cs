using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Parameters;

namespace TestsModifiableParameters.Parameters
{
    [TestClass]
    public class TestPrecludingParameter : ASimpleParameterTests<PrecludingParameter, bool>
    {
        [TestMethod]
        public override void CheckBaseValuesAfterInstance()
        {
            CreateAndCheckValue(true);
            CreateAndCheckValue(false);
        }

        [TestMethod]
        public void TestReasonSave()
        {
            var parameter = CreateAndCheckValue(true);
            string reason = "Testing";
            
            PrecludingModifier modifier = new PrecludingModifier(reason);
            Assert.AreEqual(reason,modifier.Reason);
            parameter.AddModifier(modifier);
            Assert.AreNotEqual(parameter.BaseValue, parameter.CurrentValue);

            string firstReason = parameter.GetFirstReason();
            Assert.AreEqual(reason, firstReason);

            parameter.RemoveModifier(modifier);
            Assert.AreEqual(parameter.BaseValue, parameter.CurrentValue);

            string nullReason = parameter.GetFirstReason();
            Assert.IsNull(nullReason);
        }

        [TestMethod]
        public void TestNotPrecludingModifier()
        {
            var parameter = CreateAndCheckValue(true);
            parameter.AddModifier( new ParameterModifier<bool>(true));

            Assert.AreNotEqual(parameter.BaseValue, parameter.CurrentValue);
            string reason = parameter.GetFirstReason();
            Assert.IsNull(reason);
        }

        [TestMethod]
        public void TestSeveralReasones()
        {
            var parameter = CreateAndCheckValue(true);
            string [] reasones = {"First", "Second", null, "Fourth"};

            
            foreach (var reason in reasones)
            {
                parameter.AddModifier(new PrecludingModifier(reason));
            }

            string[] reasonesArray = parameter.GetReasones();

            Assert.AreEqual(reasonesArray.Length, reasones.Length);
            foreach (var reason in reasones)
            {
                bool isExists = reasonesArray.Contains(reason);
                Assert.IsTrue(isExists);
            }
        }

        public override Func<bool> GetRandomValueFunc => () => new Random().NextDouble() >= 0.5;
    }
}
