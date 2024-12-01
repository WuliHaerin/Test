using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;


public class TableItem
{
    public int id;
}

public class Item : TableItem
{
    public string name;
    public ItemType itemType;
    public bool stackable;
    public string itemDesc;
    public string prefabPath;
    public string iconPath;
    public int count = 1;

    public Item DeepClone()
    {
        Item item = (Item)MemberwiseClone();
        item.itemType = itemType;
        item.name = name;
        item.itemDesc = itemDesc;
        item.prefabPath = prefabPath;
        item.iconPath = iconPath;
        return item;
    }
}


public class Table<T> where T : TableItem 
{
    protected List<T> m_ItemList = new List<T>();
    public void LoadConfig(string path)
    {
        //string fullPath = PathUtil.ProjectPath + path;
        //string content = File.ReadAllText(fullPath);
        //m_ItemList = new List<TableItem>(JsonConvert.DeserializeObject<List<T>>(content));
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        m_ItemList = JsonConvert.DeserializeObject<List<T>>(textAsset.text);
    }

    public T GetDataByID(int inID)
    {
        foreach (T item in m_ItemList)
        {
            if (item.id == inID)
            {
                return item;
            }
        }
        return null;
    }

    public T GetDataByIndex(int index)
    {
        if (index < 0 || index > m_ItemList.Count)
        {
            return null;
        }
        return m_ItemList[index];
    }

    public int itemCount
    {
        get { return m_ItemList.Count; }
    }
}

