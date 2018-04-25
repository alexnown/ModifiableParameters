using ModifiableParameters.Parameters;

namespace ModifiableParameters.Limiters
{
    /// <summary> Задает максимальное значение для CurrentValue параметра. </summary>
    public class FloatMaxValueLimiter : AParameterLimiter<float>
    {
        public float MaxValue;

        public FloatMaxValueLimiter(float maxValue)
        {
            MaxValue = maxValue;
        }
        
        public override bool IsMeetLimit(IParameter<float> parameter, ref float correctedCurrValue)
        {
            if (correctedCurrValue <= MaxValue) return true;
            correctedCurrValue = MaxValue;
            return false;
        }
    }
}
