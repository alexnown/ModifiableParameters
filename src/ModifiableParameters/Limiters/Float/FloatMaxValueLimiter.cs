﻿using ModifiableParameters.Parameters;

namespace ModifiableParameters.Limiters
{
    /// <summary> Set maximum limit to the parameter CurrentValue. </summary>
    public class FloatMaxValueLimiter : IParameterLimiter<float>
    {
        public float MaxValue;

        public FloatMaxValueLimiter(float maxValue)
        {
            MaxValue = maxValue;
        }
        
        public bool IsMeetLimit(IParameter<float> parameter, ref float correctedCurrValue)
        {
            if (correctedCurrValue <= MaxValue) return true;
            correctedCurrValue = MaxValue;
            return false;
        }
    }
}
