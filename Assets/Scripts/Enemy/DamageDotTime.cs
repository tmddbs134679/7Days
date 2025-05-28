using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDotTime : MonoBehaviour
{
    public float damagePerTick = 2f;
    public float tickInterval = 1f;
    public float duration = 5f;

    private BaseBuilding<MonoBehaviour> target;      //Test용

    public void Apply(BaseBuilding<MonoBehaviour> target)
    {
        this.target = target;
        StartCoroutine(DoTickDamage());
    }

    private IEnumerator DoTickDamage()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            if (target == null)
                yield break;

            target.Damage(damagePerTick);
            yield return new WaitForSeconds(tickInterval);
            elapsed += tickInterval;
        }

        Destroy(this); // 끝나면 자기 자신 제거
    }
}
