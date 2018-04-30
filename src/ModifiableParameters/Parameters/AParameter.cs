using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModifiableParameters.Calculators;

namespace ModifiableParameters.Parameters
{
    public abstract class AParameter<V> : IParameter<V>
    {
        public event Action<V> Recalculated;
        public V CurrentValue => _currentValue;

        public IParameterCalculator<V> Calculator
        {
            get { return _calculateStrategy; }
            set
            {
                if (value == null) throw new ArgumentNullException("Calculator cant be null.");
                _calculateStrategy = value;
                RecalculateCurentValue();
            }
        }

        protected IParameterCalculator<V> _calculateStrategy;
        protected V _currentValue;
        protected void OnRecalculate(V newValue) => Recalculated?.Invoke(newValue);
        
        public virtual void RecalculateCurentValue()
        {
            var newValue = _calculateStrategy.CalculateCurrentValue(this);
            _currentValue = newValue;
            OnRecalculate(newValue);
        }

        protected AParameter(IParameterCalculator<V> calculateStrategy)
        {
            _calculateStrategy = calculateStrategy;
        }
    }
}
