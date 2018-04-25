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

    public interface IHasBaseValue <V>
    {
        V BaseValue { get; set; }
    }
    
    public interface IModifiable<V>
    {
        bool RecalculateOnChangeModifiers { get; set; }
        int ModifiersCount { get; }
        void AddModifier(AParameterModifier<V> modifier);
        void RemoveModifier(AParameterModifier<V> modifier);
        bool ModifierExists(AParameterModifier<V> modifier);
        IEnumerable<AParameterModifier<V>> GetModifiers();
    }

    public interface ILimitable<V>
    {
        bool RecalculateOnChangeLimiters { get; set; }
        int LimitersCount { get; }
        void AddLimiter(AParameterLimiter<V> limiter);
        void RemoveLimiter(AParameterLimiter<V> limiter);
        bool LimiterExists(AParameterLimiter<V> limiter);
        IEnumerable<AParameterLimiter<V>> GetLimiters();
    }

    public interface IModifiableParameter<V> : IParameter<V>, IModifiable<V>, IHasBaseValue<V>
    {

    }
}
