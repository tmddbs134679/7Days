using System;

public class PlayerEventHandler
{
    public event Action<float> onChangeHealth;
    public event Action<float> onChangeStamina;
    public event Action<float> onChangeHunger;
    public event Action<float> onChangeHydration;
    public void RaisedChangeHealth(float value) => onChangeHealth?.Invoke(value);
    public void RaisedChangeStamina(float value) => onChangeStamina?.Invoke(value);
    public void RaisedChangeHunger(float value) => onChangeHunger?.Invoke(value);
    public void RaisedChangeHydration(float value) => onChangeHydration?.Invoke(value);
}
