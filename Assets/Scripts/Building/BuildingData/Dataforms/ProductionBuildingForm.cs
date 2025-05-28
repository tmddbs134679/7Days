using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ProductionBuilding", menuName = "BuildingData/Production")]
public class ProductionBuildingForm : BaseBuildingForm<BuildingData<ProductionBuildingData>>
{
    public override void CreateForm()
    {
        base.CreateForm();
        foreach (var data in dataList)
        {
            DataDic[(int)data.ID] = data;
        }
    }
}

// 벽을 상속. 생산하는 특수 기능 추가
[Serializable]
public class ProductionBuildingData : BasicBuildingData
{
    [Header("생산 기능 정보")]
    [Tooltip("생산품")]
    public ItemData product;
    [Tooltip("생산 시간")]
    public float productionTime;
    [Tooltip("한번에 생산하는 양")]
    public int amount;
    [Tooltip("생산 적재 한계치")]
    public int capacity;
}