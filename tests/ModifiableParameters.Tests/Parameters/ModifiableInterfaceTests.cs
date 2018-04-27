using System;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Extensions;
using ModifiableParameters.Parameters;

namespace TestsModifiableParameters.Parameters
{
    public class ModifiableInterfaceTests<V>
    {
        public void RunTests(IModifiable<V> parameter)
        {
            ChangeRecalculateFlag(parameter);
            AddNullModifier_CatchArgumentNullException(parameter);
            RemoveNullModifier_CatchArgumentNullException(parameter);
            CheckContainsNullModifier_CatchArgumentNullException(parameter);
            AddAlreadyContainsModifier_CatchInvalidOparetionException(parameter);
            AddModifierTests(parameter);
            CheckContainsModifier(parameter);
            RemoveModifierTests(parameter);
            CheckModifiersCountUpdate(parameter);
        }
        
        public void ChangeRecalculateFlag(IModifiable<V> parameter)
        {
            bool recalculateOn = parameter.RecalculateOnChangeModifiers;

            parameter.RecalculateOnChangeModifiers = false;
            Assert.AreEqual(false, parameter.RecalculateOnChangeModifiers);

            parameter.RecalculateOnChangeModifiers = true;
            Assert.AreEqual(true, parameter.RecalculateOnChangeModifiers);

            parameter.RecalculateOnChangeModifiers = recalculateOn;
        }

        public void AddNullModifier_CatchArgumentNullException(IModifiable<V> parameter)
        {
            try
            {
                parameter.AddModifier(null);
                Assert.Fail($"{nameof(ArgumentNullException)} not throws on add null modifier.");
            } catch(ArgumentNullException) { }
        }

        public void RemoveNullModifier_CatchArgumentNullException(IModifiable<V> parameter)
        {
            try
            {
                parameter.RemoveModifier(null);
                Assert.Fail($"{nameof(ArgumentNullException)} not throws on remove null modifier.");
            } catch(ArgumentNullException) { }
        }

        public void CheckContainsNullModifier_CatchArgumentNullException(IModifiable<V> parameter)
        {
            try
            {
                parameter.ContainsModifier(null);
                Assert.Fail($"{nameof(ArgumentNullException)} not throws when get contains null modifier.");
            }
            catch (ArgumentNullException) { }
        }

        public void AddAlreadyContainsModifier_CatchInvalidOparetionException(IModifiable<V> parameter)
        {
            var modifier = new ParameterModifier<V>(default(V));
            parameter.AddModifier(modifier);
            try
            {
                parameter.AddModifier(modifier);
                Assert.Fail($"{nameof(InvalidOperationException)} not throws when modifier added second time.");
            }
            catch (InvalidOperationException) { }
        }

        public void AddModifierTests(IModifiable<V> parameter)
        {
            bool addedEventHandled = false;
            Action<ParameterModifier<V>> addEventHandler = parameterModifier => addedEventHandled = true;
            parameter.ModifierAdded += addEventHandler;
            var modifier = new ParameterModifier<V>(default(V));

            parameter.AddModifier(modifier);
            Assert.AreEqual(true, addedEventHandled);
            bool isContained = parameter.ContainsModifier(modifier);
            Assert.AreEqual(true, isContained);

            parameter.RemoveModifier(modifier);
            parameter.ModifierAdded -= addEventHandler;
        }

        public void CheckContainsModifier(IModifiable<V> parameter)
        {
            var modifier = new ParameterModifier<V>(default(V));
            AssertModifierContainedInParameter(false,parameter, modifier);
            parameter.AddModifier(modifier);
            AssertModifierContainedInParameter(true, parameter, modifier);
            parameter.RemoveModifier(modifier);
            AssertModifierContainedInParameter(false, parameter, modifier);
        }

        public void RemoveModifierTests(IModifiable<V> parameter)
        {
            var modifier = new ParameterModifier<V>(default(V));
            parameter.AddModifier(modifier);

            bool removedEventHandled = false;
            Action<ParameterModifier<V>> removeEventHandler = parameterModifier => removedEventHandled = true;
            parameter.ModifierRemoved += removeEventHandler;

            bool isContained = parameter.ContainsModifier(modifier);
            Assert.AreEqual(true,isContained);

            parameter.RemoveModifier(modifier);

            isContained = parameter.ContainsModifier(modifier);
            Assert.AreEqual(false, isContained);
            Assert.AreEqual(true, removedEventHandled);

            parameter.ModifierRemoved -= removeEventHandler;
        }

        public void CheckModifiersCountUpdate(IModifiable<V> parameter)
        {
            var removedModifiers = parameter.RemoveAllModifiers();
            var modifier = new ParameterModifier<V>(default(V));

            Assert.AreEqual(0, parameter.ModifiersCount);
            parameter.AddModifier(modifier);
            Assert.AreEqual(1, parameter.ModifiersCount);
            parameter.RemoveModifier(modifier);
            Assert.AreEqual(0, parameter.ModifiersCount);

            if (removedModifiers != null)
                parameter.AddAllModifiers(removedModifiers);
        }

        public void AssertModifierContainedInParameter(bool expected, IModifiable<V> parameter, ParameterModifier<V> modifier)
        {
            bool fromContainedMethod = parameter.ContainsModifier(modifier);
            Assert.AreEqual(expected, fromContainedMethod);
            bool foundedInList = parameter.GetModifiers().Contains(modifier);
            Assert.AreEqual(expected, foundedInList);
        }

    }
}
