
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Animator _animator;
    private Vector3 _oriPos;
    private Vector3 _oriAngle;
    public GameObject firePos;
    private GunAnimMgr m_GunAnimMgr;
    private Transform m_BulletMgrTrans;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _oriPos = transform.localPosition;
        _oriAngle = transform.localEulerAngles;
        m_GunAnimMgr = new GunAnimMgr();
        m_GunAnimMgr.gunAnimator = _animator;
        m_BulletMgrTrans = BulletMgr.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        AnimationChange();
    }

    
    private void AnimationChange()
    {
        m_GunAnimMgr.SetAllState(Player.instance.playerAnimMgr);
        m_GunAnimMgr.JudgeAnimState();
    }

    
    private void SetReloadFalse()
    {
        Player.instance.playerAnimMgr.isReload = false;
    }

    private void Fire()
    {
        GameObject bulletObj = ResMgr.instance.LoadAsset("Prefabs/Weapon/bullet",m_BulletMgrTrans);
        BulletMgr.instance.bulletList.Add(bulletObj.GetComponent<Bullet>());
    }
}
