using ModifiableParameters.Parameters;

namespace ModifiableParameters.Limiters
{
    /// <summary> Устанавливает обязательные условия к итоговому параметру после его подсчета.
    /// Дает возможность задавать максимальные пределы или делать проверки на 0 у модификаторов. </summary>
    public abstract class AParameterLimiter <V>
    {
        //Выполняет проверку обязательных пределов. Возвращает false, если требование было нарушено и текущее значение должно быть изменено.
        public abstract bool IsMeetLimit(IParameter<V> parameter, ref V correctedCurrValue);
    }
}
