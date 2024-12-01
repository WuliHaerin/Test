using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff 
{
    //public int id = 0;
    //public Transform m_Caster = null;   //施法者
    //public Transform m_Target = null;   //Buff接受目标
    //public BuffItem m_BuffItem = null;  //Buff属性
    //protected float m_Duration = 0;     //Buff持续时间
    //protected float m_Interval = 0;     //Buff作用间隔
    //private float m_PastTime = 0;       //Buff生效已经过去的时间
    //private float m_IntervalPastTime = 0;  //作用间隔已经过去的时间
    //public bool isDone = false;         //Buff是否完成

    ////Buff初始化
    //public void Init(BuffItem config, Transform target )
    //{
    //    m_BuffItem = config;
    //    id = config.id;
    //    m_Target = target;
    //}

    ////Buff重置
    //public virtual void Reset()
    //{
    //    m_PastTime = 0;
    //    m_IntervalPastTime = 0;
    //}

    //public void Loop(float deltaTime)
    //{
    //    m_PastTime += deltaTime;
    //    //如果Buff持续时间到则Buff完成返回出去
    //    if(m_PastTime>=m_BuffItem.duration)
    //    {
    //        isDone = true;
    //        return;
    //    }
    //    //如果Buff作用间隔时间到，则执行Buff的具体效果函数，并重置间隔过去的时间
    //    if (m_IntervalPastTime>=m_Interval)
    //    {
    //        OnInterval();
    //    }
    //    m_PastTime -= m_Interval;
    //}

    ////具体Buff生效逻辑
    //public virtual void OnInterval()
    //{

    //}

}
