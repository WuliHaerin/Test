using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr : SingleTonMono<UIMgr>
{
    private Transform m_UIRootTransform;
    //�洢UI��Ӧ�㼶���ֵ�
    private Dictionary<UILayer, Transform> m_LayerDic = new Dictionary<UILayer, Transform>();
    //�洢�Ѿ����볡����UI
    private Dictionary<string, UIBase> m_OnLoadUI = new Dictionary<string, UIBase>();
    //�洢�Ѿ������UI
    private Dictionary<string, UIBase> m_ActiveUI = new Dictionary<string, UIBase>();
    //�洢�Ѿ��򿪵�UI�б�
    private List<UIBase> m_ShowingUIList = new List<UIBase>();

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void Init()
    {
        //����Canvas(��UIRoot)��Ԥ����
        GameObject rootObj = ResMgr.instance.LoadAsset(SysDefine.UIRootPath, transform);
        //�ҵ����ڵ�
        m_UIRootTransform = rootObj.transform;

        //�ҵ���ͬLayer��Transform����ŵ� m_LayerDic�ֵ���
        Transform normalTransform = m_UIRootTransform.Find("Normal");
        m_LayerDic.Add(UILayer.Normal, normalTransform);
        Transform popupTransform = m_UIRootTransform.Find("Popup");
        m_LayerDic.Add(UILayer.Popup, popupTransform);
    }

    public bool CheckShowing(string path)
    {
        for (int i = 0; i < m_ShowingUIList.Count; i++)
        {
            UIBase ui = m_ShowingUIList[i];
            if (ui.uiPath == path)
            {
                return true;
            }
        }
        return false;
    }
    public UIBase GetShowingUI(string path)
    {
        for (int i = 0; i < m_ShowingUIList.Count; i++)
        {
            UIBase ui = m_ShowingUIList[i];
            if (ui.uiPath == path)
            {
                return ui;
            }
        }
        return null;
    }
    public UIBase LoadUI(string path, object param = null)
    {
        UIBase ui=null;
        if (!m_OnLoadUI.TryGetValue(path, out ui))
        {
            GameObject uiObj = ResMgr.instance.LoadAsset(path);
            ui = uiObj.GetComponent<UIBase>();
            if (ui != null)
            {
                ui.uiPath = path;
                //���䵽��ͬ�㼶
                uiObj.transform.SetParent(m_LayerDic[ui.layer], false);
                //�洢��������ui���ֵ���
                m_OnLoadUI.Add(path, ui);
                m_ActiveUI.Add(path, ui);
                ui.Init(param);
                //����UI
                CloseUI(path);
            }
        }
        return ui;

    }
    public void SetUIActive(string path,bool isActive)
    {
        UIBase ui;
        if (!m_OnLoadUI.TryGetValue(path, out ui))
        {
            Debug.Log("��UI������");
            return;
        }
        if (isActive)
        {
            if (m_ActiveUI.TryGetValue(path, out ui))
            {
                Debug.Log("��UI�Ѽ���");
                return;
            }
            m_ActiveUI.Add(path, ui);
            ui.gameObject.SetActive(true);
        }
        else
        {
            if (!m_ActiveUI.TryGetValue(path, out ui))
            {
                Debug.Log("��UI��ʧЧ");
                return;
            }
            ui.gameObject.SetActive(false);
            m_ActiveUI.Remove(path);
        }
       
    }
    public void OpenUI(string path, object param = null)
    {
        if(CheckShowing(path))
        {
            Debug.Log("UI�Ѵ�");
            return;
        }
        UIBase ui;
        if (m_OnLoadUI.TryGetValue(path, out ui))
        {
            if(!m_ActiveUI.TryGetValue(path,out ui))
            {
                SetUIActive(path,true);
            }
            ui.SetUIVisible(true);
            m_ShowingUIList.Add(ui);
        }
        else
        {
            Debug.Log("δ�����UI");
        }
    }
    public void CloseUI(string path)
    {
        UIBase ui;
        if (!m_OnLoadUI.TryGetValue(path, out ui))
        {
            Debug.Log("δ�����UI");
            return;
        }
        if (CheckShowing(path))
        {
            ui = GetShowingUI(path);
            m_ShowingUIList.Remove(ui);
        }
        ui.SetUIVisible(false);

    }
    public void ClearUI()
    {
        for (int i = 0; i < m_ShowingUIList.Count; i++)
        {
            m_ShowingUIList[i].HideUI();
        }
        m_ShowingUIList.Clear();
    }

}
