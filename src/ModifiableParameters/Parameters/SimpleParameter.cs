using System;
using System.Collections.Generic;
using ModifiableParameters.Calculators;

namespace ModifiableParameters.Parameters
{
    public class SimpleParameter<V> : ALimitableParameter<V>, IModifiableParameter<V>
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

        #region IModifiable implementation

        public event Action<ParameterModifier<V>> ModifierAdded;
        public event Action<ParameterModifier<V>> ModifierRemoved;

        public bool RecalculateOnChangeModifiers { get; set; } = true;

        public int ModifiersCount => _modifiersList.Count;

        private readonly List<ParameterModifier<V>> _modifiersList = new List<ParameterModifier<V>>();

        public void AddModifier(ParameterModifier<V> modifier)
        {
            if (ContainsModifier(modifier)) throw new InvalidOperationException("Modifier already exists");
            modifier.OnValueChanged += RecalculateCurentValue;
            _modifiersList.Add(modifier);
            ModifierAdded?.Invoke(modifier);
            if (RecalculateOnChangeModifiers)
                RecalculateCurentValue();
        }

        public void RemoveModifier(ParameterModifier<V> modifier)
        {
            if (modifier == null) throw new ArgumentNullException("Modifier is null.");
            bool success = _modifiersList.Remove(modifier);
            if (success)
            {
                modifier.OnValueChanged -= RecalculateCurentValue;
                ModifierRemoved?.Invoke(modifier);
                if (RecalculateOnChangeModifiers)
                    RecalculateCurentValue();
            }
        }

        public bool ContainsModifier(ParameterModifier<V> modifier)
        {
            if (modifier == null) throw new ArgumentNullException("Modifier is null.");
            return _modifiersList.Contains(modifier);
        }

        public IEnumerable<ParameterModifier<V>> GetModifiers()
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
