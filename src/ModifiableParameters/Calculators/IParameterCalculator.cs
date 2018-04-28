using ModifiableParameters.Parameters;

namespace ModifiableParameters.Calculators
{
    /// <summary> Algorithm for applying the modifier list to calculate the CurrentValue of the parameter. </summary>
    public interface IParameterCalculator<V>
    {
        V CalculateCurrentValue(IParameter<V> parameter);
    }
}
