using Unity.VisualScripting;
using UnityEngine;

public class BuildingBluePrint : MonoBehaviour
{
    // 건물 청사진의 충돌 판정을 위해 리지드바디 필요
    Rigidbody rb;
    Collider col;
    MeshRenderer meshRenderer;

    Color originColor;
    readonly float tranparency = 0.5f; // 건물 청사진의 투명도

    // 겹치는지 여부
    public bool IsOverlap {  get; private set; }
    private void OnEnable()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true; // 중력 영향을 받지 않도록

        // 반투명하게 표시
        meshRenderer = GetComponent<MeshRenderer>();
        MatColorChange(tranparency);
        // 반투명한 색 기억
        originColor = meshRenderer.material.color;
        // 콜라이더를 트리거로 설정 >> 트리거 이벤트로 겹친 물체 판정
        col = GetComponent<Collider>();
        if (col)
            col.isTrigger = true;
    }

    // 건물 설치 완료 때 호출하여 청사진 기능 해제 및 제거
    public void WhenBuildComplete()
    {
        // 반투명 해제
        MatColorChange(1);
        // 트리거를 다시 콜라이더로 원복하여 충돌 판정 생기게
        if (col)
            col.isTrigger = false;

        // 설치가 끝났을 때 불필요한 컴포넌트들 제거
        Destroy(rb);
        Destroy(this);
    }

    // 겹치는지 여부 판정 및 색상 변화
    private void OnTriggerEnter(Collider other)
    {
        IsOverlap = true;
        meshRenderer.material.color *= Color.red;
    }

    private void OnTriggerExit(Collider other)
    {
        IsOverlap = false;
        meshRenderer.material.color = originColor;
    }

    // 머티리얼 투명도 변화
    void MatColorChange(float transparency)
    {
        if (meshRenderer)
        {
            Color colorTransparent = meshRenderer.material.color;
            colorTransparent.a = transparency;
            meshRenderer.material.color = colorTransparent;
        }
    }
}
