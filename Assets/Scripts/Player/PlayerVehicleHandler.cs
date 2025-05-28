using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerVehicleHandler : MonoBehaviour
{
    Player player;
    VehicleController curVehicle;

    public void Init(Player player)
    {
        this.player = player;
    }

    public void SetVehicle(VehicleController vehicle, PlayerInput playerInput)
    {
        if (vehicle != null)
        {
            player.ChangeState(PlayerState.Vehicle);
            curVehicle = vehicle;

            transform.SetParent(vehicle.MountPos);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            vehicle.Init(playerInput);
        }
        else
        {
            player.ChangeState(PlayerState.Idle);
            
            transform.SetParent(null);
            curVehicle.StopControl();

            curVehicle = null;
        }
    }
}
