using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimMgr : AnimMgr
{
    public override void JudgeAnimState()
    {
        base.JudgeAnimState();
        if (Player.instance.GetGun()!=null)
        {
            if (isFire)
            {
                curMotion = MotionType.fire;
            }
            if (isRunning)
            {
                curMotion = MotionType.run_Gun;
            }
            if (isReload)
            {
                curMotion = MotionType.reload;
            }
            if (isCrouch)
            {
                curMotion = MotionType.crouch_idle_Gun;
            }
            if (isJump)
            {
                curMotion = MotionType.jump_Gun;
            }
            if (isCrouchFire)
            {
                curMotion = MotionType.crouch_Fire;
            }
            if (isCrouchMove)
            {
                curMotion = MotionType.crouch_move_Gun;
            }
            if (isCrouchReload)
            {
                curMotion = MotionType.crouch_reload;
            }
            if (isIdle)
            {
                curMotion = MotionType.idle_Gun;
            }
        }
        else
        {
            if (isRunning)
            {
                curMotion = MotionType.run_knife;
            }
            if (isCrouch)
            {
                curMotion = MotionType.crouch_idle_knife;
            }
            if (isJump)
            {
                curMotion = MotionType.jump_knife;
            }
            if (isCrouchMove)
            {
                curMotion = MotionType.crouch_move_knife;
            }
            if (isIdle)
            {
                curMotion = MotionType.idle_knife;
            }
        }
        if (isKnifeAttack)
        {
            curMotion = MotionType.knife_attack;
        }
    }


}
