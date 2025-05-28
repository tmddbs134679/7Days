using UnityEngine;

public class Refinery : BaseBuilding<BuildingData<ProductionBuildingData>>
{
    [SerializeField] InventoryManager inventoryManager;
    // 제작 진행 시간, 제작에 필요한 시간
    float progressTime, requireTime;
    // 생산된 양
    int productAmount;

    protected override void Start()
    {
        base.Start();
        // 업데이트에서 매번 찾기보다 변수에 담아두는 게 좋지 않을까
        requireTime = data.buildingDatas[level].productionTime;
    }

    private void Update()
    {
        // 시간을 세어주다, 생산 필요 시간을 넘었다면 생산!
        progressTime += Time.deltaTime;
        if(progressTime > requireTime)
        {
            progressTime = 0; // 시간 초기화
            Production(); // 생산
        }
    }

    protected override void Init()
    {
        // 데이터 받아오기
        data = FormManager.Instance.GetForm<ProductionBuildingForm>().GetDataByID((int)buildingIndex);
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
        // 레벨업으로 변한 생산 시간 반영
        requireTime = data.buildingDatas[level].productionTime;
    }

    // 생산량만큼 적재
    void Production() => productAmount = Mathf.Clamp(productAmount + data.buildingDatas[level].amount, 0, data.buildingDatas[level].capacity);
    
    // 생산한 아이템을 인벤토리에 넣게끔
    public void OnInteract() => inventoryManager.AddResource(data.buildingDatas[level].product, productAmount);
}
