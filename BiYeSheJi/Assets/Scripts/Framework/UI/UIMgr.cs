using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr : SingleTonMono<UIMgr>
{
    private Transform m_UIRootTransform;
    //存储UI对应层级的字典
    private Dictionary<UILayer, Transform> m_LayerDic = new Dictionary<UILayer, Transform>();
    //存储已经载入场景的UI
    private Dictionary<string, UIBase> m_OnLoadUI = new Dictionary<string, UIBase>();
    //存储已经激活的UI
    private Dictionary<string, UIBase> m_ActiveUI = new Dictionary<string, UIBase>();
    //存储已经打开的UI列表
    private List<UIBase> m_ShowingUIList = new List<UIBase>();

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void Init()
    {
        //加载Canvas(即UIRoot)的预制体
        GameObject rootObj = ResMgr.instance.LoadAsset(SysDefine.UIRootPath, transform);
        //找到根节点
        m_UIRootTransform = rootObj.transform;

        //找到不同Layer的Transform，存放到 m_LayerDic字典中
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
                //分配到不同层级
                uiObj.transform.SetParent(m_LayerDic[ui.layer], false);
                //存储到已载入ui的字典里
                m_OnLoadUI.Add(path, ui);
                m_ActiveUI.Add(path, ui);
                ui.Init(param);
                //隐藏UI
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
            Debug.Log("该UI不存在");
            return;
        }
        if (isActive)
        {
            if (m_ActiveUI.TryGetValue(path, out ui))
            {
                Debug.Log("该UI已激活");
                return;
            }
            m_ActiveUI.Add(path, ui);
            ui.gameObject.SetActive(true);
        }
        else
        {
            if (!m_ActiveUI.TryGetValue(path, out ui))
            {
                Debug.Log("该UI已失效");
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
            Debug.Log("UI已打开");
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
            Debug.Log("未载入该UI");
        }
    }
    public void CloseUI(string path)
    {
        UIBase ui;
        if (!m_OnLoadUI.TryGetValue(path, out ui))
        {
            Debug.Log("未载入该UI");
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
