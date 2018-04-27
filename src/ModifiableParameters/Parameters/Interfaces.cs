using System;
using System.Collections.Generic;
using ModifiableParameters.Calculators;
using ModifiableParameters.Limiters;

namespace ModifiableParameters.Parameters
{
    public interface IParameter<V>
    {
        event Action<V> ParameterRecalculated;
        V CurrentValue { get; }
        AParameterCalculator<V> Calculator { get; set; }
        void RecalculateCurentValue();
    }
    
    public interface IModifiable<V>
    {
        event Action<ParameterModifier<V>> ModifierAdded;
        event Action<ParameterModifier<V>> ModifierRemoved;
        bool RecalculateOnChangeModifiers { get; set; }
        int ModifiersCount { get; }
        void AddModifier(ParameterModifier<V> modifier);
        void RemoveModifier(ParameterModifier<V> modifier);
        bool ContainsModifier(ParameterModifier<V> modifier);
        IEnumerable<ParameterModifier<V>> GetModifiers();
    }

    public interface IModifiableParameter<V> : IParameter<V>, IModifiable<V>
    {
        V BaseValue { get; set; }
    }

    public interface ILimitable<V>
    {
        event Action<AParameterLimiter<V>> LimiterAdded;
        event Action<AParameterLimiter<V>> LimiterRemoved;
        bool RecalculateOnChangeLimiters { get; set; }
        int LimitersCount { get; }
        void AddLimiter(AParameterLimiter<V> limiter);
        void RemoveLimiter(AParameterLimiter<V> limiter);
        bool ContainsLimiter(AParameterLimiter<V> limiter);
        IEnumerable<AParameterLimiter<V>> GetLimiters();
    }
}
