using System.Collections;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    Player player;
    PlayerDataSO playerDataSO;

    private float moveSpeed;
    public float MoveSpeed { get; private set; }
    
    // 현재 스탯들
    [Header("Player Current Stats")]
    [SerializeField] private float curHealth;
    [SerializeField] private float curStamina;
    [SerializeField] private float curHunger;
    [SerializeField] private float curHydration;

    public float CurHealth
    {
        get => curHealth;
        set => curHealth = Mathf.Clamp(value, 0, playerDataSO.MaxHealth);
    }
    public float CurStamina
    {
        get => curStamina;
        set => curStamina = Mathf.Clamp(value, 0, playerDataSO.MaxStamina);
    }
    public float CurHunger
    {
        get => curHunger;
        set => curHunger = Mathf.Clamp(value, 0, playerDataSO.MaxHunger);
    }
    public float CurHydration
    {
        get => curHydration;
        set => curHydration = Mathf.Clamp(value, 0, playerDataSO.MaxHydration);
    }

    // 스탯 감소량
    private float healthDecayPerInterval;
    private float staminaDecayPerInterval;
    private float hungerDecayPerInterval;
    private float hydrationDecayPerInterval;
    private float decayPerInterval;

    public void Init(Player player)
    {
        this.player = player;
        playerDataSO = player.PlayerDataSO;

        curHealth = playerDataSO.MaxHealth;
        curStamina = playerDataSO.MaxStamina;
        curHunger = playerDataSO.MaxHunger;
        curHydration = playerDataSO.MaxHydration;

        healthDecayPerInterval = playerDataSO.HealthDecayPerInterval;
        staminaDecayPerInterval = playerDataSO.StaminaDecayPerInterval;
        hungerDecayPerInterval = playerDataSO.HungerDecayPerInterval;
        hydrationDecayPerInterval = playerDataSO.HydrationDecayPerInterval;

        decayPerInterval = playerDataSO.DecayPerInterval;

        StartCoroutine(DecayPerIntervalCoroutine());
    }

    IEnumerator DecayPerIntervalCoroutine()
    {
        while (!player.IsDie)
        {

            yield return new WaitForSeconds(decayPerInterval);
        }
    }

    public bool UseStamina(float amount)
    {
        if (curStamina - amount >= 0)
        {
            CurStamina -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
}
