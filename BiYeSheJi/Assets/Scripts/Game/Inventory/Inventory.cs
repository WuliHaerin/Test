using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : UIBase
{
    [SerializeField]
    private Button m_CloseButton;
    //private CanvasGroup m_CanvasGroup;
    [SerializeField]
    private GameObject m_DescObj;
    private TMP_Text m_DescText;
    public Transform tempImage;
    public static Inventory instance;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //m_CanvasGroup = GetComponent<CanvasGroup>();
        m_CloseButton.onClick.AddListener(HideUI);
        m_DescText = m_DescObj.GetComponentInChildren<TMP_Text>();
        tempImage = transform.Find("TempImage");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            ShowUI();
        }
    }

    public override void ShowUI()
    {
        base.ShowUI();
        GameObject playerObj = GameObject.Find("Player");
        playerObj.GetComponent<Player>().enabled = false;
    }

    public override void HideUI()
    {
        base.HideUI();
        GameObject playerObj = GameObject.Find("Player");
        playerObj.GetComponent<Player>().enabled = true;
    }

    //存储物品
    public bool StoreItem(Item item)
    {
        if (item == null)
        {
            Debug.Log("该物品不存在");
            return false;
        }
        else
        {
            BagSlot emptySlot = FindEmptySlot();
            if (item.stackable)
            {
                BagSlot sameItemSlot = FindSameItem(item);                
                if (sameItemSlot != null)
                {
                    sameItemSlot.IncreaseItemCount(item.count);
                    return false;
                }
            }
            if (emptySlot != null)
            {
                emptySlot.StoreItem(item);
            }
            else
            {
                Debug.LogWarning("空间不足");
                return false;
            }

        }
        return true;
    }
    //找到相同物品的槽
    private BagSlot FindSameItem(Item item)
    {
        for (int i = 0; i < SlotMgr.instance.bagSlotList.Length ; i++)
        {
            if (SlotMgr.instance.bagSlotList[i].HasItem() && SlotMgr.instance.bagSlotList[i].GetChildID() == item.id)
            {
                return SlotMgr.instance.bagSlotList[i];
            }
        }
        return null;
    }
    //找到空槽
    private BagSlot FindEmptySlot()
    {
        for (int i = 0; i < SlotMgr.instance.bagSlotList.Length; i++)
        {
            if (!SlotMgr.instance.bagSlotList[i].HasItem())
            {
                return SlotMgr.instance.bagSlotList[i];
            }
        }
        return null;
    }

    //更新物品介绍栏与激活状态
    public void SetDesc(bool a, Vector2 pos, string text = null)
    {
        m_DescObj.SetActive(a);
        m_DescText.text = text;
        m_DescObj.transform.position = pos+new Vector2(22,-22);
        
    }
}
