using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Extensions;
using ModifiableParameters.Parameters;

namespace TestsModifiableParameters.Extensions
{
    [TestClass]
    public class ModifiersExtensionsTests
    {
        [TestMethod]
        public void RemoveAllModifiersTest()
        {
            var parameter = new IntParameter(0);
            var modifiersArray = new[]
            {
                new ParameterModifier<int>(5), new ParameterModifier<int>(10),
            };
            foreach (var modifier in modifiersArray)
            {
                parameter.AddModifier(modifier);
            }

            var removedModifiersArray = parameter.RemoveAllModifiers();
            Assert.AreEqual(modifiersArray.Length, removedModifiersArray.Length);
            foreach (var modifier in modifiersArray)
            {
                Assert.IsTrue(removedModifiersArray.Contains(modifier));
            }
        }

        [TestMethod]
        public void AddAllModifiersTest()
        {
            var parameter = new IntParameter(0);
            var modifiersArray = new[]
            {
                new ParameterModifier<int>(5), new ParameterModifier<int>(10),
            };
            parameter.AddAllModifiers(modifiersArray);

            Assert.AreEqual(modifiersArray.Length, parameter.ModifiersCount);
            foreach (var modifier in modifiersArray)
            {
                Assert.IsTrue(parameter.GetModifiers().Contains(modifier));
            }
        }
    }
}
