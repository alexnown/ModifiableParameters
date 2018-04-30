using System;
using ModifiableParameters.Parameters;

namespace ModifiableParameters.Limiters
{
    /// <summary> If parameter current value equal BannedValue, limiter changes it to CorrectedValue. </summary>
    public class BannedFloatLimiter : IParameterLimiter<float>
    {
        public float BannedValue;
        public float СorrectedValue;
        public float ToleranceOffset;

        public BannedFloatLimiter(float bannedValue = 0, float returnValueIfVioleted = 0, float toleranceOffset = 0.001f)
        {
            BannedValue = bannedValue;
            СorrectedValue = returnValueIfVioleted;
            ToleranceOffset = toleranceOffset;
        }

        public bool IsMeetLimit(IParameter<float> parameter, ref float correctedCurrValue)
        {
            if (Math.Abs(correctedCurrValue - BannedValue) < ToleranceOffset)
            {
                correctedCurrValue = СorrectedValue;
                return false;
            }
            return true;
        }
    }
}
