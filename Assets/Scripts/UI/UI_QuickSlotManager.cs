using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using static UnityEditor.Progress;

public class UI_QuickSlotManager : MonoBehaviour
{
    private PlayerEventHandler eventHandler;
    private PlayerDataSO playerData;
    private PlayerWeaponHandler weaponHandler;
    // 대쉬 이미지는 고정
    [SerializeField] private Sprite dashSprite;
    [SerializeField] private Sprite skillASprite;
    [SerializeField] private Sprite skillBSprite;
    [SerializeField] private UI_QuickSlot[] itemSlots;

    private int dashIndex = 4;
    private int SkillAIndex = 5;
    private int SkillBIndex = 6;

    private void Start()
    {
        playerData = InventoryManager.instance.player.PlayerDataSO;
        eventHandler= InventoryManager.instance.player.PlayerEvents;
        weaponHandler = InventoryManager.instance.player.playerWeapon;
        InventoryManager.instance.quickSlotManager = this;
        SetSkillSlot(dashIndex, dashSprite, playerData.DashCoolDown);
        SetSkillSlot(SkillAIndex, skillASprite, weaponHandler.weapons[0].weaponDataSO.cooldown);
        SetSkillSlot(SkillBIndex, skillBSprite, weaponHandler.weapons[1].weaponDataSO.cooldown);

        eventHandler.onDash += OnDash;
        eventHandler.onSkillA += OnSkillA;
        eventHandler.onSkillB += OnSkillB;
    }

    public void SetItemSlot(int index, ItemInfo info, float cooldown = 0)
    {
        itemSlots[index].SetSlot(info, cooldown);
    }
    public void ClearItemSlot(int index)
    {
        itemSlots[index].ClearSlot();
    }

    public void SetSkillSlot(int index, Sprite sprite ,float cooldown)
    {
        itemSlots[index].SetSlot(sprite, cooldown);
    }


    public void OnSlotPressed(int index)
    {
        itemSlots[index].TriggerCooldown();
    }
    // 추후 플레이어컨트롤러와 연결해주어야함.
    public void OnDash()
    {
        itemSlots[dashIndex].TriggerCooldown();
    }
    public void OnSkillA()
    {
        itemSlots[SkillAIndex].TriggerCooldown();
    }
    public void OnSkillB()
    {
        itemSlots[SkillBIndex].TriggerCooldown();
    }
    public bool CheckQuick(int index, bool isEnd = false)
    {
         return itemSlots[index].TriggerCooldown(isEnd);
    }
    public void UpdateStack(int index, ItemInfo info)
    {
        itemSlots[index].UpdateStack(info);
    }


}
