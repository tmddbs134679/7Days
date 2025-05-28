using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class UI_QuickSlot : UI_Scene
{
    private Image iconImage;
    private Image cooldownOverlay;
    private float cooldownTime = -999;
    private float lastUsedTime = -999;
    public bool isActive = false;
    [SerializeField] private Sprite baseIcon;
    public enum Images
    {
        IconBack,
        Icon,
    }
    private void Awake()
    {
        Bind<Image>(typeof(Images));
        iconImage = Get<Image>((int)Images.IconBack);
        cooldownOverlay = Get<Image>((int)Images.Icon);
    }
    public override void Init()
    {
        base.Init();
    }

    public void SetSlot(Sprite icon, float cooldown)
    {
        this.cooldownTime = cooldown;
        this.lastUsedTime = -cooldown; 
        iconImage.sprite = icon;
        cooldownOverlay.sprite = icon;
        cooldownOverlay.fillAmount = 1f;
    }
    public void SetSlot(ItemData data, float cooldown)
    {
        this.cooldownTime = cooldown;
        this.lastUsedTime = -cooldown;
        iconImage.sprite = data.icon;
        cooldownOverlay.sprite = data.icon;
        cooldownOverlay.fillAmount = 1f;

    }
    public void ClearSlot()
    {
        iconImage.sprite = baseIcon;
        cooldownOverlay.sprite = baseIcon;
        cooldownOverlay.fillAmount = 1f;
    }
    public bool TriggerCooldown()
    {
        if (!isActive && iconImage.sprite != baseIcon)
        {
            isActive = true;
            cooldownOverlay.fillAmount = 0f;
            lastUsedTime = Time.time;
            return true;
        }

        return false;
    }

    private void Update()
    {
        if (isActive)
        {
            float elapsed = Time.time - lastUsedTime;
            float ratio = Mathf.Clamp01(elapsed / cooldownTime);
            cooldownOverlay.fillAmount = ratio;

            if (elapsed >= cooldownTime)
            {
                isActive = false;
                cooldownOverlay.fillAmount = 1f;
                lastUsedTime = -999;
            }
        }
    }
}
