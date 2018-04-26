using System;

namespace ModifiableParameters.Parameters
{
    public class ParameterModifier<V>
    {
        public event Action OnValueChanged;
        
        public V Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnValueChanged?.Invoke();
            }
        }

        private V _value;

        public ParameterModifier(V value)
        {
            _value = value;
        }
    }

}
