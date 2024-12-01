using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Slot : MonoBehaviour
{
    public  abstract ItemUI m_ItemUI { get; set; }
    //存储物品
    public virtual Item StoreItem(Item item)
    {
        GameObject itemObj = ResMgr.instance.LoadAsset("Prefabs/UI/ItemUI", transform, true);
        m_ItemUI = itemObj.GetComponent<ItemUI>();
        m_ItemUI.Init(item.DeepClone());
        return null;
    }
    //判断槽里是否有物品
    public bool HasItem()
    {
        return transform.GetComponentInChildren<ItemUI>() != null;
    }

    //获取槽里物品的id
    public int GetChildID()
    {
        return m_ItemUI.item.id;
    }
    //设置槽里的物品信息
    public virtual void SetItem(Item item)
    {
        m_ItemUI.Init(item);
        m_ItemUI.ResetUI();
    }
    //增加物品的数量
    public void IncreaseItemCount(int num)
    {
        m_ItemUI.item.count += num;
        m_ItemUI.RefreshCount();
    }
}
