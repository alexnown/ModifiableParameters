using ModifiableParameters.Calculators;

namespace ModifiableParameters.Parameters
{
    public class BoolParameter : SimpleParameter<bool>
    {
        public BoolParameter(bool baseValue) : base(baseValue, new AndGateCalculator())
        {
        }

        public BoolParameter(bool baseValue, IParameterCalculator<bool> calculateStrategy) : base(baseValue, calculateStrategy)
        {
        }
    }

    public class BoolModifier : ParameterModifier<bool>
    {
        public BoolModifier(bool value) : base(value)
        {
        }
    }
}
