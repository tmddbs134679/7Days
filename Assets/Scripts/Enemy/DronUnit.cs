using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DroneUnit : MonoBehaviour
{
    private DroneHandler droneHandler;

    public DroneMode droneMode;

    private Dictionary<ItemData, int> gatherResources;
    private Vector3 initPosition;
    public Transform target;
    public float moveSpeed = 3f;
    public float actionCooldown = 2f;
    public int gatherAmount = 1;
    public float repairAmount = 10f;

    private bool isWorking = false;
    public bool IsWorking { get => isWorking; }
    private bool IsGathering => droneMode == DroneMode.Gather;
    private bool IsConstructing => droneMode == DroneMode.Construct;
    private bool IsRepairing => droneMode == DroneMode.Repair;
    private bool IsStunned => droneMode == DroneMode.Stun;

    public void Init(DroneHandler droneHandler)
    {
        this.droneHandler = droneHandler;

        DroneManager.RegisterDrone(transform);
        initPosition = transform.position;
        gatherResources = new Dictionary<ItemData, int>();
    }

    void OnDestroy()
    {
        DroneManager.UnregisterDrone(transform);
    }

    void Update()
    {
        // if (target == null || isWorking || droneMode == DroneMode.Stun) return;


        // transform.position = Vector3.MoveTowards(
        //     transform.position,
        //     target.position,
        //     moveSpeed * Time.deltaTime
        // );

        // float dist = Vector3.Distance(transform.position, target.position);
        // if (dist < 0.2f)
        // {
        //     if (droneMode == DroneMode.Gather)
        //         StartCoroutine(GatherRoutine());
        //     else if (droneMode == DroneMode.Repair)
        //         StartCoroutine(RepairRoutine());
        // }
    }

    IEnumerator GatherRoutine()
    {
        if (droneMode != DroneMode.Gather) yield break;

        gatherResources.Clear();

        isWorking = true;

        while (IsGathering)
        {
            if (gatherResources.Count == 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);

                if (Vector3.Distance(transform.position, target.position) <= 0.9f)
                {
                    gatherResources = ResourceManager.Instance.GetRandomResource();
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, initPosition, Time.deltaTime * moveSpeed);

                if (Vector3.Distance(transform.position, initPosition) <= 0.95f)
                {
                    droneHandler.SaveResouceToStorage(gatherResources);
                    gatherResources.Clear();

                    droneMode = DroneMode.Idle;
                    isWorking = false;
                }
            }

            yield return null;
        }
    }

    IEnumerator RepairRoutine()
    {
        if (droneMode != DroneMode.Repair) yield break;

        isWorking = true;

        while (IsRepairing)
        {
            RepairableBuilding building = target.GetComponent<RepairableBuilding>();
            if (building != null && building.NeedsRepair)
            {
                building.TryRepair();
            }

            yield return new WaitForSeconds(actionCooldown);
        }
        
        isWorking = false;
    }

    IEnumerator ConstructRoutine()
    {
        if (droneMode != DroneMode.Construct) yield break;

        isWorking = true;

        while (IsConstructing)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);

            if (Vector3.Distance(transform.position, target.position) <= 0.95f)
            {
                // 건물 짓기 시작!
                if (target.TryGetComponent(out BaseBuilding building))
                {
                    building.StartConstruct(() =>
                    {
                        
                    });
                }
            }

            yield return null;
        }
    }

    IEnumerator StunRoutine()
    {
        if (droneMode != DroneMode.Stun) yield break;
    }

    public void ChangeMode(DroneMode mode)
    {
        droneMode = mode;
    }

    public void ChangeMode(DroneMode mode, Transform target)
    {
        droneMode = mode;
        this.target = target;

        switch (mode)
        {
            case DroneMode.Repair:
                StartCoroutine(RepairRoutine());
                break;

            case DroneMode.Gather:
                StartCoroutine(GatherRoutine());
                break;

            case DroneMode.Construct:
                StartCoroutine(ConstructRoutine());
                break;

            case DroneMode.Stun:
                StartCoroutine(StunRoutine());
                break;

            default: break;
        }
    }
}

