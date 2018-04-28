using System;
using System.Linq;
using ModifiableParameters.Parameters;

namespace ModifiableParameters.Calculators
{
    /// <summary> Returns parameter base value if don't contain any modifiers. If parametr has any modifiers, returns !BaseValue. </summary>
    public class EmptyModifiersRequirement : IParameterCalculator<bool>
    {
        public bool CalculateCurrentValue(IParameter<bool> parameter)
        {
            var modifiable = parameter as IModifiableParameter<bool>;
            if (modifiable == null) throw new InvalidOperationException($"Parameter {parameter} must implement {nameof(IModifiableParameter<bool>)} interface.");
            return modifiable.ModifiersCount == 0 ? modifiable.BaseValue : !modifiable.BaseValue;
        }
    }

    /// <summary> Returns BaseValue if it and all modifiers value the same (or modifiers count is 0). If contains modifiers with another values, returns !BaseValue.</summary>
    public class AndGateCalculator : IParameterCalculator<bool>
    {
        public bool CalculateCurrentValue(IParameter<bool> parameter)
        {
            var modifiable = parameter as IModifiableParameter<bool>;
            if (modifiable == null) throw new InvalidOperationException($"Parameter {parameter} must implement {nameof(IModifiableParameter<bool>)} interface.");
            if (modifiable.ModifiersCount == 0)
            {
                return modifiable.BaseValue;
            }
            bool hasDifferentValue = modifiable.GetModifiers().Any(modifier => modifier.Value != modifiable.BaseValue);
            return hasDifferentValue ? !modifiable.BaseValue : modifiable.BaseValue;
        }
    }

    /// <summary> Returns true if parameter base value and all modifiers value the same. If don't contain any modifiers, returns true.</summary>
    public class SameModifiersValueRequirement : IParameterCalculator<bool>
    {
        public bool CalculateCurrentValue(IParameter<bool> parameter)
        {
            var modifiable = parameter as IModifiableParameter<bool>;
            if (modifiable == null) throw new InvalidOperationException($"Parameter {parameter} must implement {nameof(IModifiableParameter<bool>)} interface.");
            if (modifiable.ModifiersCount == 0)
            {
                return true;
            }
            bool hasDifferentValue = modifiable.GetModifiers().Any(modifier => modifier.Value != modifiable.BaseValue);
            return !hasDifferentValue;
        }
    }

}
