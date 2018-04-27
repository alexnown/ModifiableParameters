using System.Collections.Generic;
using System.Linq;
using ModifiableParameters.Limiters;
using ModifiableParameters.Parameters;

namespace ModifiableParameters.Extensions
{
    public static class LimitersExtentions
    {
        public static AParameterLimiter<V>[] RemoveAllLimiters<V>(this ILimitable<V> limitableParameter)
        {
            if (limitableParameter.LimitersCount == 0) return null;
            var limiters = limitableParameter.GetLimiters().ToArray();
            foreach (var limiter in limiters)
            {
                limitableParameter.RemoveLimiter(limiter);
            }
            return limiters;
        }

        public static void AddAllLimiters<V>(this ILimitable<V> limitableParameter, IEnumerable<AParameterLimiter<V>> limiters)
        {
            foreach (var limiter in limiters)
            {
                limitableParameter.AddLimiter(limiter);
            }
        }
    }
}
