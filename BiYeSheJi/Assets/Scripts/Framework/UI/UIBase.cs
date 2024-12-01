using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    [NonSerialized]
    public string uiPath;
    private object m_Param;
    public UILayer layer;

    public void Init(object param)
    {
        m_Param = param;
    }

    public virtual void ShowUI()
    {
        UIMgr.instance.OpenUI(uiPath);
        OnEnter();
    }

    public virtual void HideUI()
    {
        UIMgr.instance.CloseUI(uiPath);
        OnExit();
    }

    public virtual void OnEnter()
    { 

    }

    public virtual void OnExit()
    {

    }

    public void SetUIVisible(bool a)
    {
        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.interactable = a;
        if(a==true)
        {
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.alpha = 0;
        }
    }

    
}
