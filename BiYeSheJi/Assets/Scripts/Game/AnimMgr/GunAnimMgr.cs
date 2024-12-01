using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimMgr : AnimMgr
{
    public Animator gunAnimator;
    public override void JudgeAnimState()
    {
        base.JudgeAnimState();
        if (isFire)
        {
            gunAnimator.SetBool("isFire", true);
        }
        else
        {
            gunAnimator.SetBool("isFire", false);
        }
        if (isRunning)
        {
            gunAnimator.SetBool("isRunning", true);
        }
        else
        {
            gunAnimator.SetBool("isRunning", false);
        }
        if (isReload)
        {
            gunAnimator.SetTrigger("reload");
        }
        if (isCrouch)
        {
            gunAnimator.SetBool("isCrouch", true);
        }
        else
        {
            gunAnimator.SetBool("isCrouch", false);
        }
        if (isJump)
        {
            gunAnimator.SetBool("isJump", true);
        }
        else
        {
            gunAnimator.SetBool("isJump", false);
        }

    }
}
