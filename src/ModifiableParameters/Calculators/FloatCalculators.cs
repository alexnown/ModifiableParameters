using System;
using System.Linq;
using ModifiableParameters.Parameters;

namespace ModifiableParameters.Calculators
{
    /// <summary> Стандартная стратегия подсчета параметра. Модификаторы складываются. </summary>
    public class FloatAdditionCalculator : AParameterCalculator<float>
    { 
        public override float CalculateCurrentValue(IParameter<float> parameter)
        {
            var modifiable = parameter as IModifiableParameter<float>;
            if (modifiable == null) throw new InvalidOperationException($"Parameter {parameter} must implement {nameof(IModifiableParameter<float>)} interface.");
            return modifiable.BaseValue + modifiable.GetModifiers().Sum(modifier => modifier.Value);
        }
    }
}
