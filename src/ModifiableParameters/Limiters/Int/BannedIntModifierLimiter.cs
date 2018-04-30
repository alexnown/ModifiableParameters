using System;
using System.Linq;
using ModifiableParameters.Parameters;

namespace ModifiableParameters.Limiters
{
    /// <summary> Checks the list of modifiers for forbidden value. If it finds a banned value, changed correctedCurrValue to this limiter СorrectedValue. </summary>
    public class BannedIntModifierLimiter : IParameterLimiter<int>
    {
        public int BannedValue;
        public int СorrectedValue;

        public BannedIntModifierLimiter(int bannedValue, int returnValueIfVioleted)
        {
            BannedValue = bannedValue;
            СorrectedValue = returnValueIfVioleted;
        }

        public bool IsMeetLimit(IParameter<int> parameter, ref int correctedCurrValue)
        {
            IModifiable<int> modifiable = parameter as IModifiable<int>;
            if (modifiable == null || modifiable.ModifiersCount == 0) return true;
            if (modifiable.GetModifiers().Any(modifier => modifier.Value == BannedValue))
            {
                correctedCurrValue = СorrectedValue;
                return false;
            }
            return true;
        }
    }
}
