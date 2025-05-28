using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private WeaponDataSO weaponDataSO;
    private Rigidbody _rigidbody;

    [SerializeField] GameObject buffEffect;
    private bool isExplosion = false;
    [SerializeField] List<Collider> effectedTargets = new List<Collider>(); 

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
        
        // 수류탄 모델 비활성화 후 이펙트 생성
        transform.GetChild(0).gameObject.SetActive(false);
        //Instantiate(buffEffect, transform.position, Quaternion.identity);

        Collider[] targets = Physics.OverlapSphere(transform.position, weaponDataSO.range);
        foreach (var target in targets)
        {
            ApplyEffect(target);
        }

        yield return new WaitForSeconds(weaponDataSO.duration);

        foreach (var target in effectedTargets)
        {
            RemoveEffect(target);
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
                    effectedTargets.Add(col);
                }
                break;

            case WeaponType.Debuff:
                if (col.TryGetComponent(out AI_Base enemy))
                {
                    // 적 이동속도 감소 효과
                    effectedTargets.Add(col);
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
