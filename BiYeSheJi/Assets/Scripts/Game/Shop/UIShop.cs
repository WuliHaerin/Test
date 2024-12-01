using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop :UIBase
{
    public Button closeBtn;
    // Start is called before the first frame update
    void Start()
    {
        closeBtn.onClick.AddListener(() =>
       {
           UIMgr.instance.SetUIActive(uiPath, false);
       });
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public override void ShowUI()
    {
        base.ShowUI();
    }

    public override void HideUI()
    {
        base.HideUI();
    }



}
