using UnityEngine;
using System.Collections;
using System.Resources;

public class DroneUnit : MonoBehaviour
{
    public enum DroneMode { Gather, Repair }
    public DroneMode mode = DroneMode.Gather;

    public Transform target;
    public float moveSpeed = 3f;
    public float gatherCooldown = 2f;
    public int gatherAmount = 1;
    public float repairAmount = 10f;

    private bool isWorking = false;

    void Start()
    {
        DroneManager.RegisterDrone(transform);
    }

    void OnDestroy()
    {
        DroneManager.UnregisterDrone(transform);
    }

    void Update()
    {
        if (target == null || isWorking) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );

        float dist = Vector3.Distance(transform.position, target.position);
        if (dist < 0.2f)
        {
            if (mode == DroneMode.Gather)
                StartCoroutine(GatherRoutine());
            else if (mode == DroneMode.Repair)
                StartCoroutine(RepairRoutine());
        }
    }

    IEnumerator GatherRoutine()
    {
        isWorking = true;
        ResourceSpot spot = target.GetComponent<ResourceSpot>();
        if (spot != null)
        {
            int amount = spot.Collect(gatherAmount);
            ResourceManager.Instance.AddResource(spot.resourceType, amount);
        }
        yield return new WaitForSeconds(gatherCooldown);
        isWorking = false;
    }

    IEnumerator RepairRoutine()
    {
        isWorking = true;
        RepairableBuilding building = target.GetComponent<RepairableBuilding>();
        if (building != null && building.NeedsRepair)
        {
            building.Repair(repairAmount);
        }
        yield return new WaitForSeconds(gatherCooldown);
        isWorking = false;
    }
}
