public class Wall : BaseBuilding<BuildingData<BasicBuildingData>>
{
    BasicBuildingData basicBuildingData;
    protected override void Init()
    {
        // 데이터 받아오기
        data = FormManager.Instance.GetForm<WallForm>().GetDataByID((int)buildingIndex);
        // 최대 레벨
        levelMax = data.buildingDatas.Length - 1;
        SetBuildingStatus();
    }

    protected override void SetBuildingStatus()
    {
        // 해당 레벨에 맞는 데이터
        basicBuildingData = data.buildingDatas[level];
        // 레벨업으로 인한 최대 HP 증가
        hpMax = basicBuildingData.hpMax;
    }

    public override (BasicBuildingData, BuildingStatus) OnClick() => (basicBuildingData, new BuildingStatus(level, levelMax, hpCurrent));
}

public class BuildingStatus
{
    public int level, // 건물 레벨
               levelMax; // 해당 건물 종류의 최대 레벨
    public float hpCurrent; // 현재 체력

    public BuildingStatus(int level, int levelMax, float hpCurrent)
    {
        this.level = level;
        this.levelMax = levelMax;
        this.hpCurrent = hpCurrent;
    }
}
