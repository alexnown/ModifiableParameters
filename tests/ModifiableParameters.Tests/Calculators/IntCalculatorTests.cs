using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Calculators;
using ModifiableParameters.Parameters;

namespace TestsModifiableParameters.Calculators
{
    [TestClass]
    public class IntCalculatorTests
    {
        private readonly Random _random = new Random();

        [TestMethod]
        public void IntAdditionCalculatorTest()
        {
            TestAdditionCalculatorWithValues(10, 5);
            TestAdditionCalculatorWithValues(0, 0);
            TestAdditionCalculatorWithValues(15);
            TestAdditionCalculatorWithValues(-20, 10);
            TestAdditionCalculatorWithValues(int.MaxValue, int.MinValue+1);

            RunAdditionTestsWithRandomValues(100);
        }

        private void RunAdditionTestsWithRandomValues(int testsCount)
        {
            for (int i = 0; i < testsCount; i++)
            {
                int modifiersCount = _random.Next(0, 4);

                int[] modifierValues = new int[modifiersCount];
                if (modifiersCount > 0)
                {
                    for (int j = 0; j < modifierValues.Length; j++)
                    {
                        modifierValues[j] = GenerateRandomValue(1000);
                    }
                }

                int baseValue = GenerateRandomValue(1000);
                TestAdditionCalculatorWithValues(baseValue, modifierValues);
            }
        }

        private int GenerateRandomValue(int range)
        {
            double value = range * (_random.NextDouble() - 0.5f);
            return (int)value;
        }

        private void TestAdditionCalculatorWithValues(int baseValue, params int[] modifierValues)
        {
            var calculator = new IntAdditionCalculator();
            var floatParameter = new IntParameter(baseValue, calculator) { RecalculateOnChangeModifiers = true };
            float expectedValue = baseValue;
            foreach (var modifierValue in modifierValues)
            {
                var modifier = new IntModifier(modifierValue);
                floatParameter.AddModifier(modifier);
                expectedValue += modifierValue;
            }
            Assert.AreEqual(expectedValue, floatParameter.CurrentValue);
        }
    }
}
