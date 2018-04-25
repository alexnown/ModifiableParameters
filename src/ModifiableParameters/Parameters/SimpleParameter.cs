using System;
using System.Collections.Generic;
using ModifiableParameters.Calculators;

namespace ModifiableParameters.Parameters
{
    public class SimpleParameter <V> : ALimitableParameter<V>, IModifiableParameter<V>
    {
        public V BaseValue
        {
            get { return _baseValue; }
            set
            {
                _baseValue = value;
                RecalculateCurentValue();
            }
        }

        private V _baseValue;

        #region Modifiable implementation

        public event Action<AParameterModifier<V>> ModifierAdded;
        public event Action<AParameterModifier<V>> ModifierRemoved;

        public bool RecalculateOnChangeModifiers { get; set; } = true;

        public int ModifiersCount => _modifiersList.Count;

        private readonly List<AParameterModifier<V>> _modifiersList = new List<AParameterModifier<V>>();

        public void AddModifier(AParameterModifier<V> modifier)
        {
            if (ModifierExists(modifier)) throw new InvalidOperationException("Modifier already exists");
            modifier.OnValueChanged += RecalculateCurentValue;
            _modifiersList.Add(modifier);
            if (RecalculateOnChangeModifiers) RecalculateCurentValue();
            ModifierAdded?.Invoke(modifier);
        }

        public void RemoveModifier(AParameterModifier<V> modifier)
        {
            bool success = _modifiersList.Remove(modifier);
            if (success)
            {
                modifier.OnValueChanged -= RecalculateCurentValue;
                if (RecalculateOnChangeModifiers) RecalculateCurentValue();
                ModifierRemoved?.Invoke(modifier);
            }
        }

        public bool ModifierExists(AParameterModifier<V> modifier)
        {
            return _modifiersList.Contains(modifier);
        }

        public IEnumerable<AParameterModifier<V>> GetModifiers()
        {
            return _modifiersList;
        }

        #endregion

        public SimpleParameter(V baseValue, AParameterCalculator<V> calculateStrategy) : base(calculateStrategy)
        {
            _baseValue = baseValue;
            RecalculateCurentValue();
        }
    }
}
