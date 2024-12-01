using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMgr : SingleTonMono<SlotMgr>
{
    private Transform m_BagSlotTrans;
    private Transform m_EquipSlotTrans;
    private BagSlot[] m_BagSlotList;
    public BagSlot[] bagSlotList
    {
        get
        {
            if(m_BagSlotList==null)
            {
                m_BagSlotList =m_BagSlotTrans.GetComponentsInChildren<BagSlot>();
            }
            return m_BagSlotList;
        }
        set
        {
            m_BagSlotList = value;
        }
    }
    private EquipSlot[] m_EquipSlotList;
    public EquipSlot[] equipSlotList
    {     
        get
        {
            if (m_EquipSlotList == null)
            {
                m_EquipSlotList = m_EquipSlotTrans.GetComponentsInChildren<EquipSlot>();
            }
            return m_EquipSlotList;
        }
        set
        {
            m_EquipSlotList = value;
        }
    }
    private GameObject workItem; //交换物品的中间物品
    private Item m_TempItem; //中间物品的信息
    private Slot m_CurSlot;  //当前槽
    private Slot m_OriSlot;  //被拖动的槽

    public override void Awake()
    {
        m_BagSlotTrans = transform.Find("BagSlots");
        m_EquipSlotTrans = transform.Find("EquipSlots");
        for (int i = 0; i < m_BagSlotTrans.childCount; i++)
        {
            m_BagSlotTrans.GetChild(i).gameObject.AddComponent<BagSlot>();
        }
        for (int i = 0; i < m_EquipSlotTrans.childCount; i++)
        {
            m_EquipSlotTrans.GetChild(i).gameObject.AddComponent<EquipSlot>();
        }
        InitEquipSlot();
        Item item = TableManager.gunTable.GetDataByID(2001);
        m_EquipSlotList[0].StoreItem(item);
    }
    private void Start()
    {

    }
    //设置中间物品的值
    public void SetWorkItem(Slot oriSlot, Item tempItem,Sprite itemSprite)
    {
        m_OriSlot = oriSlot;
        m_TempItem =tempItem;
        workItem = Inventory.instance.tempImage.gameObject;
        workItem.SetActive(true);
        workItem.GetComponent<Image>().sprite = itemSprite;
    }
    //设置当前鼠标指定的槽
    public void SetCurSlot(Slot slot)
    {
        m_CurSlot = slot;
    }
    //设置中间物品的位置
    public void SetWorkItemPos(Vector2 pos)
    {
        workItem.transform.position = pos;
    }
    //交换两个槽的物品
    public void ChangeItem()
    {
        if (m_CurSlot.HasItem())
        {
            //交换两槽的Item
            m_OriSlot.SetItem(m_CurSlot.m_ItemUI.item);
            m_CurSlot.SetItem(m_TempItem);
        }
        //如果为空槽
        else if(!m_CurSlot.HasItem())
        {
            Item item=m_CurSlot.StoreItem(m_TempItem);
            if(item!=null)
            {
                m_OriSlot.SetItem(m_TempItem);
            }
            else
            {
                Destroy(m_OriSlot.transform.GetChild(0).gameObject);
                m_OriSlot.m_ItemUI = null;
            }
        }
        //如果没有槽
        else if(m_CurSlot==null)
        {
            m_OriSlot.SetItem(m_TempItem) ;
        }
        workItem.SetActive(false);
    }

    public void InitEquipSlot()
    {
        equipSlotList[0].itemType = ItemType.Gun;
        equipSlotList[1].itemType = ItemType.Aromor;
        equipSlotList[2].itemType = ItemType.Potion;
    }

    //获取武器栏
    //获取护甲栏
    //获取药水栏

}
