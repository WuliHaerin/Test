using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : SingleTonMono<GameEngine>
{
    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public void Init()
    {

    }
    private void Update()
    {
        //������ʱ��ģ���Update
        TimerMgr.instance.Update(Time.deltaTime);

        //����ģ���Update�������������
    }
}
