using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum DroneMode
{
    Idle,
    Repair,
    Gather,
    Construct,
    Stun
}

public class DroneHandler : MonoBehaviour
{
    private DroneManagerOffice droneManagerOffice;
    private List<DroneUnit> activeDrones = new List<DroneUnit>();

    [SerializeField] Transform spawnTransform;
    [SerializeField] Transform gatherTransform;

    DroneUnit selectedDrone;

    public void Init(DroneManagerOffice droneManagerOffice)
    {
        this.droneManagerOffice = droneManagerOffice;
    }

    public void GenerateDrone(GameObject dronePrefab)
    {
        GameObject drone = Instantiate(dronePrefab, spawnTransform.position, Quaternion.identity);

        if (drone.TryGetComponent(out DroneUnit droneUnit))
        {
            droneUnit.Init(this);
            activeDrones.Add(droneUnit);
            DroneManager.AliveDrones.Add(droneUnit.transform);
        }
    }

    public void SelectDrone(int idx)
    {
        if (idx <= -1 || idx >= activeDrones.Count) return;

        if (selectedDrone.droneMode == DroneMode.Idle)
            selectedDrone = activeDrones[idx];
        else
            Debug.Log("해당 드론은 바빠요!");
    }

    public void ChangeDroneMode(DroneMode mode)
    {
        if (selectedDrone == null) return;

        if (mode == DroneMode.Gather)
        {
            selectedDrone.ChangeMode(mode, gatherTransform);
        }
        else
        {
            selectedDrone.ChangeMode(mode);
        }
    }

    public void SaveResouceToStorage(Dictionary<ItemData, int> gatherResource)
    {
        if(gatherResource != null)
            droneManagerOffice.SaveResouce(gatherResource);        
    }


    [ContextMenu("TestSelectDrone")]
    public void TestSelectDrone()
    {
        selectedDrone = activeDrones[0];
        Debug.Log($"선택된 드론: {selectedDrone.name}");
    }

    [ContextMenu("TestGather")]
    public void TestGather()
    {
        selectedDrone.ChangeMode(DroneMode.Gather, gatherTransform);
    }
}
