using UnityEngine;
using System.Collections;

public class DroneUnit : MonoBehaviour
{
    public DroneMode droneMode;

    public Transform target;
    public float moveSpeed = 3f;
    public float actionCooldown = 2f;
    public int gatherAmount = 1;
    public float repairAmount = 10f;

    private bool isWorking = false;
    private bool IsGathering => droneMode == DroneMode.Gather;

    public void Init()
    {
        DroneManager.RegisterDrone(transform);
    }

    void OnDestroy()
    {
        DroneManager.UnregisterDrone(transform);
    }

    void Update()
    {

        if (target == null || isWorking || droneMode == DroneMode.Stun) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );

        float dist = Vector3.Distance(transform.position, target.position);
        if (dist < 0.2f)
        {
            if (droneMode == DroneMode.Gather)
                StartCoroutine(GatherRoutine());
            else if (droneMode == DroneMode.Repair) { }
                //StartCoroutine(RepairRoutine());
        }
    }

    IEnumerator GatherRoutine()
    {
        yield return null;
    }

    // IEnumerator RepairRoutine()
    // {
    //     isWorking = true;
    //     RepairableBuilding building = target.GetComponent<RepairableBuilding>();
    //     if (building != null && building.NeedsRepair)
    //     {
    //         building.Repair(repairAmount);
    //     }

    //     yield return new WaitForSeconds(gatherCooldown);
    //     isWorking = false;
    // }

    public void ChangeMode(DroneMode mode)
    {
        droneMode = mode;
    }

    public void ChangeMode(DroneMode mode, Transform target)
    {
        droneMode = mode;
        this.target = target;
    }
}