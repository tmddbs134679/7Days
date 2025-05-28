public class SlowTurret : BaseBuilding<BuildingData<DebuffTurretData>>
{
    DebuffTurretData debuffTurretData;
    protected override void Init()
    {
        // 데이터 받아오기
        data = FormManager.Instance.GetForm<DebuffTurretForm>().GetDataByID((int)buildingIndex);
        // 최대 레벨
        levelMax = data.buildingDatas.Length - 1;
        SetBuildingStatus();
    }

    protected override void SetBuildingStatus()
    {
        // 해당 레벨에 맞는 데이터
        var levelData = data.buildingDatas[level];
        // 레벨업으로 인한 최대 HP 증가
        hpMax = levelData.hpMax;
    }
    public override (BasicBuildingData, BuildingStatus) OnClick() => (debuffTurretData, new DebuffTurretStatus(level, levelMax, hpCurrent));
}

public class DebuffTurretStatus : BuildingStatus
{
    public DebuffTurretStatus(int level, int levelMax, float hpCurrent) : base(level, levelMax, hpCurrent)
    {
    }
}