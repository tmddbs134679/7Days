using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DroneUnit : MonoBehaviour
{
    private DroneHandler droneHandler;
    private BuildingsManager buildingsManager;
    public DroneMode droneMode;

    private Dictionary<ItemData, int> gatherResources;
    private Vector3 initPosition;
    public Vector3 target;
    public float moveSpeed = 3f;
    public float actionCooldown = 2f;
    public int gatherAmount = 1;

    [Header("Repair Settings")]
    [SerializeField] ItemData repairResource; // 수리에 필요한 자원
    [SerializeField] int costPerRepair = 1;             // 1회 수리당 소모 자원 개수
    [SerializeField] int repairUnitAmount = 20;      // 1회 수리량
    [SerializeField] float repairPerTime = 3f;

    private bool isWorking = false;
    public bool IsWorking { get => isWorking; }
    private bool IsIdle => droneMode == DroneMode.Idle;
    private bool IsGathering => droneMode == DroneMode.Gather;
    private bool IsConstructing => droneMode == DroneMode.Construct;
    private bool IsRepairing => droneMode == DroneMode.Repair;
    private bool IsStunned => droneMode == DroneMode.Stun;

    public void Init(DroneHandler droneHandler)
    {
        this.droneHandler = droneHandler;

        DroneManager.RegisterDrone(transform);
        buildingsManager = BuildingsManager.Instance;
        initPosition = transform.position;
        gatherResources = new Dictionary<ItemData, int>();
    }

    void OnDestroy()
    {
        DroneManager.UnregisterDrone(transform);
    }

    void Update()
    {
        if (IsIdle && Vector3.Distance(transform.position, initPosition) > 0.95f)
        {
            transform.position = Vector3.MoveTowards(transform.position, initPosition, Time.deltaTime * moveSpeed);
        }    
    }

    IEnumerator GatherRoutine()
    {
        if (droneMode != DroneMode.Gather) yield break;

        gatherResources.Clear();

        target = transform.position + new Vector3(UnityEngine.Random.Range(50, 100), 0f, UnityEngine.Random.Range(50, 100));
        isWorking = true;

        while (IsGathering)
        {
            if (gatherResources.Count == 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);

                if (Vector3.Distance(transform.position, target) <= 0.9f)
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
        BaseBuilding building = buildingsManager.GetNeedRepairBuilding();

        while (IsRepairing && building != null)
        {
            target = building.transform.position + Vector3.up * transform.position.y;

            while (Vector3.Distance(transform.position, target) > 0.95f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
                yield return null;
            }

            bool isDone = false;

            StartCoroutine(RepairBuilding(building, () =>
            {
                isDone = true;
            }));

            yield return new WaitUntil(() => isDone);

            building = buildingsManager.GetNeedRepairBuilding();
        }

        droneMode = DroneMode.Idle;
        isWorking = false;
    }

    IEnumerator ConstructRoutine()
    {
        if (droneMode != DroneMode.Construct) yield break;

        isWorking = true;
        BaseBuilding building = buildingsManager.GetNeedConstructBuilding();

        while (IsConstructing && building != null)
        {
            target = building.transform.position + Vector3.up * transform.position.y;


            while (Vector3.Distance(transform.position, target) > 0.95f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
                yield return null;
            }

            bool isDone = false;
            building.StartConstruct(() => { isDone = true; });

            yield return new WaitUntil(() => isDone);

            building = buildingsManager.GetNeedConstructBuilding();

        }

        droneMode = DroneMode.Idle;
        isWorking = false;
    }

    IEnumerator StunRoutine()
    {
        if (droneMode != DroneMode.Stun) yield break;
    }

    public void ChangeMode(DroneMode mode)
    {
        droneMode = mode;

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

            default:
                droneMode = DroneMode.Idle;
                break;
        }
    }

    IEnumerator RepairBuilding(BaseBuilding building, Action onRepaired)
    {
        if (building == null) yield break;

        while (building.NeedsRepair)
        {
            if (InventoryManager.instance.HasResource(repairResource, costPerRepair))
            {
                building.Fix(repairUnitAmount);
                yield return new WaitForSeconds(repairPerTime);
            }

            else break;
        }

        onRepaired?.Invoke();
    }
}

