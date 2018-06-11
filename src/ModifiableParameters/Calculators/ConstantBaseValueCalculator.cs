using System;
using ModifiableParameters.Parameters;

namespace ModifiableParameters.Calculators
{
    /// <summary> Sets BaseValue as CurrentValue of parameter regardless of parameter properties. </summary>
    public class ConstantBaseValueCalculator <V> : IParameterCalculator<V>
    {
       public V CalculateCurrentValue(IParameter<V> parameter)
       {
            var modifiable = parameter as IHasBaseValue<V>;
            if (modifiable == null) throw new InvalidOperationException($"Parameter {parameter} must implement {nameof(IHasBaseValue<float>)} interface.");
            return modifiable.BaseValue;
        }
    }
}
