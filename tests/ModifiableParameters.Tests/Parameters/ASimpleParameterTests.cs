using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Parameters;

namespace TestsModifiableParameters.Parameters
{
    [TestClass]
    public abstract class ASimpleParameterTests<P, V> where P : SimpleParameter<V>
    {

        public abstract Func<V> GetRandomValueFunc { get; }
        public abstract void CheckBaseValuesAfterInstance();
        
        [TestMethod]
        public void RunParameterInterfaceTests()
        {
            var parameter = CreateAndCheckValue(GetRandomValueFunc());
            var parameterTests = new ParameterInterfaceTests<V>();
            parameterTests.RunTests(parameter,GetRandomValueFunc());
        }

        [TestMethod]
        public void RunModifiableInterfaceTests()
        {
            var parameter = CreateAndCheckValue(GetRandomValueFunc());
            var modifiableTests = new ModifiableInterfaceTests<V>();
            modifiableTests.RunTests(parameter);
        }

        [TestMethod]
        public void RunModifiableParameterInterfaceTests()
        {
            var parameter = CreateAndCheckValue(GetRandomValueFunc());
            var modifiableParameterTests = new ModifiableParameterInterfaceTests<V>();
            modifiableParameterTests.RunTests(parameter, GetRandomValueFunc);
        }
        
        /*
        [TestMethod]
        public override void RecalculateEventOnBaseValueChanged()
        {
            var parameter = CreateAndCheckValue(default(V));
            bool recalculateHandled = false;
            parameter.ParameterRecalculated += (newValue) => { recalculateHandled = true; };
            parameter.BaseValue = default(V);
            Assert.AreEqual(recalculateHandled, true);
        }


        [TestMethod]
        public override void RecalculateEventOnChangeModifiers()
        {
            var parameter = CreateAndCheckValue(default(V));
            bool recalculateHandled = false;
            parameter.ParameterRecalculated += (newValue) => { recalculateHandled = true; };
            var modifier = new ParameterModifier<V>(default(V));
            //Add modifier
            recalculateHandled = false;
            parameter.AddModifier(modifier);
            Assert.AreEqual(recalculateHandled, true);
            //Remove modifier
            recalculateHandled = false;
            parameter.RemoveModifier(modifier);
            Assert.AreEqual(recalculateHandled, true);
        }



        [TestMethod]
        public void AddModifierEvent()
        {
            var parameter = CreateAndCheckValue(default(V));
            int eventSequence = 0;
            int modifierEventSequence = 0;
            int recalculateEventSequence = 0;
            parameter.ParameterRecalculated += newValue =>
            {
                eventSequence++;
                recalculateEventSequence = eventSequence;
            };
            parameter.ModifierAdded += parameterModifier =>
            {
                eventSequence++;
                modifierEventSequence = eventSequence;
            };
            var modifier = new ParameterModifier<V>(default(V));
            parameter.AddModifier(modifier);
            Assert.AreEqual(modifierEventSequence, 1);
            Assert.AreEqual(recalculateEventSequence, 2);
            Assert.AreEqual(parameter.ContainsModifier(modifier), true);
        }

        [TestMethod]
        public void RemoveModifierEvent()
        {
            var parameter = CreateAndCheckValue(default(V));
            var modifier = new ParameterModifier<V>(default(V));
            parameter.AddModifier(modifier);
            int eventSequence = 0;
            int modifierEventSequence = 0;
            int recalculateEventSequence = 0;
            parameter.ParameterRecalculated += newValue =>
            {
                eventSequence++;
                recalculateEventSequence = eventSequence;
            };
            parameter.ModifierRemoved += parameterModifier =>
            {
                eventSequence++;
                modifierEventSequence = eventSequence;
            };
            parameter.RemoveModifier(modifier);
            Assert.AreEqual(modifierEventSequence, 1);
            Assert.AreEqual(recalculateEventSequence, 2);
            Assert.AreEqual(parameter.ContainsModifier(modifier), false);
        }
        */
        protected P CreateAndCheckValue(V baseValue)
        {
            var parameter = Activator.CreateInstance(typeof(P), baseValue) as SimpleParameter<V>;
            Assert.AreEqual(baseValue, parameter.BaseValue);
            Assert.AreEqual(baseValue, parameter.CurrentValue);
            return (P)parameter;
        }
    }
}
