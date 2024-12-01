using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimerMgr :SingleTon<TimerMgr>
{
    private static int taskID = 0;
    private Dictionary<int, Timer> m_DicTimer = new Dictionary<int, Timer>();     
    private List<Timer> m_AddCache = new List<Timer>();
    private List<Timer> m_RemoveCache = new List<Timer>();

    public int StartTimer(float duration,int repeatCount,Action complete)
    {
        Timer timer = new Timer();
        timer.duration = duration;
        timer.repeatCount = repeatCount;
        timer.complete += complete;
        timer.id = ++taskID;
        m_AddCache.Add(timer);
        return taskID;
    }

    public void Update(float delta)
    {
        //����ӻ�����Ӷ�ʱ������ջ���
        for(int i=0;i<m_AddCache.Count;i++)
        {
            Timer timer = m_AddCache[i];
            m_DicTimer.Add(timer.id,timer);
        }
        m_AddCache.Clear();
        //���Ѿ���ɵĶ�ʱ����ӵ��Ƴ�����
        foreach(var item in m_DicTimer)
        {
            Timer timer = item.Value;
            if (timer.isFinish)
            {
                m_RemoveCache.Add(timer);
            }
        }
        for(int i=0;i<m_RemoveCache.Count;i++)
        {
            Timer timer = m_RemoveCache[i];
            m_DicTimer.Remove(timer.id);
        }
        m_RemoveCache.Clear();
        //����ÿ����ʱ����Update
        foreach(var item in m_DicTimer)
        {
            Timer timer = item.Value;
            if(timer!=null)
            {
                timer.Update(delta);
            }
        }
    }
}
