using ModifiableParameters.Calculators;

namespace ModifiableParameters.Parameters
{
    public class IntParameter : SimpleParameter<int>
    {
        public IntParameter(int baseValue) : base(baseValue, new IntAdditionCalculator())
        {
        }

        public IntParameter(int baseValue, IParameterCalculator<int> calculateStrategy) : base(baseValue, calculateStrategy)
        {
        }
    }

    public class IntModifier : ParameterModifier<int>
    {
        public IntModifier(int value) : base(value)
        {
        }
    }
}
