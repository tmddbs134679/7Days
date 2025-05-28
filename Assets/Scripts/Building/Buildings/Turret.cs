using UnityEngine;
public class Turret : BaseBuilding<BuildingData<TurretData>>
{
    // 현재 참조중인 터렛 데이터
    public TurretData turretData { get; private set; }

    protected override void Init()
    {
        // 데이터 받아오기
        data = FormManager.Instance.GetForm<TurretForm>().GetDataByID((int)buildingIndex);
        // 최대 레벨
        levelMax = data.buildingDatas.Length - 1;
        SetBuildingStatus();
    }

    protected override void SetBuildingStatus()
    {
        // 해당 레벨에 맞는 데이터
        turretData = data.buildingDatas[level];
        // 레벨업으로 인한 최대 HP 증가
        hpMax = turretData.hpMax;
    }
    public override (BasicBuildingData, BuildingStatus) OnClick() => (turretData, new TurretStatus(level, levelMax, hpCurrent));
}

public class TurretStatus : BuildingStatus
{
    public TurretStatus(int level, int levelMax, float hpCurrent) : base(level, levelMax, hpCurrent)
    {
    }
}
