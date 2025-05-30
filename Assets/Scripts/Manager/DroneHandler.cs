using System.Collections.Generic;
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

        selectedDrone = activeDrones[idx];
    }

    public void ChangeDroneMode(DroneMode mode)
    {
        if (selectedDrone == null) return;

        selectedDrone.ChangeMode(mode);
    }

    public void SaveResouceToStorage(Dictionary<ItemData, int> gatherResource)
    {
        if (gatherResource != null)
            droneManagerOffice.SaveResouce(gatherResource);
    }
}
