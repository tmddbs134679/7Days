using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    private Player player;
    [SerializeField] WeaponController[] weapons;
    [SerializeField] WeaponController curWeapon;

    public void Init(Player player)
    {
        this.player = player;
    }

    public void ChangeWeapon(int idx)
    {
        curWeapon = weapons[idx];
        curWeapon.Init();
    }
}
