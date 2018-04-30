using ModifiableParameters.Parameters;

namespace ModifiableParameters.Limiters
{
    /// <summary> Set minimum limit to the parameter CurrentValue. </summary>
    public class IntMinValueLimiter : IParameterLimiter<int>
    {
        public int MinValue;

        public IntMinValueLimiter(int minValue)
        {
            MinValue = minValue;
        }

        public bool IsMeetLimit(IParameter<int> parameter, ref int correctedCurrValue)
        {
            if (correctedCurrValue >= MinValue) return false;
            correctedCurrValue = MinValue;
            return true;
        }
    }
}
