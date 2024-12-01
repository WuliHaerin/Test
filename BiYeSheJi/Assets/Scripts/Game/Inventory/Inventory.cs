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

    //�洢��Ʒ
    public bool StoreItem(Item item)
    {
        if (item == null)
        {
            Debug.Log("����Ʒ������");
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
                Debug.LogWarning("�ռ䲻��");
                return false;
            }

        }
        return true;
    }
    //�ҵ���ͬ��Ʒ�Ĳ�
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
    //�ҵ��ղ�
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

    //������Ʒ�������뼤��״̬
    public void SetDesc(bool a, Vector2 pos, string text = null)
    {
        m_DescObj.SetActive(a);
        m_DescText.text = text;
        m_DescObj.transform.position = pos+new Vector2(22,-22);
        
    }
}
