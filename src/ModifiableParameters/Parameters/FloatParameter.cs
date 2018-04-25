using ModifiableParameters.Calculators;

namespace ModifiableParameters.Parameters
{
    public class FloatParameter : SimpleParameter<float>
    {
        public FloatParameter(float baseValue) : base(baseValue, new FloatAdditionCalculator())
        {
        }

        public FloatParameter(float baseValue, AParameterCalculator<float> calculateStrategy)
            : base(baseValue, calculateStrategy)
        {
        }
    }

    public class FloatModifier : AParameterModifier<float>
    {
        public FloatModifier(float value) : base(value)
        {
        }
    }
}
