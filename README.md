# ModifiableParameters
A small class library that allows to apply modifiers and limits to the some base value. 
Contains the implementation of IParameter interface for main value types and can extended.

* <a href="#simpleParameters">Simple Parameters</a>
* <a href="#calculator">Calculator strategy</a>

# <a id="simpleParameters"></a>Simple Parameters
Simple parameters is the container for the main value types. They allow you to set the BaseValue for the parameter. 
You can subscribe to the Recalculated event for receive new CurrentValue when it will change.
```csharp
public IntParameter Health = new IntParameter(100);
public BoolParameter CanMove = new BoolParameter(true);
public FloatParameter MoveSpeed = new FloatParameter(1.5f);

CanMove.Recalculated += CanMove_Recalculated;

private void CanMove_Recalculated(bool newValue)
{
    //stop or begin move, update ui etc.
}
```
Parameter modifiers can be added to a parameter for change its current value. It's useful to temporarily change the character's parameter when apply the effects of spells, buffs or auras.
```csharp
//When the buff is applied, the parameter CurrentValue increases by 0.5
FloatModifier moveSpeedBuff = new FloatModifier(0.5f);
MoveSpeed.AddModifier(moveSpeedBuff);

//When the buff has ended
MoveSpeed.RemoveModifier(moveSpeedBuff);
```

# <a id="calculator"></a>Calculator strategy
By default, parameters use addition strategy (add base value and all modifiers value). 
You can set custom calculator strategy for any parameter. For this you need implement IParameterCalculator<V> interface and calculate current value in CalculateCurrentValue method.
```csharp
public class FloatAdditionCalculator : IParameterCalculator<float>
{
    public float CalculateCurrentValue(IParameter<float> parameter)
    {
        var modifiable = parameter as IModifiableParameter<float>;
        return modifiable.BaseValue + modifiable.GetModifiers().Sum(modifier => modifier.Value);
    }
}
```
You can use ConstantValueCalculator<V> for sets some constant value or ConstantBaseValueCalculator <V> for sets BaseValue as CurrentValue of parameter regardless of parameter properties.
