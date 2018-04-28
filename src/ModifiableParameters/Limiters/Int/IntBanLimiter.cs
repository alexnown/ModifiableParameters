using ModifiableParameters.Parameters;

namespace ModifiableParameters.Limiters
{
    public class IntBanLimiter : IParameterLimiter<int>
    {
        public int BannedValue;
        public int CorrectValue;

        public bool IsMeetLimit(IParameter<int> parameter, ref int correctedCurrValue)
        {
            if (correctedCurrValue != BannedValue) return true;
            correctedCurrValue = CorrectValue;
            return false;
        }
    }
}
