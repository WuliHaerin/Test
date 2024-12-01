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

    //��ʼ��
    public void Init(Item config)
    {
        m_Item = config;
        imageComp.sprite = Resources.Load<Sprite>(item.iconPath);
        countText.text = m_Item.count.ToString();
    }
    //ˢ��Count
    public void RefreshCount()
    {
        countText.text = m_Item.count.ToString();
    }
    //�϶�ʱ������Ʒ����Ϣ
    public void ClearInfo()
    {
        imageComp.color = Color.clear;
        countText.text = "";
    }
    //�϶������ָ���Ʒ����Ϣ
    public void ResetUI()
    {
        imageComp.color = Color.white;
        countText.text = m_Item.count.ToString();
    }

    public void HideText()
    {
        
    }
}
