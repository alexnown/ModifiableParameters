using System;
using ModifiableParameters.Calculators;

namespace ModifiableParameters.Parameters
{
    public class FloatComplexParameter : ALimitableParameter<float>
    {
        public readonly FloatParameter NumericPart;
        public readonly FloatParameter MultiplierPart;

        public FloatComplexParameter(float numericValue, float multiplier = 1, IParameterCalculator<float> calculateStrategy = null) :
            base(calculateStrategy ?? new FloatComplexCalculator())
        {
            NumericPart = new FloatParameter(numericValue);
            MultiplierPart = new FloatParameter(multiplier);
            NumericPart.Recalculated += OnPartRecalculated;
            MultiplierPart.Recalculated += OnPartRecalculated;
            RecalculateCurentValue();
        }

        private void OnPartRecalculated(float newValue)
        {
            RecalculateCurentValue();
        }
    }

    /// <summary> Multiplies NumericPart value by MultiplierPart value. </summary>
    public class FloatComplexCalculator : IParameterCalculator<float>
    {
        public float CalculateCurrentValue(IParameter<float> parameter)
        {
            var floatComplexParameter = parameter as FloatComplexParameter;
            if (floatComplexParameter == null) throw new InvalidOperationException($"{nameof(FloatComplexCalculator)} requered {nameof(FloatComplexParameter)}.");
            return floatComplexParameter.NumericPart.CurrentValue * floatComplexParameter.MultiplierPart.CurrentValue;
        }
    }
}
