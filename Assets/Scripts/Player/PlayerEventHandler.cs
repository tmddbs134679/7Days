using System;

public class PlayerEventHandler
{
    public event Action<float, float> onChangeHealth;
    public event Action<float, float> onChangeStamina;
    public event Action<float, float> onChangeHunger;
    public event Action<float, float> onChangeHydration;
    public void RaisedChangeHealth(float maxValue, float curValue) => onChangeHealth?.Invoke(maxValue, curValue);
    public void RaisedChangeStamina(float maxValue, float curValue) => onChangeStamina?.Invoke(maxValue, curValue);
    public void RaisedChangeHunger(float maxValue, float curValue) => onChangeHunger?.Invoke(maxValue, curValue);
    public void RaisedChangeHydration(float maxValue, float curValue) => onChangeHydration?.Invoke(maxValue, curValue);
}
