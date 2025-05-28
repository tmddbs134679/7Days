using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    PlayerStatus playerStatus;
    [SerializeField] WeaponController[] weapons;
    [SerializeField] List<WeaponController> unlockWeapons = new List<WeaponController>();
    [SerializeField] WeaponController curWeapon;
    [SerializeField] Transform throwPoint;
    
    private bool isAiming = false;
    private TrajectoryController trajectoryController;

    public void Init(PlayerStatus playerStatus)
    {
        this.playerStatus = playerStatus;

        UnlockWeapon(0);
        UnlockWeapon(1);

        trajectoryController = GetComponentInChildren<TrajectoryController>();
    
        if (trajectoryController)
            trajectoryController.Init(throwPoint);
    }

    public void UnlockWeapon(int idx)
    {
        GameObject obj = Instantiate(weapons[idx].gameObject, throwPoint);
        obj.GetComponentInChildren<MeshRenderer>().enabled = false;

        WeaponController weapon = obj.GetComponent<WeaponController>();
        weapon.Init(throwPoint);

        unlockWeapons.Add(weapon);
    }

    public void ChangeWeapon(int idx)
    {
        if (curWeapon != null)
        {
            curWeapon.GetComponentInChildren<MeshRenderer>().enabled = false;
        }

        if (idx > -1 && idx < unlockWeapons.Count)
        {
            curWeapon = unlockWeapons[idx];
            curWeapon.GetComponentInChildren<MeshRenderer>().enabled = true;
        }
    }

    public void StartAiming()
    {
        if (curWeapon == null || curWeapon.IsCoolDown) return;

        isAiming = true;
        trajectoryController.Show();
    }

    public void StopAiming()
    {
        isAiming = false;
        trajectoryController.Hide();
    }

    public void CheckThrow()
    {
        if (!isAiming || !playerStatus.UseStamina(curWeapon.WeaponDataSO.useStamina)) return;

        isAiming = false;
        trajectoryController.Hide();

        Vector3 direction = trajectoryController.GetAimDirection(out float force);
        curWeapon.ThrowWeapon(direction, force);
    }
}
