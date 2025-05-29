public class SlowTurret : BaseBuilding
{
    public DebuffTurretData data { get; private set; }
    protected override void Init()
    {
        // 데이터 받아오기
        data = FormManager.Instance.GetForm<DebuffTurretForm>().GetDataByID((int)buildingIndex);
        // 최대 레벨
        levelMax = data.dataByLevel.Length - 1;
        // 건설 필요 시간 써주기
        requireTime = data.dataByLevel[0].time;
        SetBuildingStatus();
    }

    protected override void SetBuildingStatus()
    {
        // 레벨업으로 인한 최대 HP 증가
        hpMax = data.dataByLevel[level].hpMax;
    }
    // 건물마다 고유로 가지는 값들 반환
    public override BuildingStatus GetIndividualBuildingInfo() => new DebuffTurretStatus(level, levelMax, hpCurrent);

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
}

public class DebuffTurretStatus : BuildingStatus
{
    public DebuffTurretStatus(int level, int levelMax, float hpCurrent) : base(level, levelMax, hpCurrent)
    {
    }
}