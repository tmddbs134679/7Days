using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class CallConstruct : MonoBehaviour
{
    [SerializeField] Transform[] buildings;
    [SerializeField] BuildManager buildManager;
    int buildingLength;

    private void Start()
    {
        buildingLength = buildings.Length;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    int keyInt = key - KeyCode.Alpha0;
                    if ((keyInt > 0 && keyInt <= 9) && keyInt < buildingLength)
                    {
                        buildManager.SetBuilding(buildings[keyInt]);
                        buildManager.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
