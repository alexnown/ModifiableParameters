using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Parameters;

namespace TestsModifiableParameters.Parameters
{
    [TestClass]
    public abstract class ASimpleParameterTests<P, V> where P : SimpleParameter<V>
    {

        public abstract Func<V> GetRandomValueFunc { get; }
        public abstract void CheckBaseValuesAfterInstance();

        [TestMethod]
        public void RunParameterInterfaceTests()
        {
            var parameter = CreateAndCheckValue(GetRandomValueFunc());
            var parameterTests = new ParameterInterfaceTests<V>();
            parameterTests.RunTests(parameter, GetRandomValueFunc());
        }

        [TestMethod]
        public void RunModifiableInterfaceTests()
        {
            var parameter = CreateAndCheckValue(GetRandomValueFunc());
            var modifiableTests = new ModifiableInterfaceTests<V>();
            modifiableTests.RunTests(parameter);
        }

        [TestMethod]
        public void RunModifiableParameterInterfaceTests()
        {
            var parameter = CreateAndCheckValue(GetRandomValueFunc());
            var modifiableParameterTests = new ModifiableParameterInterfaceTests<V>();
            modifiableParameterTests.RunTests(parameter, GetRandomValueFunc);
        }

        protected P CreateAndCheckValue(V baseValue)
        {
            var parameter = Activator.CreateInstance(typeof(P), baseValue) as SimpleParameter<V>;
            Assert.AreEqual(baseValue, parameter.BaseValue);
            Assert.AreEqual(baseValue, parameter.CurrentValue);
            return (P)parameter;
        }
    }
}
