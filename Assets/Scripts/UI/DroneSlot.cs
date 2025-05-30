using System;
using TMPro;
using UnityEngine;

public class DroneSlot : MonoBehaviour
{
    private DroneUI droneUI;
    private int slotIdx;

    [SerializeField] TMP_Dropdown dropdown;
    public static Action<int> onWorkCompleted;

    public void Init(DroneUI droneUI, int slotIdx)
    {
        dropdown = GetComponentInChildren<TMP_Dropdown>();

        this.droneUI = droneUI;
        this.slotIdx = slotIdx;

        if (dropdown != null)
        {
            dropdown.onValueChanged.AddListener(CommandToDrone);
        }

        onWorkCompleted += OnInteractable;
    }

    void CommandToDrone(int mode)
    {
        droneUI.CommandToDrone(slotIdx, mode);
        dropdown.interactable = false;
    }

    public void OnInteractable(int idx)
    {
        if (slotIdx == idx)
        {
            dropdown.value = 0;
            dropdown.interactable = true;
        }
    }
}
