using ModifiableParameters.Limiters;
using ModifiableParameters.Parameters;

namespace ModifiableParameters.Extensions
{
    public static class IntParameterExtensions
    {
        /// <summary> Add or update IntMaxValueLimiter to the parameter. </summary>
        public static IntParameter SetMax(this IntParameter parameter, int maxValue)
        {
            bool limiterAlreadyExists = false;
            if (parameter.LimitersCount > 0)
            {
                foreach (var limiter in parameter.GetLimiters())
                {
                    if (limiter is IntMaxValueLimiter)
                    {
                        var existedLimiter = limiter as IntMaxValueLimiter;
                        existedLimiter.MaxValue = maxValue;
                        limiterAlreadyExists = true;
                        break;
                    }
                }
            }
            if (limiterAlreadyExists == false)
            {
                var limiter = new IntMaxValueLimiter(maxValue);
                parameter.AddLimiter(limiter);
            }
            return parameter;
        }

        /// <summary> Add or update IntMinValueLimiter to the parameter. </summary>
        public static IntParameter SetMin(this IntParameter parameter, int minValue)
        {
            bool limiterAlreadyExists = false;
            if (parameter.LimitersCount > 0)
            {
                foreach (var limiter in parameter.GetLimiters())
                {
                    if (limiter is IntMinValueLimiter)
                    {
                        var existedLimiter = limiter as IntMinValueLimiter;
                        existedLimiter.MinValue = minValue;
                        limiterAlreadyExists = true;
                        break;
                    }
                }
            }
            if (limiterAlreadyExists == false)
            {
                var limiter = new IntMinValueLimiter(minValue);
                parameter.AddLimiter(limiter);
            }
            return parameter;
        }

        /// <summary> Add BannedIntLimiter to the parameter limiters. If the parameter CurrentValue equals bannedValue, set it equal correctValue. </summary>
        public static IntParameter BanValue(this IntParameter parameter, int bannedValue, int correctValue)
        {
            var limiter = new BannedIntLimiter(bannedValue,correctValue);
            parameter.AddLimiter(limiter);
            return parameter;
        }

        /// <summary> Add BannedIntModifierLimiter to the parameter limiters. 
        /// If some modifier value equals bannedValue, set parameter CurrentValue equal correctValue. </summary>
        public static IntParameter BanModifierValue(this IntParameter parameter, int bannedValue, int correctValue)
        {
            var limiter = new BannedIntModifierLimiter(bannedValue, correctValue);
            parameter.AddLimiter(limiter);
            return parameter;
        }
    }
}
