using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData/New Data")]
public class PlayerDataSO : ScriptableObject
{
    [Header("Player Stats")]
    [SerializeField] float maxHealth;
    [SerializeField] float maxStamina;
    [SerializeField] float maxHunger;
    [SerializeField] float maxThirst;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;

    public float MaxHealth { get => maxHealth; }
    public float MaxStamina { get => maxStamina; }
    public float MaxHunger { get => maxHunger; }
    public float MaxThirst { get => maxThirst; }
    public float WalkSpeed { get => walkSpeed;}
    public float RunSpeed { get => runSpeed; }

    [Header("Stat Decay Per Interval")]
    [SerializeField] float decayPerInterval;
    [SerializeField] float healthDecayPerInterval;
    [SerializeField] float hungerDecayPerInterval;
    [SerializeField] float thirstDecayPerInterval;

    public float DecayPerInterval { get => decayPerInterval; }
    public float HealthDecayPerInterval { get => healthDecayPerInterval; }
    public float HungerDecayPerInterval { get => hungerDecayPerInterval; }
    public float ThirstDecayPerInterval { get => thirstDecayPerInterval; }
}
