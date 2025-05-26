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
    public Dictionary<string, ItemInfo> itemList = new Dictionary<string, ItemInfo>();

    public void AddItem(ItemData item, int amount)
    {
        if (itemList.ContainsKey(item.resourceName))
        {
            itemList[item.resourceName].count += amount; // 기존 값에 더하기
        }
        else
        {
            itemList[item.resourceName] = new ItemInfo(item, amount); // 새로 추가
        }

        Debug.Log(itemList[item.resourceName].data.description + itemList[item.resourceName].count);
        for (int i = 0; i < itemList[item.resourceName].data.consumables.Length; i++)
        {
            Debug.Log(itemList[item.resourceName].data.consumables[i].type.ToString() + "\n");
            Debug.Log(itemList[item.resourceName].data.consumables[i].value.ToString() + "\n");
        }
    }
}
