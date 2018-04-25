using System;
using System.Linq;
using ModifiableParameters.Parameters;

namespace ModifiableParameters.Calculators
{
    /// <summary> Стандартная стратегия подсчета параметра. Модификаторы складываются. </summary>
    public class IntAdditionCalculator : AParameterCalculator<int>
    {
        public override int CalculateCurrentValue(IParameter<int> parameter)
        {
            var modifiable = parameter as IModifiableParameter<int>;
            if (modifiable == null) throw new InvalidOperationException($"Parameter {parameter} must implement {nameof(IModifiableParameter<int>)} interface.");
            return modifiable.BaseValue + modifiable.GetModifiers().Sum(modifier => modifier.Value);
        }
    }
}
