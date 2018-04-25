using System;
using System.Linq;
using ModifiableParameters.Parameters;

namespace ModifiableParameters.Limiters
{
    /// <summary> Дает возможность проверить нет ли модификатора с запрещенным значением. 
    /// Если находит такой модификатор (например 0), то меняет СorrectedValue на заданное.</summary>
    public class FloatModifierBanLimiter : AParameterLimiter<float>
    {
        public float BannedValue;
        public float СorrectedValue;
        public float ToleranceOffset;

        public FloatModifierBanLimiter(float bannedValue=0, float returnValueIfVioleted=0, float toleranceOffset = 0.001f)
        {
            BannedValue = bannedValue;
            СorrectedValue = returnValueIfVioleted;
            ToleranceOffset = toleranceOffset;
        }
        
        public override bool IsMeetLimit(IParameter<float> parameter, ref float correctedCurrValue)
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
