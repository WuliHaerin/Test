using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : SingleTonMono<GameMgr>
{
    public void Init()
    {
        GameEngine.instance.Init();
        UIMgr.instance.Init();
        LuaEngine.instance.Init();
        BulletMgr.instance.Init();
    }

    public override void Awake()
    {
        DontDestroyOnLoad(this.transform.root);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
