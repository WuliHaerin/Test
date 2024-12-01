using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer 
{
    public int id = 0;                                //定时器ID
    public float duration = 0;                  //持续时间
    public int repeatCount = 0;              //重复次数
    public Action complete;                   //回调函数
    private float m_PassTime = 0;          //已经过去的时间
    private int m_RepeatCount = 0;       //已经重复的次数
    public bool isFinish = false;              //定时器是否结束

    public void Update(float delta)
    {
        if(isFinish)
        {
            return;
        }
        m_PassTime += delta;
        if(m_PassTime>=duration)
        {
            m_PassTime -= duration;
            m_RepeatCount++;
            if(repeatCount>0 && m_RepeatCount>=repeatCount)
            {
                isFinish = true;
            }
            if(complete!=null)
            {
                complete();
            }
        }
    }

}
