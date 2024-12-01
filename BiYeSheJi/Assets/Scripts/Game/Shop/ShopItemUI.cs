using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShopItemUI : MonoBehaviour
{
    private ShopItem m_ShopItem;
    public ShopItem shopItem
    {
        get { return m_ShopItem; }
        set { m_ShopItem = value; }
    }
    [SerializeField]
    private TMP_Text m_CountText;
    [SerializeField]
    private Image imageComp;
    public Sprite itemSprite
    {
        get { return imageComp.sprite; }
        set { imageComp.sprite = value; }
    }
    [SerializeField]
    private TMP_Text m_PriceText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(ShopItem config)
    {
        m_ShopItem = config;
        ItemType itemType = (ItemType)(config.itemID / 1000);
        string iconPath="";
        switch (itemType)
        {
            case ItemType.Gun:
                iconPath = TableManager.gunTable.GetDataByID(config.itemID).iconPath;
                break;
            case ItemType.Aromor:
                iconPath = TableManager.armorTable.GetDataByID(config.itemID).iconPath;
                break;
            case ItemType.Potion:
                iconPath = TableManager.potionTable.GetDataByID(config.itemID).iconPath;
                break;
            default:
                break;
        }
        if (iconPath == "")
            return;
        imageComp.sprite = Resources.Load<Sprite>(iconPath);
        m_CountText.text = config.num.ToString();
        m_PriceText.text= config.price.ToString();
    }


}
