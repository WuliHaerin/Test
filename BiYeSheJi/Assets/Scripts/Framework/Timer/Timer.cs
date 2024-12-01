using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer 
{
    public int id = 0;                                //��ʱ��ID
    public float duration = 0;                  //����ʱ��
    public int repeatCount = 0;              //�ظ�����
    public Action complete;                   //�ص�����
    private float m_PassTime = 0;          //�Ѿ���ȥ��ʱ��
    private int m_RepeatCount = 0;       //�Ѿ��ظ��Ĵ���
    public bool isFinish = false;              //��ʱ���Ƿ����

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
