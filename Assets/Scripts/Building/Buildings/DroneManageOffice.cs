using System;
using UnityEngine;

public class DroneManagerOffice : BaseBuilding
{
    public WorkerOfficeData data { get; private set; }

    [SerializeField] private DroneHandler droneHandler;

    protected override void Start()
    {
        base.Start();

        if (TryGetComponent(out droneHandler))
        {
            droneHandler.Init(this);
        }
    }

    protected override void Init()
    {
        // 데이터 받아오기
        data = FormManager.Instance.GetForm<WorkerOfficeForm>().GetDataByID((int)buildingIndex);
        // 최대 레벨
        levelMax = data.dataByLevel.Length - 1;
        // 건설 필요 시간 써주기
        requireTime = data.dataByLevel[0].time;
        SetBuildingStatus();

        DroneManager.DroneBuildings.Add(transform);
    }

    protected override void SetBuildingStatus()
    {
        // 레벨업으로 인한 최대 HP 증가
        hpMax = data.dataByLevel[level].hpMax;
    }

    // 건물마다 고유로 가지는 값들 반환
    public override BuildingStatus GetIndividualBuildingInfo() => new WorkerOfficeStatus(level, levelMax, hpMax);

    public override void ResourceConsumption(int nextLevel)
    {
        // 건설/업그레이드에 필요한 자원이 충분치 않다면 종료
        if (!ResourceCheck(nextLevel))
            return;

        // 건설 필요 시간 써주기
        requireTime = data.dataByLevel[nextLevel].time;
        // 건설 상태
        isConstructing = true;
    }

    // 해당 레벨로의 건설/업그레이드에 필요한 자원이 충분한지 여부
    public override bool ResourceCheck(int nextLevel)
    {
        ResourceRequire[] resourcesRequire = data.dataByLevel[nextLevel].resources;
        foreach (ResourceRequire resourceRequire in resourcesRequire)
        {
            if (!InventoryManager.instance.HasResource(resourceRequire.resourceSort, resourceRequire.amount))
                return false;
        }
        return true;
    }

    /// <summary>
    /// 드론 생성
    /// </summary>
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