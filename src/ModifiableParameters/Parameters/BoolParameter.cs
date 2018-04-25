using ModifiableParameters.Calculators;

namespace ModifiableParameters.Parameters
{
    public class BoolParameter : SimpleParameter<bool>
    {
        public BoolParameter(bool baseValue) : base(baseValue, new AndGateRequarement())
        {
        }

        public BoolParameter(bool baseValue, AParameterCalculator<bool> calculateStrategy) : base(baseValue, calculateStrategy)
        {
        }
    }

    public class BoolModifier : AParameterModifier<bool>
    {
        public BoolModifier(bool value) : base(value)
        {
        }
    }
}
