using System;
using System.Linq;
using ModifiableParameters.Parameters;

namespace ModifiableParameters.Calculators
{
    /// <summary> Если список модификаторов пуст, возвращает BaseValue, иначе !BaseValue. </summary>
    public class EmptyModifiersRequarement : AParameterCalculator<bool>
    {
        public override bool CalculateCurrentValue(IParameter<bool> parameter)
        {
            var modifiable = parameter as IModifiableParameter<bool>;
            if (modifiable == null) throw new InvalidOperationException($"Parameter {parameter} must implement {nameof(IModifiableParameter<bool>)} interface.");
            return modifiable.ModifiersCount == 0 ? modifiable.BaseValue : !modifiable.BaseValue;
        }
    }

    /// <summary> Если среди модификаторов есть хоть один, со значением не совпадающим с BaseValue, то вернется !BaseValue. </summary>
    public class AndGateRequarement : AParameterCalculator<bool>
    {
        public override bool CalculateCurrentValue(IParameter<bool> parameter)
        {
            var modifiable = parameter as IModifiableParameter<bool>;
            if(modifiable == null) throw new InvalidOperationException($"Parameter {parameter} must implement {nameof(IModifiableParameter<bool>)} interface.");
            if (modifiable.ModifiersCount == 0)
            {
                return modifiable.BaseValue;
            }
            if (modifiable.GetModifiers().Any(modifier => modifier.Value != modifiable.BaseValue))
            {
                return !modifiable.BaseValue;
            }
            return modifiable.BaseValue;
        }
    }
    
}
