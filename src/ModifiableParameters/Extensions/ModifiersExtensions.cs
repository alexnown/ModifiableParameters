using System.Collections.Generic;
using System.Linq;
using ModifiableParameters.Parameters;

namespace ModifiableParameters.Extensions
{
    public static class ModifiersExtensions
    {
        /// <summary> Remove and return all modifiers from modifiable parameter. </summary>
        public static ParameterModifier<V>[] RemoveAllModifiers<V>(this IModifiable<V> modifiableParameter)
        {
            if (modifiableParameter.ModifiersCount == 0) return null;
            var modifiersList = modifiableParameter.GetModifiers().ToArray();
            foreach (var modifier in modifiersList)
            {
                modifiableParameter.RemoveModifier(modifier);
            }
            return modifiersList;
        }

        /// <summary> Add list of modifiers to modifiable parameter. </summary>
        public static void AddAllModifiers<V>(this IModifiable<V> modifiableParameter, IEnumerable<ParameterModifier<V>> modifiers)
        {
            foreach (var modifier in modifiers)
            {
                modifiableParameter.AddModifier(modifier);
            }
        }

        /// <summary> Returns true if modifier not contains in parameter and was successfully added. </summary>
        public static bool TryAddModifier<V>(this IModifiable<V> modifiableParameter, ParameterModifier<V> modifier)
        {
            if(modifiableParameter.ContainsModifier(modifier)) return false;
            modifiableParameter.AddModifier(modifier);
            return true;
        }

        /// <summary> Returns true if modifier contains in parameter and was successfully removed. </summary>
        public static bool TryRemoveModifier<V>(this IModifiable<V> modifiableParameter, ParameterModifier<V> modifier)
        {
            if (modifiableParameter.ContainsModifier(modifier))
            {
                modifiableParameter.RemoveModifier(modifier);
                return true;
            }
            return false;
        }
    }
}
