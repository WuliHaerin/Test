using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff 
{
    //public int id = 0;
    //public Transform m_Caster = null;   //ʩ����
    //public Transform m_Target = null;   //Buff����Ŀ��
    //public BuffItem m_BuffItem = null;  //Buff����
    //protected float m_Duration = 0;     //Buff����ʱ��
    //protected float m_Interval = 0;     //Buff���ü��
    //private float m_PastTime = 0;       //Buff��Ч�Ѿ���ȥ��ʱ��
    //private float m_IntervalPastTime = 0;  //���ü���Ѿ���ȥ��ʱ��
    //public bool isDone = false;         //Buff�Ƿ����

    ////Buff��ʼ��
    //public void Init(BuffItem config, Transform target )
    //{
    //    m_BuffItem = config;
    //    id = config.id;
    //    m_Target = target;
    //}

    ////Buff����
    //public virtual void Reset()
    //{
    //    m_PastTime = 0;
    //    m_IntervalPastTime = 0;
    //}

    //public void Loop(float deltaTime)
    //{
    //    m_PastTime += deltaTime;
    //    //���Buff����ʱ�䵽��Buff��ɷ��س�ȥ
    //    if(m_PastTime>=m_BuffItem.duration)
    //    {
    //        isDone = true;
    //        return;
    //    }
    //    //���Buff���ü��ʱ�䵽����ִ��Buff�ľ���Ч�������������ü����ȥ��ʱ��
    //    if (m_IntervalPastTime>=m_Interval)
    //    {
    //        OnInterval();
    //    }
    //    m_PastTime -= m_Interval;
    //}

    ////����Buff��Ч�߼�
    //public virtual void OnInterval()
    //{

    //}

}
