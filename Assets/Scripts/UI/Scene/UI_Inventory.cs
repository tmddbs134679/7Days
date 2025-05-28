using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : UI_Popup
{
   public InventoryManager inventoryManager;

    [SerializeField] private int slotCount;

    private GameObject ItemSlotLayout;
    private List<UI_ItemSlot> slots = new List<UI_ItemSlot>();
    /*
    //Detail 파트
    private ItemData _curItemData;
    private int _curIndex;

    private TextMeshProUGUI _detailNameTxt;
    private TextMeshProUGUI _detailDescritionTxt;
    private Button _consumBtn;
    private Button _equipBtn;
    private Button _unequipBtn;
    */

    enum Grid
    {
        ItemSlotLayout,
    }
    enum TMPs
    {
        Name,
        Description
    }
    enum Buttons
    {
        UseConsumable,
        Equip,
        Unequip,
    }
    private void Awake()
    {
        inventoryManager = T_PC.instance.inventoryManager;
    }
    public override void Init()
    {
        base.Init();

        Bind<GridLayoutGroup>(typeof(Grid));
      //  Bind<TextMeshProUGUI>(typeof(TMPs));
       // Bind<Button>(typeof(Buttons));

        ItemSlotLayout = Get<GridLayoutGroup>((int)Grid.ItemSlotLayout).gameObject;
        SetSlot();
        /*
        _detailNameTxt = Get<TextMeshProUGUI>((int)TMPs.Name);
        _detailDescritionTxt = Get<TextMeshProUGUI>((int)TMPs.Description);
        _consumBtn = Get<Button>((int)Buttons.UseConsumable);
        _equipBtn = Get<Button>((int)Buttons.Equip);
        _unequipBtn = Get<Button>((int)Buttons.Unequip);


        //함수 연결
        _consumBtn.onClick.AddListener(ConsumItem);
        _equipBtn.onClick.AddListener(EquipItem);
        _unequipBtn.onClick.AddListener(UnequipItem);
        */

        //초기화 작업
        CloseAllBtn();

        for (int i = 0; i < slotCount; i++)
        {/*
            var go = Instantiate(_slotPrefab);
            itemSlots.Add(go.GetComponent<ItemSlot>());
            itemSlots[i].uI_Inventory = this;
            itemSlots[i].Idx = i;
            go.transform.SetParent(_slots.transform, false);
            */
        }
    }
    private void SetSlot()
    {
        GameObject prefab = Resources.Load<GameObject>($"Prefabs/UI/Popup/UI_ItemSlot");
        // 슬롯생성
        for (int i = 0; i < slotCount; i++)
        {
            var go = Instantiate(prefab);
            slots.Add(go.GetComponent<UI_ItemSlot>());
            slots[i].inventory = this;
            slots[i].index = i;
            slots[i].ResetSlot();
            go.transform.SetParent(ItemSlotLayout.transform, false);
        }

        // 인벤토리에서 슬롯 반영
        var curSlots =  inventoryManager.itemSlots;
        for (int i = 0; i < slotCount; i++)
        {
            if (curSlots[i] != null)
            {
                slots[i].Item = curSlots[i].data;
                slots[i].Stack = curSlots[i].count;
                slots[i].UpdateIcon(curSlots[i].data.icon);
                slots[i].UpdateTMP();
            }
        }

    }

    private void OnDisable()
    {
      //  _interaction.OnAddItem -= AddItem;
    }

    // stack과 빈칸을 확인해서 생성 가능한지 확인하는 함수.


    public void ShowDetail(int index)
    {/*
        var slot = itemSlots[_curIndex];
        _curIndex = index;
        _curItemData = itemSlots[_curIndex].Item;
        _detailNameTxt.text = _curItemData.DisplayName;
        _detailDescritionTxt.text = _curItemData.Descrition;

        CloseAllBtn();

        switch (_curItemData.Type)
        {
            case ItemType.Consumable:
                _consumBtn.gameObject.SetActive(true);
                break;
            case ItemType.Equipable:
                if (slot.isEquiped)
                {
                    _unequipBtn.gameObject.SetActive(true);
                    _equipBtn.gameObject.SetActive(false);
                }
                else
                {
                    _unequipBtn.gameObject.SetActive(false);
                    _equipBtn.gameObject.SetActive(true);
                }
                break;
        }
        */
    }



    void ResetDetail()
    {
       // _detailNameTxt.text = "";
      //  _detailDescritionTxt.text = "";
        CloseAllBtn();
    }
    void CloseAllBtn()
    {
        //_consumBtn.gameObject.SetActive(false);
       // _equipBtn.gameObject.SetActive(false);
       // _unequipBtn.gameObject.SetActive(false);
    }

    public void OnInventory()
    {
     //   if (_inventory.activeSelf == true)
     //       _inventory.SetActive(false);
     //   else
      //      _inventory.SetActive(true);
    }


}
