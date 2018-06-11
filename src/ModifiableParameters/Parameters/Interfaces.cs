using System;
using System.Collections.Generic;
using ModifiableParameters.Calculators;
using ModifiableParameters.Limiters;

namespace ModifiableParameters.Parameters
{
    public interface IParameter<V>
    {
        event Action<V> Recalculated;
        V CurrentValue { get; }
        IParameterCalculator<V> Calculator { get; set; }
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

    public interface IHasBaseValue<V>
    {
        V BaseValue { get; set; }
    }

    public interface IModifiableParameter<V> : IParameter<V>, IModifiable<V>, IHasBaseValue<V>
    {
        
    }
    
    public interface ILimitable<V>
    {
        event Action<IParameterLimiter<V>> LimiterAdded;
        event Action<IParameterLimiter<V>> LimiterRemoved;
        bool RecalculateOnChangeLimiters { get; set; }
        int LimitersCount { get; }
        void AddLimiter(IParameterLimiter<V> limiter);
        void RemoveLimiter(IParameterLimiter<V> limiter);
        bool ContainsLimiter(IParameterLimiter<V> limiter);
        IEnumerable<IParameterLimiter<V>> GetLimiters();
    }

    public interface ILimitableParameter<V> : IParameter<V>, ILimitable<V>
    {

    }
}
