using System.Collections;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private WeaponDataSO weaponDataSO;
    private Rigidbody _rigidbody;

    [SerializeField] GameObject buffEffect;
    private bool isExplosion = false;

    public void Init(WeaponDataSO weaponDataSO, Vector3 direction, float force)
    {
        this.weaponDataSO = weaponDataSO;
        _rigidbody = GetComponent<Rigidbody>();

        Throw(direction, force);
    }

    void Throw(Vector3 direction, float force)
    {
        _rigidbody.AddForce(direction * force, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isExplosion)
        {
            isExplosion = true;
            StartCoroutine(GrenadeEffectCoroutine());
        }
    }

    IEnumerator GrenadeEffectCoroutine()
    {
        yield return new WaitForSeconds(weaponDataSO.explosionDelay);

        //Instantiate(buffEffect, transform.position, Quaternion.identity);

        Collider[] hits = Physics.OverlapSphere(transform.position, weaponDataSO.range);
        foreach (var hit in hits)
        {
            ApplyEffect(hit);
        }

        yield return new WaitForSeconds(weaponDataSO.duration);

        foreach (var hit in hits)
        {
            RemoveEffect(hit);
        }

        Destroy(gameObject);
    }

    void ApplyEffect(Collider col)
    {
        switch (weaponDataSO.weaponType)
        {
            case WeaponType.Buff:
                if (col.TryGetComponent(out Turret turret))
                {
                    // 터렛 공격속도 강화 효과
                }
                break;

            case WeaponType.Debuff:
                if (col.TryGetComponent(out AI_Base enemy))
                {
                    // 적 이동속도 감소 효과
                }
                break;
        }
    }

    void RemoveEffect(Collider col)
    {
        switch (weaponDataSO.weaponType)
        {
            case WeaponType.Buff:
                if (col.TryGetComponent(out Turret turret))
                {
                    // 터렛 강화 효과 제거
                }
                break;

            case WeaponType.Debuff:
                if (col.TryGetComponent(out AI_Base enemy))
                {
                    // 적 감소 효과 제거
                }
                break;
        }
    }

}
