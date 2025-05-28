using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] WeaponDataSO weaponDataSO;
    public WeaponDataSO WeaponDataSO { get => weaponDataSO; }
    [SerializeField] GameObject grenadePrefab;
    Transform throwPoint;
    private bool isCoolDown;
    public bool IsCoolDown { get => isCoolDown; }

    public void Init(Transform throwPoint)
    {
        isCoolDown = false;
        this.throwPoint = throwPoint;
    }

    public void ThrowWeapon(Vector3 direction, float force)
    {
        if (!isCoolDown)
        {
            GameObject obj = Instantiate(grenadePrefab, throwPoint.position, Quaternion.identity);

            if (obj.TryGetComponent(out Grenade grenade))
            {
                grenade.Init(weaponDataSO, direction, force);
                StartCoroutine(CoolDownCoroutine());
            }
        }
    }

    private IEnumerator CoolDownCoroutine()
    {
        isCoolDown = true;

        yield return new WaitForSeconds(weaponDataSO.cooldown);

        isCoolDown = false;
    }

}
