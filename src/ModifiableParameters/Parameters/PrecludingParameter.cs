using System.Linq;
using ModifiableParameters.Calculators;

namespace ModifiableParameters.Parameters
{
    /// <summary> Precluding parameter contains modifiers with specifier reason to preclusion. If contains any modifier, return !BaseValue. </summary>
    public class PrecludingParameter : SimpleParameter<bool>
    {
        public PrecludingParameter(bool baseValue) : base(baseValue, new EmptyModifiersRequirement())
        {
        }

        public PrecludingParameter(bool baseValue, IParameterCalculator<bool> calculateStrategy) : base(baseValue, calculateStrategy)
        {
        }

        public string[] GetReasones()
        {
            if (ModifiersCount == 0)
            {
                return new string[0];
            }
            else if (ModifiersCount == 1)
            {
                return new string[] { GetFirstReason() };
            }
            return GetModifiers().Select(modifier => (modifier as PrecludingModifier)?.Reason).ToArray();
        }

        public string GetFirstReason()
        {
            if (ModifiersCount == 0) return null;
            var inhibitorModifier = GetModifiers().FirstOrDefault() as PrecludingModifier;
            return inhibitorModifier?.Reason;
        }
    }

    public class PrecludingModifier : ParameterModifier<bool>
    {
        public string Reason { get; protected set; }

        public PrecludingModifier(string reason, bool value = false) : base(value)
        {
            Reason = reason;
        }
    }
}
