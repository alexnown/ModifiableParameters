﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Parameters;

namespace TestsModifiableParameters.Parameters
{
    public class ModifiableParameterInterfaceTests<V>
    {
        public void RunTests(IModifiableParameter<V> parameter, Func<V> getRandomValueFunc)
        {
            bool recalculateOn = parameter.RecalculateOnChangeModifiers;

            ChangeBaseValue_HandleRecalculateEvent(parameter, getRandomValueFunc());
            ChangeModifier_HandleRecalculateEvent(parameter);

            parameter.RecalculateOnChangeModifiers = false;
            AddModifier_CheckEventsOrder(parameter);
            RemoveModifier_CheckEventsOrder(parameter);
            parameter.RecalculateOnChangeModifiers = true;
            AddModifier_CheckEventsOrder(parameter);
            RemoveModifier_CheckEventsOrder(parameter);

            parameter.RecalculateOnChangeModifiers = recalculateOn;
        }

        public void ChangeBaseValue_HandleRecalculateEvent(IModifiableParameter<V> parameter, V newBaseValue)
        {
            bool recalculateHandled = false;
            Action<V> recalculateEventHandler = (newValue) => { recalculateHandled = true; };
            parameter.Recalculated += recalculateEventHandler;

            parameter.BaseValue = newBaseValue;
            Assert.AreEqual(true, recalculateHandled);

            parameter.Recalculated -= recalculateEventHandler;
        }

        public void ChangeModifier_HandleRecalculateEvent(IModifiableParameter<V> parameter)
        {
            var modifier = new ParameterModifier<V>(default(V));
            bool recalculateHandled = false;
            Action<V> recalculateEventHandler = (newValue) => { recalculateHandled = true; };
            parameter.Recalculated += recalculateEventHandler;

            parameter.AddModifier(modifier);
            Assert.AreEqual(true, recalculateHandled);
            recalculateHandled = false;
            parameter.RemoveModifier(modifier);
            Assert.AreEqual(true, recalculateHandled);

            parameter.Recalculated -= recalculateEventHandler;
        }

        public void AddModifier_CheckEventsOrder(IModifiableParameter<V> parameter)
        {
            var modifier = new ParameterModifier<V>(default(V));
            int eventSequence = 0;
            int modifierEventSequence = 0;
            int recalculateEventSequence = 0;

            Action<V> recalculateEventHandler = (newValue) =>
            {
                eventSequence++;
                recalculateEventSequence = eventSequence;
            };
            Action<ParameterModifier<V>> modifierChangeHandler = (mModifier) =>
            {
                eventSequence++;
                modifierEventSequence = eventSequence;
            };

            parameter.Recalculated += recalculateEventHandler;
            parameter.ModifierAdded += modifierChangeHandler;

            parameter.AddModifier(modifier);
            Assert.AreEqual(true, parameter.ContainsModifier(modifier));
            Assert.AreEqual(1, modifierEventSequence);
            int recalculateExpectedSequence = parameter.RecalculateOnChangeModifiers ? 2 : 0;
            Assert.AreEqual(recalculateExpectedSequence, recalculateEventSequence);

            parameter.Recalculated -= recalculateEventHandler;
            parameter.ModifierAdded -= modifierChangeHandler;
            parameter.RemoveModifier(modifier);
        }

        public void RemoveModifier_CheckEventsOrder(IModifiableParameter<V> parameter)
        {
            var modifier = new ParameterModifier<V>(default(V));
            parameter.AddModifier(modifier);
            int eventSequence = 0;
            int modifierEventSequence = 0;
            int recalculateEventSequence = 0;

            Action<V> recalculateEventHandler = (newValue) =>
            {
                eventSequence++;
                recalculateEventSequence = eventSequence;
            };
            Action<ParameterModifier<V>> modifierChangeHandler = (mModifier) =>
            {
                eventSequence++;
                modifierEventSequence = eventSequence;
            };

            parameter.Recalculated += recalculateEventHandler;
            parameter.ModifierRemoved += modifierChangeHandler;

            parameter.RemoveModifier(modifier);
            Assert.AreEqual(false, parameter.ContainsModifier(modifier));
            Assert.AreEqual(1, modifierEventSequence);
            int recalculateExpectedSequence = parameter.RecalculateOnChangeModifiers ? 2 : 0;
            Assert.AreEqual(recalculateExpectedSequence, recalculateEventSequence);


            parameter.Recalculated -= recalculateEventHandler;
            parameter.ModifierRemoved -= modifierChangeHandler;
        }

    }
}
