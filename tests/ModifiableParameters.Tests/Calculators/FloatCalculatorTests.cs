using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Calculators;
using ModifiableParameters.Parameters;

namespace TestsModifiableParameters.Calculators
{
    [TestClass]
    public class FloatCalculatorTests
    {
        private readonly Random _random = new Random();

        [TestMethod]
        public void FloatAdditionCalculatorTest()
        {
            TestAdditionCalculatorWithValues(10f, 5);
            TestAdditionCalculatorWithValues(0, 0);
            TestAdditionCalculatorWithValues(15);
            TestAdditionCalculatorWithValues(-20,10);
            TestAdditionCalculatorWithValues(float.MaxValue, float.MinValue);
            
            RunAdditionTestsWithRandomValues(100);
        }

        private void RunAdditionTestsWithRandomValues(int testsCount)
        {
            for (int i = 0; i < testsCount; i++)
            {
                int modifiersCount = _random.Next(0, 4);

                float[] modifierValues = new float[modifiersCount];
                if (modifiersCount > 0)
                {
                    for (int j = 0; j < modifierValues.Length; j++)
                    {
                        modifierValues[j] = GenerateRandomValue(1000);
                    }
                }

                float baseValue = GenerateRandomValue(1000);
                TestAdditionCalculatorWithValues(baseValue, modifierValues);
            }
        }

        private float GenerateRandomValue(int range)
        {
            double value = range * (_random.NextDouble() - 0.5f);
            return (float)value;
        }

        private void TestAdditionCalculatorWithValues(float baseValue, params float[] modifierValues)
        {
            var calculator = new FloatAdditionCalculator();
            var parameter = new FloatParameter(baseValue, calculator) {RecalculateOnChangeModifiers = true};
            float expectedValue = baseValue;
            foreach (var modifierValue in modifierValues)
            {
                var modifier = new FloatModifier(modifierValue);
                parameter.AddModifier(modifier);
                expectedValue += modifierValue;
            }
            Assert.AreEqual(expectedValue, parameter.CurrentValue, 0.01f);
        }
    }
}
