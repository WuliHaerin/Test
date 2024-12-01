using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class ItemUI : MonoBehaviour
{
    private Item m_Item;
    public Item item
    {
        get { return m_Item; }
        set { m_Item = value; }
    }
    [SerializeField]
    private TMP_Text countText;
    [SerializeField]
    private Image imageComp;
    public Sprite itemSprite
    {
        get { return imageComp.sprite; }
        set { imageComp.sprite = value; }
    }

    //初始化
    public void Init(Item config)
    {
        m_Item = config;
        imageComp.sprite = Resources.Load<Sprite>(item.iconPath);
        countText.text = m_Item.count.ToString();
    }
    //刷新Count
    public void RefreshCount()
    {
        countText.text = m_Item.count.ToString();
    }
    //拖动时隐藏物品槽信息
    public void ClearInfo()
    {
        imageComp.color = Color.clear;
        countText.text = "";
    }
    //拖动结束恢复物品槽信息
    public void ResetUI()
    {
        imageComp.color = Color.white;
        countText.text = m_Item.count.ToString();
    }

    public void HideText()
    {
        
    }
}
