using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class UI_QuickSlotManager : MonoBehaviour
{
    // 대쉬 이미지는 고정
    [SerializeField] private Sprite dashSprite;
    [SerializeField] private UI_QuickSlot[] itemSlots;

    private int dashIndex = 4;

    private void Start()
    {
        T_PC.instance.inventoryManager.quickSlotManager = this;
        SetDashSlot(2.0f); // 추후 변경
    }

    public void SetItemSlot(int index, ItemInfo info, float cooldown = 0)
    {
        itemSlots[index].SetSlot(info.data.icon, cooldown);
    }
    public void SetDashSlot(float cooldown)
    {
        itemSlots[dashIndex].SetSlot(dashSprite, cooldown);
    }

    public void OnSlotPressed(int index)
    {
        itemSlots[index].TriggerCooldown();
    }


    //테스트
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            itemSlots[dashIndex].TriggerCooldown();
        }
    }
}
