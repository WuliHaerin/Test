using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipSlot : Slot, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public override ItemUI m_ItemUI { get; set; }
    public ItemType itemType;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override Item StoreItem(Item item)
    {
        if(item.itemType==itemType)
        {
            base.StoreItem(item);
            return null;
        }
        return item;
    }

    public override void SetItem(Item item)
    {
        if(item.itemType==itemType)
        {
            base.SetItem(item);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SlotMgr.instance.SetCurSlot(this);
        if (HasItem())
        {
            string text = m_ItemUI.item.itemDesc;
            //TimerMgr.instance.StartTimer(0.3f, 1, () =>
            //{
            //    Inventory.instance.SetDesc(true, transform.position, text);
            //});
            Inventory.instance.SetDesc(true, transform.position, text);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (HasItem())
        {
            Inventory.instance.SetDesc(false, Vector2.zero);
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (HasItem())
        {
            m_ItemUI.ClearInfo();
            SlotMgr.instance.SetWorkItem(this, m_ItemUI.item, m_ItemUI.itemSprite);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (HasItem())
        {
            SlotMgr.instance.SetWorkItemPos(eventData.position);
        }
        Inventory.instance.SetDesc(false, Vector2.zero);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (HasItem())
        {
            SlotMgr.instance.ChangeItem();
        }
    }

}
