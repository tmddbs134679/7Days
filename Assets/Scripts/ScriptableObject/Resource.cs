using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public ItemData item;
    public InventoryManager inventory;
    public GameObject parentObject; // 부모 오브젝트
    public SpawnManager spawn;
    public int minCount;
    public int maxCount;
    public float farmingRate; // 자원 획득까지 걸리는 시간
    public float spawnRate; // 자원 스폰 시간

    void Awake()
    {
        parentObject = transform.parent.gameObject;
        inventory = GameObject.Find("Inventory").GetComponent<InventoryManager>();
        spawn = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    public void GetResource()
    {
        int getCount = Random.Range(minCount, maxCount);
        inventory.AddItem(item, getCount);
        spawn.SpawnResource(parentObject, spawnRate);
        parentObject.SetActive(false);
    }

    IEnumerator DigResource() //  AI 자원 채굴
    {
        int getCount = Random.Range(minCount, maxCount);
        yield return new WaitForSeconds(farmingRate);
        Debug.Log(item.resourceName + " 를 " + getCount + "개 획득");
        parentObject.SetActive(false);
    }

    IEnumerator ResourceSpawn() // 자원 스폰 코루틴
    {
        yield return new WaitForSeconds(spawnRate);
        parentObject.SetActive(true);
    }
}
