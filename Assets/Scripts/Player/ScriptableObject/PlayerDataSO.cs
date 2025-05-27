using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData/New Data")]
public class PlayerDataSO : ScriptableObject
{
    [Header("Player Stats")]
    [SerializeField] float maxHealth;
    [SerializeField] float maxStamina;
    [SerializeField] float maxHunger;
    [SerializeField] float maxHydration;
    public float MaxHealth { get => maxHealth; }
    public float MaxStamina { get => maxStamina; }
    public float MaxHunger { get => maxHunger; }
    public float MaxHydration { get => maxHydration; }

    [Header("Player Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;
    [SerializeField] float dashCoolDown;
    [SerializeField] float dashStamina;
    public float MoveSpeed { get => moveSpeed; }
    public float DashSpeed { get => dashSpeed; }
    public float DashDuration { get => dashDuration; }
    public float DashCoolDown { get => dashCoolDown; }
    public float DashStamina { get => dashStamina; }

    [Header("Stat Decay Per Interval")]
    [SerializeField] float decayPerInterval;
    [SerializeField] float healthDecayPerInterval;
    [SerializeField] float staminaDecayPerInterval;
    [SerializeField] float hungerDecayPerInterval;
    [SerializeField] float hydrationDecayPerInterval;

    [Header("Stat Recover Per Interval")]
    [SerializeField] float healthRecoverPerInterval;

    public float DecayPerInterval { get => decayPerInterval; }
    public float HealthDecayPerInterval { get => healthDecayPerInterval; }
    public float StaminaDecayPerInterval { get => staminaDecayPerInterval; }
    public float HungerDecayPerInterval { get => hungerDecayPerInterval; }
    public float HydrationDecayPerInterval { get => hydrationDecayPerInterval; }

    public float HealthRecoverPerInterval { get => healthRecoverPerInterval; }
}
