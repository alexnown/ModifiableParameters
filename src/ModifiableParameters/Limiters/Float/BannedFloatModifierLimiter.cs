using System;
using System.Linq;
using ModifiableParameters.Parameters;

namespace ModifiableParameters.Limiters
{
    /// <summary> Checks the list of modifiers for forbidden value. If it finds a banned value, changed correctedCurrValue to this limiter СorrectedValue. </summary>
    public class BannedFloatModifierLimiter : IParameterLimiter<float>
    {
        public float BannedValue;
        public float СorrectedValue;
        public float ToleranceOffset;

        public BannedFloatModifierLimiter(float bannedValue, float correctedValue, float toleranceOffset = 0.001f)
        {
            BannedValue = bannedValue;
            СorrectedValue = correctedValue;
            ToleranceOffset = toleranceOffset;
        }
        
        public bool IsMeetLimit(IParameter<float> parameter, ref float correctedCurrValue)
        {
            IModifiable<float> modifiable = parameter as IModifiable<float>;
            if (modifiable == null || modifiable.ModifiersCount==0) return true;
            if (modifiable.GetModifiers().Any(modifier => Math.Abs(modifier.Value - BannedValue) < ToleranceOffset))
            {
                correctedCurrValue = СorrectedValue;
                return false;
            }
            return true;
        }
    }
}
