using System;
using System.Collections.Generic;
using ModifiableParameters.Calculators;
using ModifiableParameters.Limiters;

namespace ModifiableParameters.Parameters
{
    public abstract class ALimitableParameter<V> : AParameter<V>, ILimitable<V>
    {
        /// <summary> Список требований к подсчитанным параметрам. Дает возможность задавать пределы или делать проверки на 0 для модификаторов умножения.
        /// При изменении списка нужно вручную вызывать перерасчет параметров. </summary>
        private List<AParameterLimiter<V>> _parameterLimitersList;

        /// <summary> Вызывает пересчет текущего значения. </summary>
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

        public event Action<AParameterLimiter<V>> LimiterAdded;
        public event Action<AParameterLimiter<V>> LimiterRemoved;

        public bool RecalculateOnChangeLimiters { get; set; } = true;

        public int LimitersCount => _parameterLimitersList?.Count ?? 0;

        public void AddLimiter(AParameterLimiter<V> limiter)
        {
            if (ContainsLimiter(limiter)) throw new InvalidOperationException("Limiter already exists");
            if (_parameterLimitersList == null) _parameterLimitersList = new List<AParameterLimiter<V>>();
            _parameterLimitersList.Add(limiter);
            if (RecalculateOnChangeLimiters) RecalculateCurentValue();
            LimiterAdded?.Invoke(limiter);
        }

        public void RemoveLimiter(AParameterLimiter<V> limiter)
        {
            if (limiter == null) throw new ArgumentNullException("Limiter is null.");
            bool success = _parameterLimitersList.Remove(limiter);
            if (success)
            {
                if (RecalculateOnChangeLimiters) RecalculateCurentValue();
                LimiterRemoved?.Invoke(limiter);
            }
        }

        public bool ContainsLimiter(AParameterLimiter<V> limiter)
        {
            if (limiter == null) throw new ArgumentNullException("Limiter is null.");
            return _parameterLimitersList?.Contains(limiter) ?? false;
        }

        public IEnumerable<AParameterLimiter<V>> GetLimiters()
        {
            return _parameterLimitersList;
        }

        #endregion

        protected ALimitableParameter(AParameterCalculator<V> calculateStrategy) : base(calculateStrategy)
        {
        }
    }
}
