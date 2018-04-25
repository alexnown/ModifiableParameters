using System;
using ModifiableParameters.Calculators;

namespace ModifiableParameters.Parameters
{
    public class FloatComplexParameter : ALimitableParameter<float>
    {
        public readonly FloatParameter NumericPart;
        public readonly FloatParameter MultiplierPart;
        
        public FloatComplexParameter(float numericValue, float multiplier=1, AParameterCalculator<float> calculateStrategy=null) : 
            base(calculateStrategy ?? new FloatComplexCalculator())
        {
            NumericPart = new FloatParameter(numericValue);
            MultiplierPart = new FloatParameter(multiplier);
            NumericPart.ParameterRecalculated += OnPartRecalculated;
            MultiplierPart.ParameterRecalculated += OnPartRecalculated;
            RecalculateCurentValue();
        }
        
        private void OnPartRecalculated(float newValue)
        {
            RecalculateCurentValue();
        }
    }

    /// <summary> Стандартная стратегия подсчета составного параметра: NumericPart умножается на MultiplierPart. </summary>
    public class FloatComplexCalculator : AParameterCalculator<float>
    {
        public override float CalculateCurrentValue(IParameter<float> parameter)
        {
            var floatComplexParameter = parameter as FloatComplexParameter;
            if (floatComplexParameter == null) throw new InvalidOperationException($"{nameof(FloatComplexCalculator)} requered {nameof(FloatComplexParameter)}.");
            return floatComplexParameter.NumericPart.CurrentValue * floatComplexParameter.MultiplierPart.CurrentValue;
        }
    }
}
