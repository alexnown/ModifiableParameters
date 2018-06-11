using ModifiableParameters.Parameters;

namespace ModifiableParameters.Calculators
{
    /// <summary> Just sets ConstantValue as CurrentValue of parameter regardless of parameter properties. </summary>
    public class ConstantValueCalculator<V> : IParameterCalculator<V>
    {
        public V ConstantValue { get; set; }

        public ConstantValueCalculator(V constantValue)
        {
            ConstantValue = constantValue;
        }  

        public V CalculateCurrentValue(IParameter<V> parameter)
        {
            throw new System.NotImplementedException();
        }
    }
}
