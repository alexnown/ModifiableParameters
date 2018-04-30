using ModifiableParameters.Parameters;

namespace ModifiableParameters.Limiters
{
    /// <summary> Set maximum limit to the parameter CurrentValue. </summary>
    public class IntMaxValueLimiter : IParameterLimiter<int>
    {
        public int MaxValue;

        public IntMaxValueLimiter(int maxValue)
        {
            MaxValue = maxValue;
        }

        public bool IsMeetLimit(IParameter<int> parameter, ref int correctedCurrValue)
        {
            if (correctedCurrValue <= MaxValue) return true;
            correctedCurrValue = MaxValue;
            return false;
        }
    }
}
