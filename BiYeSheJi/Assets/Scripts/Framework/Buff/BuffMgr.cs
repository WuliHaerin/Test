using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuffMgr 
{
    //private Transform m_Target = null;
    //public Transform target
    //{
    //    get { return m_Target; }
    //    set { m_Target = value; }
    //}
    //private List<Buff> m_BuffList = new List<Buff>();

    //public Buff AddBuff(int id)
    //{
    //    Buff buff = GetBuff(id);
    //    if(buff!=null)
    //    {
    //        //���㣿ˢ��,���Ĭ��ˢ�²���
    //        buff.Reset();
    //        return buff;
    //    }
    //    //�ҵ����ñ�����
    //    BuffItem config = TableManager.buffTable.GetDataByID(id);
    //    Type buffType = Type.GetType(config.className);
    //    buff = Activator.CreateInstance(buffType) as Buff;
    //    buff.Init(config, m_Target);
    //    m_BuffList.Add(buff);
    //    return buff;
    //}

    //public Buff GetBuff(int id)
    //{
    //    for(int i=0; i<m_BuffList.Count;i++)
    //    {
    //        Buff buff = m_BuffList[i];
    //        if (m_BuffList[i].id==id)
    //        {
    //            return buff;
    //        }
    //    }
    //    return null;
    //}

    //public void Loop(float deltaTime)
    //{
    //    //����Buff��Loop
    //    for(int i=0;i<m_BuffList.Count;i++)
    //    {
    //        Buff buff = m_BuffList[i];
    //        buff.Loop(deltaTime);
    //    }
    //    //�Ƴ���ɵ�Buff
    //    for(int i=m_BuffList.Count;i>=0;i--)
    //    {
    //        if(m_BuffList[i].isDone)
    //        {
    //            m_BuffList.RemoveAt(i);
    //        }
    //    }
    //}
}
