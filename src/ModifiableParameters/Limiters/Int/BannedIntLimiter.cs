using ModifiableParameters.Parameters;

namespace ModifiableParameters.Limiters
{
    /// <summary> If parameter current value equal BannedValue, limiter changes it to CorrectedValue. </summary>
    public class BannedIntLimiter : IParameterLimiter<int>
    {
        public int BannedValue;
        public int CorrectValue;

        public BannedIntLimiter(int bannedValue, int correctValue)
        {
            BannedValue = bannedValue;
            CorrectValue = correctValue;
        }

        public bool IsMeetLimit(IParameter<int> parameter, ref int correctedCurrValue)
        {
            if (correctedCurrValue != BannedValue) return true;
            correctedCurrValue = CorrectValue;
            return false;
        }
    }
}
