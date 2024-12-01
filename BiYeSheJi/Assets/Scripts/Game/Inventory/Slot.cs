using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Slot : MonoBehaviour
{
    public  abstract ItemUI m_ItemUI { get; set; }
    //�洢��Ʒ
    public virtual Item StoreItem(Item item)
    {
        GameObject itemObj = ResMgr.instance.LoadAsset("Prefabs/UI/ItemUI", transform, true);
        m_ItemUI = itemObj.GetComponent<ItemUI>();
        m_ItemUI.Init(item.DeepClone());
        return null;
    }
    //�жϲ����Ƿ�����Ʒ
    public bool HasItem()
    {
        return transform.GetComponentInChildren<ItemUI>() != null;
    }

    //��ȡ������Ʒ��id
    public int GetChildID()
    {
        return m_ItemUI.item.id;
    }
    //���ò������Ʒ��Ϣ
    public virtual void SetItem(Item item)
    {
        m_ItemUI.Init(item);
        m_ItemUI.ResetUI();
    }
    //������Ʒ������
    public void IncreaseItemCount(int num)
    {
        m_ItemUI.item.count += num;
        m_ItemUI.RefreshCount();
    }
}
