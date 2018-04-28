using System;
using System.Linq;
using ModifiableParameters.Parameters;

namespace ModifiableParameters.Calculators
{
    /// <summary> Adds the parameter base value and modifiers value. </summary>
    public class FloatAdditionCalculator : IParameterCalculator<float>
    {
        public float CalculateCurrentValue(IParameter<float> parameter)
        {
            var modifiable = parameter as IModifiableParameter<float>;
            if (modifiable == null) throw new InvalidOperationException($"Parameter {parameter} must implement {nameof(IModifiableParameter<float>)} interface.");
            return modifiable.BaseValue + modifiable.GetModifiers().Sum(modifier => modifier.Value);
        }
    }
}
