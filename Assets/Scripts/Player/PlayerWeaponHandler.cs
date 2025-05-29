using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    PlayerStatus playerStatus;
    [SerializeField] public WeaponController[] weapons; // 나중에 Resources 에서 가져오는 걸로
    [SerializeField] List<WeaponController> unlockWeapons = new List<WeaponController>();
    [SerializeField] WeaponController curWeapon;
    [SerializeField] Transform throwPoint;
    private  PlayerEventHandler playerEvents;
    private int curWeaponIdx = -1;
    private bool isAiming = false;
    private TrajectoryController trajectoryController;

    public void Init(Player player, PlayerStatus playerStatus)
    {
        this.playerStatus = playerStatus;
        playerEvents = player.PlayerEvents;
        UnlockWeapon(0);
        UnlockWeapon(1);

        trajectoryController = GetComponentInChildren<TrajectoryController>();

        if (trajectoryController)
            trajectoryController.Init(throwPoint);
    }

    public void UnlockWeapon(int idx)
    {
        GameObject obj = Instantiate(weapons[idx].gameObject, throwPoint);

        WeaponController weapon = obj.GetComponent<WeaponController>();
        weapon.ShowModel(false);
        weapon.Init(throwPoint);

        unlockWeapons.Add(weapon);
    }

    public void ChangeWeapon(int idx)
    {
        if (curWeapon != null)
        {
            curWeapon.ShowModel(false);
        }

        if (idx > -1 && idx < unlockWeapons.Count)
        {
            curWeapon = unlockWeapons[idx];
            curWeaponIdx = idx;
            curWeapon.ShowModel(true);
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

        switch (curWeaponIdx)
        {
            case 0:
                playerEvents.RaisedSkillA();
                break;
            case 1:
                playerEvents.RaisedSkillB();
                break;
        }

        Vector3 direction = trajectoryController.GetAimDirectionForce(out float force);
        curWeapon.ThrowWeapon(direction, force);
    }
}