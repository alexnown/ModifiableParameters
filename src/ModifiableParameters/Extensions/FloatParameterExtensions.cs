using ModifiableParameters.Limiters;
using ModifiableParameters.Parameters;

namespace ModifiableParameters.Extensions
{
    public static class FloatParameterExtensions
    {
        /// <summary> Add or update IntMaxValueLimiter to the parameter. </summary>
        public static FloatParameter SetMax(this FloatParameter parameter, float maxValue)
        {
            bool limiterAlreadyExists = false;
            if (parameter.LimitersCount > 0)
            {
                foreach (var limiter in parameter.GetLimiters())
                {
                    if (limiter is FloatMaxValueLimiter)
                    {
                        var existedLimiter = limiter as FloatMaxValueLimiter;
                        existedLimiter.MaxValue = maxValue;
                        limiterAlreadyExists = true;
                        break;
                    }
                }
            }
            if (limiterAlreadyExists == false)
            {
                var limiter = new FloatMaxValueLimiter(maxValue);
                parameter.AddLimiter(limiter);
            }
            return parameter;
        }

        /// <summary> Add or update IntMinValueLimiter to the parameter. </summary>
        public static FloatParameter SetMin(this FloatParameter parameter, float minValue)
        {
            bool limiterAlreadyExists = false;
            if (parameter.LimitersCount > 0)
            {
                foreach (var limiter in parameter.GetLimiters())
                {
                    if (limiter is FloatMinValueLimiter)
                    {
                        var existedLimiter = limiter as FloatMinValueLimiter;
                        existedLimiter.MinValue = minValue;
                        limiterAlreadyExists = true;
                        break;
                    }
                }
            }
            if (limiterAlreadyExists == false)
            {
                var limiter = new FloatMinValueLimiter(minValue);
                parameter.AddLimiter(limiter);
            }
            return parameter;
        }

        /// <summary> Add BannedIntLimiter to the parameter limiters. If the parameter CurrentValue equals bannedValue, set it equal correctValue. </summary>
        public static FloatParameter BanValue(this FloatParameter parameter, float bannedValue, float correctValue, float toleranceOffset = 0.001f)
        {
            var limiter = new BannedFloatLimiter(bannedValue, correctValue, toleranceOffset);
            parameter.AddLimiter(limiter);
            return parameter;
        }

        /// <summary> Add BannedIntModifierLimiter to the parameter limiters. 
        /// If some modifier value equals bannedValue, set parameter CurrentValue equal correctValue. </summary>
        public static FloatParameter BanModifierValue(this FloatParameter parameter, float bannedValue, float correctValue, float toleranceOffset = 0.001f)
        {
            var limiter = new BannedFloatModifierLimiter(bannedValue, correctValue, toleranceOffset);
            parameter.AddLimiter(limiter);
            return parameter;
        }
    }
}
