using System.Collections;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    Player player;
    PlayerDataSO playerDataSO;
    private float curHealth;
    private float curStamina;
    private float curHunger;
    private float curThirst;

    public void Init(Player player)
    {
        this.player = player;
        playerDataSO = player.PlayerDataSO;

        curHealth = playerDataSO.MaxHealth;
        curStamina = playerDataSO.MaxStamina;
        curHunger = playerDataSO.MaxHunger;
        curThirst = playerDataSO.MaxThirst;

        StartCoroutine(DecayPerIntervalCoroutine());
    }

    IEnumerator DecayPerIntervalCoroutine()
    {
        if (playerDataSO == null) yield break;

        while (!player.IsDie)
        {
            
            yield return new WaitForSeconds(playerDataSO.DecayPerInterval);
        }
    }
}
