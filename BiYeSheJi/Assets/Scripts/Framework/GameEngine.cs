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
        //驱动定时器模块的Update
        TimerMgr.instance.Update(Time.deltaTime);

        //其他模块的Update可以在这边驱动
    }
}
