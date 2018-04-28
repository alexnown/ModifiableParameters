using ModifiableParameters.Parameters;

namespace ModifiableParameters.Limiters
{
    /// <summary> Limiter allows to apply some requirements to calculated CurrentValue. 
    /// Use for setting limits for current value or checking for 0, etc. </summary>
    public interface IParameterLimiter<V>
    {
        /// <summary>
        /// Check if the argument correctedCurrValue meets some requirements. 
        /// If not, change ref correctedCurrValue to correct value and return false.
        /// </summary>
        /// <returns>True if correctedCurrValue meet this limiter.</returns>
        bool IsMeetLimit(IParameter<V> parameter, ref V correctedCurrValue);
    }
}
