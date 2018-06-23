using System;
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

        [TestMethod]
        public void TryAddModifierTest()
        {
            var parameter = new IntParameter(0);
            var modifier = new IntModifier(0);

            bool firstAddingResult = parameter.TryAddModifier(modifier);
            Assert.IsTrue(firstAddingResult);
            bool secondAddomgResult = parameter.TryAddModifier(modifier);
            Assert.IsFalse(secondAddomgResult);
            try
            {
                parameter.TryAddModifier(null);
                Assert.Fail($"{nameof(ArgumentNullException)} not throws on add null modifier.");
            }
            catch (ArgumentNullException e) { }
        }

        [TestMethod]
        public void TryRemoveModifierTest()
        {
            var parameter = new IntParameter(0);
            var modifier = new IntModifier(0);

            bool wrongRemove = parameter.TryRemoveModifier(modifier);
            Assert.IsFalse(wrongRemove);

            parameter.AddModifier(modifier);

            bool firstRemoveResult = parameter.TryRemoveModifier(modifier);
            Assert.IsTrue(firstRemoveResult);
            bool secondRemoveResult = parameter.TryRemoveModifier(modifier);
            Assert.IsFalse(secondRemoveResult);
            try
            {
                parameter.TryRemoveModifier(null);
                Assert.Fail($"{nameof(ArgumentNullException)} not throws on remove null modifier.");
            }
            catch (ArgumentNullException e) { }
        }
    }
}
