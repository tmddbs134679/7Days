using System;
using System.Collections.Generic;
using UnityEngine;

public class DroneManagerOffice : BaseBuilding
{
    public WorkerOfficeData data { get; private set; }

    private Dictionary<ItemData, int> resourceStorage;
    [SerializeField] private DroneHandler droneHandler;

    protected override void Init()
    {
        if (TryGetComponent(out droneHandler))
        {
            droneHandler.Init(this);
        }

        // 데이터 받아오기
        data = FormManager.Instance.GetForm<WorkerOfficeForm>().GetDataByID((int)buildingIndex);
        // 최대 레벨
        levelMax = data.dataByLevel.Length - 1;
        // 건설 필요 시간 써주기
        requireTime = data.dataByLevel[0].time;
        SetBuildingStatus();

        resourceStorage = new Dictionary<ItemData, int>();

        DroneManager.DroneBuildings.Add(transform);
    }

    protected override void SetBuildingStatus()
    {
        // 레벨업으로 인한 최대 HP 증가
        hpMax = data.dataByLevel[level].hpMax;

        for (int i = 0; i < data.dataByLevel[level].workerCount; i++)
        {
            GenerateWorker();
        }
    }

    // 건물마다 고유로 가지는 값들 반환
    public override BuildingStatus GetIndividualBuildingInfo() => new WorkerOfficeStatus(level, levelMax, hpMax);

    public override void ResourceConsumption(int nextLevel)
    {
        ResourceRequire[] resourcesRequire = data.dataByLevel[nextLevel].resources;
        foreach (ResourceRequire resourceRequire in resourcesRequire)
        {
            inventoryManager.DeductResource(resourceRequire.resourceSort, resourceRequire.amount);
        }

        // 건설 필요 시간 써주기
        requireTime = data.dataByLevel[nextLevel].time;
        // 건설 상태
        isConstructing = true;
    }

    public void SaveResouce(Dictionary<ItemData, int> gatherResources)
    {
        foreach (var resource in gatherResources.Keys)
        {
            if (resourceStorage.ContainsKey(resource))
            {
                resourceStorage[resource] += gatherResources[resource];
            }
            else
            {
                resourceStorage.Add(resource, gatherResources[resource]);
            }
        }
    }

    public void GetAllResources()
    {
        InventoryManager inventory = InventoryManager.instance;

        foreach (var resource in resourceStorage.Keys)
        {
            inventory.AddItem(resource, resourceStorage[resource]);
        }

        resourceStorage.Clear();
    }

    public void GenerateWorker()
    {
        droneHandler.GenerateDrone(data.WorkerPrefab);
    }

    public void SelectDrone(int idx)
    {
        droneHandler.SelectDrone(idx);
    }
}

public class WorkerOfficeStatus : BuildingStatus
{
    public WorkerOfficeStatus(int level, int levelMax, float hpCurrent) : base(level, levelMax, hpCurrent)
    {
        
    }
}