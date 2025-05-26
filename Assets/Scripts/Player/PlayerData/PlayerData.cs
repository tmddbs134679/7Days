using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData/New Data")]
public class PlayerData : ScriptableObject
{
    [Header("Player Stats")]
    public float maxHealth;
    public float maxStamina;    
    public float maxHunger;
    public float maxThirst;
    public float walkSpeed;
    public float runSpeed;

    [Header("Stat Decay Per Second")]
    public float healthDecayPerSecond;
    public float hungerDecayPerSecond;
    public float thirstDecayPerSecond;
}
