using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum DroneMode
{
    Idle,
    Repair,
    Gather,
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
        GameObject drone = Instantiate(dronePrefab, spawnTransform);

        if (drone.TryGetComponent(out DroneUnit droneUnit))
        {
            droneUnit.Init();
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

        if (mode == DroneMode.Gather)
        {
            selectedDrone.ChangeMode(mode, gatherTransform);
        }
        else
        {
            selectedDrone.ChangeMode(mode);
        }
    }
}
