using System;

namespace ModifiableParameters
{
    public abstract class AParameterModifier<V>
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

        protected AParameterModifier(V value)
        {
            _value = value;
        }
    }

}
