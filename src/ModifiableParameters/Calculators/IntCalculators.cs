using System;
using System.Linq;
using ModifiableParameters.Parameters;

namespace ModifiableParameters.Calculators
{
    /// <summary> Adds the parameter base value and modifiers value. </summary>
    public class IntAdditionCalculator : IParameterCalculator<int>
    {
        public int CalculateCurrentValue(IParameter<int> parameter)
        {
            var modifiable = parameter as IModifiableParameter<int>;
            if (modifiable == null) throw new InvalidOperationException($"Parameter {parameter} must implement {nameof(IModifiableParameter<int>)} interface.");
            return modifiable.BaseValue + modifiable.GetModifiers().Sum(modifier => modifier.Value);
        }
    }
}
