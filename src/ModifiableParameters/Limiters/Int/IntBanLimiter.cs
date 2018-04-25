using ModifiableParameters.Parameters;

namespace ModifiableParameters.Limiters
{
    public class IntBanLimiter : AParameterLimiter<int>
    {
        public int BannedValue;
        public int CorrectValue;

        public override bool IsMeetLimit(IParameter<int> parameter, ref int correctedCurrValue)
        {
            if (correctedCurrValue != BannedValue) return true;
            correctedCurrValue = CorrectValue;
            return false;
        }
    }
}
