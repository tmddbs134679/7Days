using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo // 리스트에 추가할 아이템 정보
{
    public ItemData data;
    public int count;

    public ItemInfo(ItemData data, int count)
    {
        this.data = data;
        this.count = count;
    }
}

public class InventoryManager : MonoBehaviour
{

    //기초 자원
    public Dictionary<string, ItemInfo> itemList = new Dictionary<string, ItemInfo>();
    //인벤토리 아이템
    public ItemInfo[] itemSlots;
    [SerializeField] private int slotCount = 20;
    private bool isOpenInventory = false;
    // 이건 인벤토리에 추가되는 녀석들, 자원은 별도로 AddResource 함수를 만들어야함.
    private void Awake()
    {
        itemSlots = new ItemInfo[slotCount];
    }
    public void AddItem(ItemData item, int amount)
    {
        int leftAmount = amount;

        // 기존 스택에 채우기 시도
        for (int i = 0; i < slotCount; i++)
        {
            // 해당 슬롯이 null이 아니고, 같은 아이템이며, 스택에 여유가 있을 때
            if (itemSlots[i] != null && itemSlots[i].data.name == item.name && itemSlots[i].count < item.maxStackAmount)
            {
                int canFill = item.maxStackAmount - itemSlots[i].count; // 현재 슬롯에 채울 수 있는 최대량
                int fillAmount = Math.Min(leftAmount, canFill);         

                itemSlots[i].count += fillAmount;
                leftAmount -= fillAmount;

                if (leftAmount == 0) // 모든 아이템을 다 넣었으면 종료
                {
                    return;
                }
            }
        }
        // 남은 아이템이 있다면 빈 슬롯에 새로 추가
        while (leftAmount > 0)
        {
            int amountToPlace = Math.Min(leftAmount, item.maxStackAmount);
            int emptySlotIndex = FindEmptySlot();

            if (emptySlotIndex != -1) // 빈 슬롯을 찾았다면
            {
                itemSlots[emptySlotIndex] = new ItemInfo(item, amountToPlace);
                leftAmount -= amountToPlace;
            }
            else // 빈 슬롯이 없다면
            {
                Console.WriteLine($"인벤토리가 가득 찼습니다. {item.name} {leftAmount}개가 드롭됩니다.");
                return; // 더 이상 아이템을 넣을 수 없으므로 종료
            }
        }
    }

    // 빈 슬롯의 인덱스를 찾아 반환하는 헬퍼 메서드
    private int FindEmptySlot()
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (itemSlots[i] == null)
            {
                return i;
            }
        }
        return -1; // 빈 슬롯이 없는 경우
    }



public void DeductItem(ItemData[] items, int[] amount)
    {
        for (int i = 0; i < items.Length; i++)
        {
            //itemList[items[i].resourceName].count -= amount[i];
         //   Debug.Log(itemList[items[i].resourceName].data.resourceName + itemList[items[i].resourceName].count);
        }
    }

    public void OnUseItem()
    {

    }
    public void OnInventory()
    {
        if (isOpenInventory)
        {
            isOpenInventory = false;
            UIManager.instance.ClosePopupUI();
        }
        else
        {
            isOpenInventory = true;
            var go =  UIManager.instance.ShowPopupUI("UI_Inventory");
            var ui = go.GetComponent<UI_Inventory>();
            ui.inventoryManager = this;
        }

    }

    // 테스트용 인풋
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnInventory();
        }
    }
}
