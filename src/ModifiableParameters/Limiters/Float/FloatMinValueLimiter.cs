﻿using ModifiableParameters.Parameters;

namespace ModifiableParameters.Limiters
{
    /// <summary> Задает минимальное значение для CurrentValue параметра. </summary>
    public class FloatMinValueLimiter : AParameterLimiter<float>
    {
        public float MinValue;

        public FloatMinValueLimiter(float minValue)
        {
            MinValue = minValue;
        }
        
        public override bool IsMeetLimit(IParameter<float> parameter, ref float correctedCurrValue)
        {
            if (correctedCurrValue >= MinValue) return false;
            correctedCurrValue = MinValue;
            return true;
        }
    }
}
