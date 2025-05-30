using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;
    public Dictionary<ResourceType, int> resourceDict = new();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        foreach (ResourceType type in System.Enum.GetValues(typeof(ResourceType)))
        {
            resourceDict[type] = 0;
        }
    }

    public void AddResource(ResourceType type, int amount)
    {
        if (!resourceDict.ContainsKey(type))
        {
            resourceDict[type] = amount;
        }
        else
        {
            resourceDict[type] += amount;
        }
    }

    public int GetResource(ResourceType type)
    {
        return resourceDict.ContainsKey(type) ? resourceDict[type] : 0;
    }
}

public enum ResourceType
{
    Scrap,        // 고철
    Circuit,      // 회로 조각
    FuelCell,     // 연료셀
    DirtyWater,   // 오염된 물
    CleanWater,   // 정제수
    CannedFood,   // 통조림
    DryRation,    // 건조 식량팩
    MemoryCore    // 기억 코어
}