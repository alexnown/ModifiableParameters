using System;
using System.Collections.Generic;
using ModifiableParameters.Calculators;
using ModifiableParameters.Limiters;

namespace ModifiableParameters.Parameters
{
    /// <summary> Abstract parameter that implements ILimitable interface.
    /// Limiters apply to CurrentValue after recalculate and allow you to limit the resulting value. </summary>
    public abstract class ALimitableParameter<V> : AParameter<V>, ILimitable<V>
    {
        private List<IParameterLimiter<V>> _parameterLimitersList;

        public sealed override void RecalculateCurentValue()
        {
            var newValue = _calculateStrategy.CalculateCurrentValue(this);
            if (_parameterLimitersList != null && _parameterLimitersList.Count > 0)
            {
                foreach (var limiter in _parameterLimitersList)
                {
                    limiter.IsMeetLimit(this, ref newValue);
                }
            }
            _currentValue = newValue;
            OnRecalculate(newValue);
        }

        #region ILimitable implementation

        public event Action<IParameterLimiter<V>> LimiterAdded;
        public event Action<IParameterLimiter<V>> LimiterRemoved;

        public bool RecalculateOnChangeLimiters { get; set; } = true;

        public int LimitersCount => _parameterLimitersList?.Count ?? 0;

        public void AddLimiter(IParameterLimiter<V> limiter)
        {
            if (ContainsLimiter(limiter)) throw new InvalidOperationException("Limiter already exists");
            if (_parameterLimitersList == null) _parameterLimitersList = new List<IParameterLimiter<V>>();
            _parameterLimitersList.Add(limiter);
            if (RecalculateOnChangeLimiters) RecalculateCurentValue();
            LimiterAdded?.Invoke(limiter);
        }

        public void RemoveLimiter(IParameterLimiter<V> limiter)
        {
            if (limiter == null) throw new ArgumentNullException("Limiter is null.");
            bool success = _parameterLimitersList.Remove(limiter);
            if (success)
            {
                if (RecalculateOnChangeLimiters) RecalculateCurentValue();
                LimiterRemoved?.Invoke(limiter);
            }
        }

        public bool ContainsLimiter(IParameterLimiter<V> limiter)
        {
            if (limiter == null) throw new ArgumentNullException("Limiter is null.");
            return _parameterLimitersList?.Contains(limiter) ?? false;
        }

        public IEnumerable<IParameterLimiter<V>> GetLimiters()
        {
            return _parameterLimitersList;
        }

        #endregion

        protected ALimitableParameter(IParameterCalculator<V> calculateStrategy) : base(calculateStrategy)
        {
        }
    }
}
