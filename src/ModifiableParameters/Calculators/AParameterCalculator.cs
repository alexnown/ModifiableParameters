using ModifiableParameters.Parameters;

namespace ModifiableParameters.Calculators
{
    /// <summary> Алгоритм применения списка модификаторов для вычисления текущего значения параметра.  </summary>
    public abstract class AParameterCalculator<V>
    {
        public abstract V CalculateCurrentValue(IParameter<V> parameter);
    }
}
