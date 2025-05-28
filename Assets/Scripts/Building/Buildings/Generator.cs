using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class Generator : BaseBuilding<BuildingData<GeneratorData>>
{
    SphereCollider col;
    GeneratorData generatorData;
    private void Awake()
    {
        // 구형 트리거 >> 상호작용 영역으로 사용
        if(TryGetComponent(out col))
            col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {

    }

    protected override void Init()
    {
        // 데이터 받아오기
        data = FormManager.Instance.GetForm<GeneratorForm>().GetDataByID((int)buildingIndex);
        // 최대 레벨
        levelMax = data.buildingDatas.Length - 1;
        SetBuildingStatus();
    }

    protected override void SetBuildingStatus()
    {
        // 해당 레벨에 맞는 데이터
        generatorData = data.buildingDatas[level];
        // 레벨업으로 인한 최대 HP 증가
        hpMax = generatorData.hpMax;
        // 발전기 작동 범위에 따라 트리거 반지름 변경
        col.radius = generatorData.activeRange;
    }

    public override (BasicBuildingData, BuildingStatus) OnClick() => (generatorData, new GeneratorStatus(level, levelMax, hpCurrent));
}

public class GeneratorStatus : BuildingStatus
{
    public GeneratorStatus(int level, int levelMax, float hpCurrent) : base(level, levelMax, hpCurrent)
    {
    }
}
