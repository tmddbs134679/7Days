using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager : Singleton<BuildingsManager>
{
    List<BaseBuilding> buildings = new List<BaseBuilding>();

    public void GetBuildingsInfo()
    {
    }

    public void AddToBuildingGroup(BaseBuilding Newbuilding) => buildings.Add(Newbuilding);

    public void RemoveFromBuildingGroup(BaseBuilding Newbuilding) => buildings.Remove(Newbuilding);
}
