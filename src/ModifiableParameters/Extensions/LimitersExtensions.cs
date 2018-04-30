using System.Collections.Generic;
using System.Linq;
using ModifiableParameters.Limiters;
using ModifiableParameters.Parameters;

namespace ModifiableParameters.Extensions
{
    public static class LimitersExtensions
    {
        /// <summary> Remove and return all limiters from limitable parameter. </summary>
        public static IParameterLimiter<V>[] RemoveAllLimiters<V>(this ILimitable<V> limitableParameter)
        {
            if (limitableParameter.LimitersCount == 0) return null;
            var limiters = limitableParameter.GetLimiters().ToArray();
            foreach (var limiter in limiters)
            {
                limitableParameter.RemoveLimiter(limiter);
            }
            return limiters;
        }

        /// <summary> Add list of limiters to limitable parameter. </summary>
        public static void AddAllLimiters<V>(this ILimitable<V> limitableParameter, IEnumerable<IParameterLimiter<V>> limiters)
        {
            foreach (var limiter in limiters)
            {
                limitableParameter.AddLimiter(limiter);
            }
        }
    }
}
