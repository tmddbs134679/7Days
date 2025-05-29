using UnityEngine;

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
                    int keyInt = key - KeyCode.Alpha1; // 1번부터 시작되게끔
                    if ((keyInt >= 0 && keyInt <= 9) && keyInt < buildingLength) // 1~9, 배열의 최대 크기를 넘지 않게
                    {
                        StartConstruct(keyInt);
                    }
                }
            }
        }
    }

    public void StartConstruct(int index)
    {
        buildManager.SetBuilding(buildings[index]);
        buildManager.gameObject.SetActive(true);
    }
}
