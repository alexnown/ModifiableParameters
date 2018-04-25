using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Parameters;

namespace TestsModifiableParameters
{
    [TestClass]
    public abstract class ASimpleParameterTests<P, V> : AParameterTestsList<P, V> where P : SimpleParameter<V>
    {
        [TestMethod]
        public override void SetNullCalculatorException()
        {
            var parameter = Activator.CreateInstance(typeof(P), default(V)) as SimpleParameter<V>;
            try
            {
                parameter.Calculator = null;
                Assert.Fail("Must throw null exception.");
            }
            catch (ArgumentNullException e) { }
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
