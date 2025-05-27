using System;
using UnityEngine;

public enum BuildingIndex
{
    SignalTower,
    Wall,
    Turret,
    SlowTurret,
    Generator,
    DroneManageOffice,
    Refinery,
}

// 건물의 기본형
public abstract class BaseBuilding<T> : MonoBehaviour
{
    protected T data;

    [SerializeField] protected BuildingIndex buildingIndex;

    protected int level = 0, // 건물 레벨
                  levelMax; // 해당 건물 종류의 최대 레벨
    protected float hpCurrent, // 현재 체력
                    hpMax; // 현재 레벨의 최대 체력

    private void Start()
    {
        Init();
    }

    // 건물 처음 생성 때 초기화가 필요한 것 모음
    protected abstract void Init();

    // 레벨업 후 스테이터스 적용
    public virtual void BuildingLvUp()
    {
        if (level < levelMax)
        {
            // 이전 레벨의 최대 HP
            float hpMaxBefore = hpMax;
            // 레벨업
            level++;
            // 레벨업 스테이터스 반영
            SetBuildingStatus();
            // 최대 체력 증가 비율에 맞춰 현재 HP 변화
            hpCurrent *= (hpMax / hpMaxBefore);
        }
    }

    // 레벨에 맞는 스테이터스 부여
    protected abstract void SetBuildingStatus();

    // 대미지를 받아 체력 감소 및 파괴

    public void Damage(float damage)
    {
        hpCurrent = Mathf.Clamp(hpCurrent - damage, 0, hpMax);
        if (hpCurrent <= 0)
            Destroy(gameObject);
    }

    // 수리
    public void Fix(float amount) => hpCurrent = Mathf.Clamp(hpCurrent + amount, 0, hpMax);
}
