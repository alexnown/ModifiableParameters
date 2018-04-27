using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiableParameters.Calculators;
using ModifiableParameters.Parameters;
using Moq;

namespace TestsModifiableParameters.Parameters
{
    public class ParameterInterfaceTests<V>
    {
        public void RunTests(IParameter<V> parameter, V randomValue)
        {
            SetNullCalculator_CatchArgumentNullException(parameter);
            CallRecalculate_HandleRecalculateEvent(parameter);
            SetCalculator_RecalculateEventAndResult(parameter,randomValue);
        }
        
        public void SetNullCalculator_CatchArgumentNullException(IParameter<V> parameter)
        {
            try
            {
                parameter.Calculator = null;
                Assert.Fail($"Must throw {nameof(ArgumentNullException)} on set null calculator.");
            }
            catch (ArgumentNullException e) { }
        }

        public void CallRecalculate_HandleRecalculateEvent(IParameter<V> parameter)
        {
            bool recalculateEventHanded = false;
            Action<V> recalculateAction = (newValue) => { recalculateEventHanded = true; };
            parameter.ParameterRecalculated += recalculateAction;

            parameter.RecalculateCurentValue();
            Assert.AreEqual(true,recalculateEventHanded);
             
            parameter.ParameterRecalculated -= recalculateAction; 
        }
        

        public void SetCalculator_RecalculateEventAndResult(IParameter<V> parameter, V someValue)
        {
            var prevCalculator = parameter.Calculator;
            
            Mock<AParameterCalculator<V>> calculatorMock = new Mock<AParameterCalculator<V>>();
            
            calculatorMock.Setup(p => p.CalculateCurrentValue(parameter)).Returns(someValue);

            bool recalculateEventHanded = false;
            Action<V> recalculateAction = (newValue) => { recalculateEventHanded = true; };
            parameter.ParameterRecalculated += recalculateAction;

            parameter.Calculator = calculatorMock.Object;
            Assert.AreEqual(true, recalculateEventHanded);
            Assert.AreEqual(someValue,parameter.CurrentValue);
            calculatorMock.Verify(p => p.CalculateCurrentValue(parameter), Times.Once);
            
            parameter.ParameterRecalculated -= recalculateAction;
            parameter.Calculator = prevCalculator;
        }

    }
}
