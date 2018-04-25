using ModifiableParameters.Parameters;

namespace TestsModifiableParameters
{
    /// <summary> List of tests for any parameter implementation. </summary>
    public abstract class AParameterTestsList<P, V> where P : IParameter<V>
    {
        public abstract void TestBaseValueAfterCreate();
        public abstract void SetNullCalculatorException();
    }
}
