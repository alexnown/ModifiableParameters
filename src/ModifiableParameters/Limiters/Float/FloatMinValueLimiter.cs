﻿using ModifiableParameters.Parameters;

namespace ModifiableParameters.Limiters
{
    /// <summary> Set minimum limit to the parameter CurrentValue. </summary>
    public class FloatMinValueLimiter : IParameterLimiter<float>
    {
        public float MinValue;

        public FloatMinValueLimiter(float minValue)
        {
            MinValue = minValue;
        }
        
        public bool IsMeetLimit(IParameter<float> parameter, ref float correctedCurrValue)
        {
            if (correctedCurrValue >= MinValue) return false;
            correctedCurrValue = MinValue;
            return true;
        }
    }
}
