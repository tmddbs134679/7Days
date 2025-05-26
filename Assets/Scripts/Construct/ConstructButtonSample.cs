using UnityEngine;
using UnityEngine.UI;

public class ConstructButtonSample : MonoBehaviour
{
    Button constructButton;

    private void Awake()
    {
        constructButton.onClick.AddListener(() => TryConstruct());
    }

    public void TryConstruct()
    {
        // 각 버튼에 건축해야 할 것들에 대한 데이터가 들어가야 함 >> 해당 데이터를 묶어줄 클래스 필요
        // 필요한 데이터
        // 1. 건축물 프리펩
        // 2. 지을 때 필요한 자원 >> 건축 가능한지 체크 (자원 맡은 팀원님과 소통 필요)
    }
}
