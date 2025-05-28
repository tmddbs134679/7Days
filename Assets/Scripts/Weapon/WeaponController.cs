using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] WeaponDataSO weaponDataSO;
    [SerializeField] SphereCollider sphereCollider;
    [SerializeField] HashSet<GameObject> effectedTarget = new HashSet<GameObject>();
    private bool isCoolDown;

    public virtual void Init()
    {
        if (TryGetComponent(out sphereCollider))
        {
            sphereCollider.radius = weaponDataSO.range / 2;
        }

        effectedTarget.Clear();
        isCoolDown = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (weaponDataSO.weaponType == WeaponType.Buff)
        {
            if (other.TryGetComponent(out Turret turret))
            {
                if (!effectedTarget.Contains(turret.gameObject))
                {
                    // 터렛 강화
                    effectedTarget.Add(turret.gameObject);
                }
            }
        }

        else if (weaponDataSO.weaponType == WeaponType.Debuff)
        {
            if (other.TryGetComponent(out AI_Base enemy))
            {
                if (!effectedTarget.Contains(enemy.gameObject))
                {
                    // 적 이동속도 감소
                    effectedTarget.Add(enemy.gameObject);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (weaponDataSO.weaponType == WeaponType.Buff)
        {
            if (other.TryGetComponent(out Turret turret))
            {
                if (effectedTarget.Contains(turret.gameObject))
                {
                    // 터렛 버프 제거
                    effectedTarget.Remove(turret.gameObject);
                }
            }
        }

        else if (weaponDataSO.weaponType == WeaponType.Debuff)
        {
            if (other.TryGetComponent(out AI_Base enemy))
            {
                if (effectedTarget.Contains(enemy.gameObject))
                {
                    // 적 디버프 제거
                    effectedTarget.Remove(enemy.gameObject);
                }
            }
        }
    }

    public virtual void UseWeapon()
    {
        if (!isCoolDown)
        {
            effectedTarget.Clear();
            StartCoroutine(ApplyWeaponEffectCoroutine());
            StartCoroutine(CoolDownCoroutine());
        }
    }

    private IEnumerator ApplyWeaponEffectCoroutine()
    {
        sphereCollider.enabled = true;

        // 처음 Enable 될 때 Trigger 이벤트 미발생 방지
        Collider[] hits = Physics.OverlapSphere(transform.position, sphereCollider.radius);
        foreach (var hit in hits)
        {
            OnTriggerEnter(hit);
        }

        yield return new WaitForSeconds(weaponDataSO.duration);

        sphereCollider.enabled = false;

        foreach (var target in effectedTarget)
        {
            switch (weaponDataSO.weaponType)
            {
                case WeaponType.Buff:
                    // 버프 수동 해제
                    break;

                case WeaponType.Debuff:
                    // 디버프 수동 해제
                    break;
            }
        }
    }

    protected virtual IEnumerator CoolDownCoroutine()
    {
        isCoolDown = true;

        yield return new WaitForSeconds(weaponDataSO.cooldown);

        isCoolDown = false;
    }

}
