using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    PlayerStatus playerStatus;
    [SerializeField] WeaponController[] weapons;
    [SerializeField] WeaponController curWeapon;
    [SerializeField] Transform throwPoint;
    private bool isAiming = false;
    private TrajectoryController trajectoryController;

    public void Init(PlayerStatus playerStatus)
    {
        this.playerStatus = playerStatus;

        ChangeWeapon(0);
        trajectoryController = GetComponentInChildren<TrajectoryController>();
        
        if (trajectoryController)
            trajectoryController.Init(throwPoint);
    }

    public void ChangeWeapon(int idx)
    {
        if (curWeapon != null)
        {
            Destroy(curWeapon.gameObject);
        }

        GameObject obj = Instantiate(weapons[idx].gameObject, throwPoint);

        if (obj.TryGetComponent(out WeaponController weapon))
        {
            curWeapon = weapon;
            curWeapon.Init(throwPoint);
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
