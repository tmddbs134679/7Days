using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SplatterEffect : MonoBehaviour
{
    [SerializeField] private Image splatterImage;     // 화면에 표시할 UI 이미지
    [SerializeField] private Sprite splatterSprite;   // 사용할 오염물 sprite

    [SerializeField] private float amplitude = 0.1f;  // 진폭
    [SerializeField] private float frequency = 2f;    // 주기
    private Vector3 initPos;

    private void Start()
    {
        initPos = transform.position;
    }

    private void Update()
    {
        float offsetX = Mathf.Sin(Time.time * frequency) * amplitude;
        float offsetY = Mathf.Cos(Time.time * frequency * 0.5f) * amplitude;
        transform.localPosition = initPos + new Vector3(offsetX, offsetY, 0f);
    }

    public void ShowSplatter()
    {
        splatterImage.sprite = splatterSprite;
        StartCoroutine(FadeInOut());
    }

    private IEnumerator FadeInOut()
    {
        // Fade in
        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            splatterImage.color = new Color(1f, 1f, 1f, t);
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);

        // Fade out
        for (float t = 1f; t > 0; t -= Time.deltaTime)
        {
            splatterImage.color = new Color(1f, 1f, 1f, t);
            yield return null;
        }

        splatterImage.sprite = null;
    }
}
