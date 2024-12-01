using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResMgr : SingleTonMono<ResMgr>
{
    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    /// <summary>
    ///HashTable 用于缓存已经加载的数据
    /// </summary>
    private Hashtable m_CacheTable = null;
    public Hashtable cacheTable
    {
        get
        {
            if(m_CacheTable==null)
            {
                m_CacheTable = new Hashtable();
            }
            return m_CacheTable;
        }
    }

    /// <summary>
    /// LoadAsset载入数据
    /// </summary>
    public GameObject LoadAsset(string path,Transform parent=null, bool isCache=false)
    {
        GameObject prefab = null;
        if(cacheTable.ContainsKey(path))
        {
            prefab = cacheTable[path] as GameObject;
        }
        else
        {
            prefab = Resources.Load<GameObject>(path);
            if(isCache)
            {
                cacheTable.Add(path, prefab);
            }
        }
        GameObject obj = Instantiate<GameObject>(prefab, parent);
        return obj;
    }

}
