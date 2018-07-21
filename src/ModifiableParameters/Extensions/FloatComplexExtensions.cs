using ModifiableParameters.Parameters;

namespace ModifiableParameters.Extensions
{
    public static class FloatComplexExtensions
    {
        public static void RemoveAllModifiersAndLimiters(this FloatComplexParameter parameter)
        {
            parameter.RemoveAllLimiters();
            parameter.NumericPart.RemoveAllLimiters();
            parameter.NumericPart.RemoveAllModifiers();
            parameter.MultiplierPart.RemoveAllLimiters();
            parameter.MultiplierPart.RemoveAllModifiers();
        }
    }
}
