using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "WallOrSignalTower", menuName = "Data/WallOrSignalTower")]
public class BaseBuildingForm : ScriptableObject
{
    // 레벨 별 벽 or 신호탑 데이터
    public BasicBuildingData[] builingDatas;
}

[CreateAssetMenu(fileName = "Generator", menuName = "Data/Generator")]
public class GeneratorForm : ScriptableObject
{
    // 레벨 별 발전기 데이터
    public GeneratorData[] generatorDatas;
}

[CreateAssetMenu(fileName = "ProductionBuilding", menuName = "Data/ProductionBuilding")]
public class ProductionBuildingForm : ScriptableObject
{
    // 자원 생산 건물 데이터
    public ProductionBuildingData[] productionBuildingDatas;
}

[CreateAssetMenu(fileName = "Turret", menuName = "Data/Turret")]
public class TurretForm : ScriptableObject
{
    // 레벨 별 터렛 데이터
    public TurretData[] turretDatas;
}

[CreateAssetMenu(fileName = "DebuffTurret", menuName = "Data/DebuffTurret")]
public class DebuffTurretForm : ScriptableObject
{
    // 레벨 별 디버프 터렛 데이터
    public DebuffTurretData[] debuffTurretDatas;
}

// 제일 기본적인 건물의 속성들을 지닌 클래스 = 벽, 신호탑(넥서스)
[Serializable]
public class BasicBuildingData
{
    public GameObject Prefeab; // 건물 프리펩
    public float time; // 건설 시간
    public int hpMax; // 체력
    public ResourceRequire[] resources; // 건설에 필요한 자원들 종류 및 갯수
}

[Serializable]
public class ResourceRequire
{
    [Header("Require Resource")]
    // !!! 머지 이후, 자원 통합 클래스를 가져와서 선언 >> 자원 종류
    public int amount; // 갯수
}

// 발전기 주변에 출력이 닿는 범위 안의 건물만 동작(스타 파일런과 비슷)
[Serializable]
public class GeneratorData : BasicBuildingData
{
    [Header("Generator Info")]
    public float activeRange; // 발전기의 출력이 미치는 범위
}

// 벽을 상속. 생산하는 특수 기능 추가
[Serializable]
public class ProductionBuildingData : BasicBuildingData
{
    [Header("Production Info")]
    public UnityEvent Production; // 생산 기능
}

// 벽을 상속. 생산하는 특수 기능 추가
[Serializable]
public class WorkerManageOfficeData : BasicBuildingData
{
    [Header("Worker Info")]
    public GameObject WorkerPrefab;
    public int workerCount;
}

// 벽을 상속, 공격 요소를 추가한 터렛
[Serializable]
public class TurretData : BasicBuildingData
{
    // 타워에 필요한 요소인 공격력, 공격 딜레이, 사거리 추가
    [Header("Turret Info")]
    public float atk;
    public float atkDelay;
    public float range;
}

// 터렛을 상속, 디버프 요소를 추가한 디버프 터렛
[Serializable]
public class DebuffTurretData : TurretData
{
    [Header("Debuff Info")]
    // !!! 머지 이후, 디버프 종류를 나타내는 것 추가
    public float debuffTime; // 디버프 지속 시간
}

