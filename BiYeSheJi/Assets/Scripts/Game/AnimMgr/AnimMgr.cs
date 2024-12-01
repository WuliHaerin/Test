using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum MotionType
{
    idle_knife = 0,
    run_knife = 1,
    crouch_idle_knife = 2,
    crouch_move_knife=3,
    jump_knife=4,
    knife_attack=5,
    idle_Gun=6,
    run_Gun=7,
    crouch_idle_Gun=8,
    crouch_move_Gun=9,
    jump_Gun=10,
    fire=11,
    crouch_Fire=12,
    reload=13,
    crouch_reload=14
}

public class AnimMgr
{
    public MotionType curMotion;
    public bool isFire;
    public bool isRunning;
    public bool isReload;
    public bool isCrouch;
    public bool isJump;
    public bool isKnifeAttack;
    public bool isCrouchFire;
    public bool isCrouchMove;
    public bool isCrouchReload;
    public bool isIdle;

    public virtual void SetAnim(Animator animator)
    {
        animator.SetInteger("MotionType",(int)curMotion);
    }
    public virtual void JudgeAnimState()
    {
        if (isFire && isCrouch)
        {
            isCrouchFire = true;
        }
        else
        {
            isCrouchFire = false;
        }
        if (isRunning && isCrouch)
        {
            isCrouchMove = true;
        }
        else
        {
            isCrouchMove = false;
        }
        if (isReload && isCrouch)
        {
            isCrouchReload = true;
        }
        else
        {
            isCrouchReload = false;
        }
        if (!isReload && !isFire && !isCrouch && !isJump && !isRunning && !isKnifeAttack)
        {
            isIdle = true;
        }
        else
        {
            isIdle = false;
        }
    }

    public void SetAllState(AnimMgr animMgr) 
    {
        isFire = animMgr.isFire;
        isRunning = animMgr.isRunning;
        isReload = animMgr.isReload;
        isCrouch = animMgr.isCrouch;
        isJump = animMgr.isJump;
        isKnifeAttack = animMgr.isKnifeAttack;
        isCrouchFire = animMgr.isCrouchFire;
        isCrouchMove = animMgr.isCrouchMove;
        isCrouchReload = animMgr.isCrouchReload;
    }
    

}

