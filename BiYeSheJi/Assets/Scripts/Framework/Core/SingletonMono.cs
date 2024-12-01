using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTonMono<T> : MonoBehaviour where T : SingleTonMono<T>
{
    private static T m_Instance;
    public static T instance
    {
        get
        {
            if(m_Instance==null)
            {
                m_Instance = FindObjectOfType<T>();
                if(m_Instance==null)
                {
                    GameObject singleObj = new GameObject(typeof(T).ToString());
                    m_Instance = singleObj.AddComponent<T>();
                }
            }
            return m_Instance;
        }
    }

    public virtual void Awake()
    {
        if(m_Instance==null)
        {
            m_Instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
