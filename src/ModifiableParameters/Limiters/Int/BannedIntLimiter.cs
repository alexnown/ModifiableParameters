using ModifiableParameters.Parameters;

namespace ModifiableParameters.Limiters
{
    /// <summary> If parameter current value equal BannedValue, limiter changes it to CorrectedValue. </summary>
    public class BannedIntLimiter : IParameterLimiter<int>
    {
        public int BannedValue;
        public int CorrectedValue;

        public BannedIntLimiter(int bannedValue, int correctedValue)
        {
            BannedValue = bannedValue;
            CorrectedValue = correctedValue;
        }

        public bool IsMeetLimit(IParameter<int> parameter, ref int correctedCurrValue)
        {
            if (correctedCurrValue != BannedValue) return true;
            correctedCurrValue = CorrectedValue;
            return false;
        }
    }
}
