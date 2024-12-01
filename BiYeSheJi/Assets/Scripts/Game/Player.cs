using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Serialization;


public class Player : SingleTonMono<Player>
{
    private float m_MoveSpeed=5;
    private float m_JumpSpeed = 10;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    [SerializeField]
    private PolygonCollider2D _polyCollider;
    [SerializeField]
    private CapsuleCollider2D _capsuleCollider;
    private float m_GravityScale;
    private Slot m_GunSlot;
    private PlayerAnimMgr m_PlayerAnimMgr;
    public PlayerAnimMgr playerAnimMgr
    {
        get
        {
            return m_PlayerAnimMgr;
        }
    }

    public override void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        m_GravityScale = _rigidBody.gravityScale;
        m_PlayerAnimMgr = new PlayerAnimMgr();
        UIMgr.instance.LoadUI("Prefabs/UI/UIInventory");
    }
    // Start is called before the first frame update
    void Start()
    {       
        m_GunSlot = SlotMgr.instance.equipSlotList[0];
        StartCoroutine("RefreshWeapon");
    }
    // Update is called once per frame
    void Update()
    {
        KnifeAttack();
        Fire();
        Jump();
        Reload();
        Crouch();
        AnimationChange();
        LoadWeapon();
        // Debug.Log(_rigidBody.velocity.y);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    //载入武器
    private void LoadWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (m_GunSlot.m_ItemUI != null && GetGun()==null)
            {
                string gunPath = m_GunSlot.m_ItemUI.item.prefabPath;
                ResMgr.instance.LoadAsset(gunPath, transform, true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (GetGun() !=null)
            {
                GameObject gunObj = transform.GetComponentInChildren<Gun>().gameObject;
                Destroy(gunObj);
            }
        }
    }
    //刷新武器
    public IEnumerator RefreshWeapon()
    {
        for(; ; )
        {
            if (GetGun()!=null)
            {
                if (!m_GunSlot.HasItem())
                {
                    Destroy(transform.GetChild(0).gameObject);
                    m_GunSlot.m_ItemUI = null;
                }
                else
                {                    
                    if (GetGun().name != m_GunSlot.m_ItemUI.item.name+"(Clone)")
                    {
                        Destroy(GetGun());
                        string itemPath = m_GunSlot.m_ItemUI.item.prefabPath;
                        ResMgr.instance.LoadAsset(itemPath, transform);
                    }
                }
            }
            yield return new WaitForSeconds(2f);           
        }
    }
    //角色移动
    public void Movement()
    {
        float faceDir = Input.GetAxisRaw("Horizontal");
        float inputX = Input.GetAxis("Horizontal");
        //左右移动
        if ((inputX > 0.1 || inputX < -0.1) && !m_PlayerAnimMgr.isFire && !m_PlayerAnimMgr.isReload)
        {
            m_PlayerAnimMgr.isRunning = true;
            switch (m_PlayerAnimMgr.curMotion)
            {
                case MotionType.run_knife:
                    m_MoveSpeed = 7;
                    break;
                case MotionType.run_Gun:
                    m_MoveSpeed = 7;
                    break;
                case MotionType.jump_Gun:
                    m_MoveSpeed = 4;
                    break;
                case MotionType.jump_knife:
                    m_MoveSpeed = 4;
                    break;
                case MotionType.crouch_move_knife:
                    m_MoveSpeed = 2;
                    break;
                case MotionType.crouch_move_Gun:
                    m_MoveSpeed = 2;
                    break;
                case MotionType.knife_attack:
                    m_MoveSpeed = 1.5f;
                    break;
                default:
                    break;
            }
            _rigidBody.velocity = new Vector2(inputX * m_MoveSpeed, _rigidBody.velocity.y);
        }
        else
        {
            m_PlayerAnimMgr.isRunning = false;
        }      
        //面向方向设置
        if (faceDir != 0)
        {
            transform.localScale = new Vector3(faceDir, 1, 1);
        }
    }
    //近战攻击设置
    public void KnifeAttack()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (!m_PlayerAnimMgr.isReload && !m_PlayerAnimMgr.isJump && !m_PlayerAnimMgr.isFire)
            {
                m_PlayerAnimMgr.isKnifeAttack = true;
                if(GetGun()!=null)
                {
                    GetGun().SetActive(false);
                }
            }
        }
    }
    //开火设置
    public void Fire()
    {
        if (!m_PlayerAnimMgr.isReload && !m_PlayerAnimMgr.isJump && Player.instance.GetGun()!=null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                m_PlayerAnimMgr.isFire = true;
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                m_PlayerAnimMgr.isFire = false;
            }
        }
    }
    //换弹设置
    public void Reload()
    {
        if (Input.GetKey(KeyCode.R))
        {
            if (!m_PlayerAnimMgr.isFire && !m_PlayerAnimMgr.isJump)
            {
                m_PlayerAnimMgr.isReload = true;
            }
        }
    }
    //下蹲设置
    public void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !m_PlayerAnimMgr.isJump)
        {
            m_PlayerAnimMgr.isCrouch = true;
            _polyCollider.enabled = true;
            _capsuleCollider.enabled = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            m_PlayerAnimMgr.isCrouch = false;
            _polyCollider.enabled = false;
            _capsuleCollider.enabled = true;
        }
    }
    //跳跃设置
    public void Jump()
    {
        //RaycastHit2D leftRay = Physics2D.Raycast(new Vector2(transform.position.x-0.38f, transform.position.y ), Vector2.left, 0.01f);
        //RaycastHit2D rightRay = Physics2D.Raycast(new Vector2(transform.position.x + 0.405f, transform.position.y), Vector2.right, 0.01f);
        RaycastHit2D downRay = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y-1.04f), Vector2.down, 0.02f);
        //跳跃
        if (Input.GetButton("Jump") && !m_PlayerAnimMgr.isFire && downRay.transform!=null)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, m_JumpSpeed);
            m_PlayerAnimMgr.isJump = true;
        }
        //判断跳跃结束
        if (_rigidBody.velocity.y < 0.01 && _rigidBody.velocity.y > -0.01 && downRay.transform != null)
        {
            m_PlayerAnimMgr.isJump = false;
        }
        //下落速度优化
        if (_rigidBody.velocity.y < -0.01)
        {
            _rigidBody.gravityScale += 0.1f;
        }
        else
        {
            _rigidBody.gravityScale = m_GravityScale;
        }
    }
    //获取枪
    public GameObject GetGun()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.tag == "Gun")
            {
                return transform.GetChild(i).gameObject;
            }
        }
        return null;
    }
    //动画切换
    private void AnimationChange()
    {
        if (GetGun()!=null)
        {
            switch (GetGun().name)
            {
                case "Pisto(Clone)":
                    _animator.SetFloat("gunType", 0);
                    break;
                case "Rifle(Clone)":
                    _animator.SetFloat("gunType", 1);
                    break;
                default:
                    break;
            }
        }
        m_PlayerAnimMgr.JudgeAnimState();
        m_PlayerAnimMgr.SetAnim(_animator);
    }
    //为动画机提供的事件接口，让动画结束时为false
    private void SetKnifeAttackFalse()
    {
        m_PlayerAnimMgr.isKnifeAttack = false;
        if(GetGun()!=null)
        {
            GetGun().SetActive(true);
        }
    }


}

