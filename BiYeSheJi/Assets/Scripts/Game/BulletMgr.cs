using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMgr : SingleTonMono<BulletMgr>
{
    private List<Bullet> m_BulletList;
    public List<Bullet> bulletList
    {
        get
        {
            return m_BulletList;
        }
    }
    public override void Awake()
    {
        base.Awake();
        m_BulletList = new List<Bullet>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {

    }

    void CheckOutOfSide()
    {

    }
}
