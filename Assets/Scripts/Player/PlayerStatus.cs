using System.Collections;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] SurvivalConditionSO conditionSO;
    Player player;
    PlayerDataSO playerDataSO;
    PlayerEventHandler playerEvents;

    #region 플레이어 스탯 관련 변수 & 프로퍼티
    private float moveSpeed;
    public float MoveSpeed { get => moveSpeed; }
    [Header("Player Max Stats")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float maxStamina;

    // 현재 스탯들
    [Header("Player Current Stats")]
    [SerializeField] private float curHealth;
    [SerializeField] private float curStamina;
    [SerializeField] private float curHunger;
    [SerializeField] private float curHydration;

    public float CurHealth
    {
        get => curHealth;
        set
        {
            curHealth = Mathf.Clamp(value, 0, maxHealth);
            playerEvents?.RaisedChangeHealth(maxHealth, curHealth);
        }
    }
    public float CurStamina
    {
        get => curStamina;
        set
        {
            curStamina = Mathf.Clamp(value, 0, maxStamina);
            playerEvents?.RaisedChangeStamina(maxStamina, curStamina);
        }
    }
    public float CurHunger
    {
        get => curHunger;
        set
        {
            curHunger = Mathf.Clamp(value, 0, playerDataSO.MaxHunger);
            playerEvents?.RaisedChangeHunger(playerDataSO.MaxHunger, curHunger);
        }
    }
    public float CurHydration
    {
        get => curHydration;
        set
        {
            curHydration = Mathf.Clamp(value, 0, playerDataSO.MaxHydration);
            playerEvents?.RaisedChangeHydration(playerDataSO.MaxHydration, curHydration);
        }
    }

    // 스탯 감소량
    private float healthDecayPerInterval;
    private float staminaDecayPerInterval;
    private float hungerDecayPerInterval;
    private float hydrationDecayPerInterval;
    private float decayPerInterval;

    private float healthRecoverPerInterval;
    #endregion

    private bool CanHeal => curHunger >= conditionSO.MinConditionToHeal;
    private bool IsDanger => curStamina <= conditionSO.MinStaminaToDecay || curHydration <= conditionSO.MinHydrationToDecay;

    public void Init(Player player)
    {
        this.player = player;
        playerDataSO = player.PlayerDataSO;
        playerEvents = player.PlayerEvents;

        moveSpeed = playerDataSO.MoveSpeed;

        maxHealth = playerDataSO.MaxHealth;
        curHealth = maxHealth;
        maxStamina = playerDataSO.MaxStamina;
        curStamina = maxStamina;

        curHunger = playerDataSO.MaxHunger;
        curHydration = playerDataSO.MaxHydration;

        healthDecayPerInterval = conditionSO.HealthDecayPerInterval;
        staminaDecayPerInterval = conditionSO.StaminaDecayPerInterval;
        hungerDecayPerInterval = conditionSO.HungerDecayPerInterval;
        hydrationDecayPerInterval = conditionSO.HydrationDecayPerInterval;

        healthRecoverPerInterval = conditionSO.HealthRecoverPerInterval;

        decayPerInterval = conditionSO.DecayPerInterval;

        StartCoroutine(DecayPerIntervalCoroutine());
    }

    IEnumerator DecayPerIntervalCoroutine()
    {
        while (!player.IsDead)
        {
            CurHydration -= hydrationDecayPerInterval;
            CurHunger -= hungerDecayPerInterval;

            if (player.CurState == PlayerState.Walk)
                CurStamina -= staminaDecayPerInterval;
        
            if (CanHeal)
                CurHealth += healthRecoverPerInterval;

            if (IsDanger)
            {
                CurHealth -= healthDecayPerInterval;
            }
            
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

    public bool UseHydration(float amount)
    {
        if (curHydration - amount >= 0)
        {
            CurHydration -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
}
