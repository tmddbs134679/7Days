using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager : Singleton<BuildingsManager>
{
    public List<BaseBuilding> buildings = new List<BaseBuilding>();
    public List<BaseBuilding> buildingsNeedConstruct = new List<BaseBuilding>();
}
