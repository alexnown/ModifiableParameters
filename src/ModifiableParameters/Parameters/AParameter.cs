using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModifiableParameters.Calculators;

namespace ModifiableParameters.Parameters
{
    public abstract class AParameter<V> : IParameter<V>
    {
        public event Action<V> ParameterRecalculated;
        public V CurrentValue => _currentValue;

        public AParameterCalculator<V> Calculator
        {
            get { return _calculateStrategy; }
            set
            {
                if (value == null) throw new ArgumentNullException("Calculator cant be null.");
                _calculateStrategy = value;
                RecalculateCurentValue();
            }
        }

        protected AParameterCalculator<V> _calculateStrategy;
        protected V _currentValue;
        protected void OnRecalculate(V newValue) => ParameterRecalculated?.Invoke(newValue);
        
        public virtual void RecalculateCurentValue()
        {
            var newValue = _calculateStrategy.CalculateCurrentValue(this);
            _currentValue = newValue;
            OnRecalculate(newValue);
        }

        protected AParameter(AParameterCalculator<V> calculateStrategy)
        {
            _calculateStrategy = calculateStrategy;
        }
    }
}
