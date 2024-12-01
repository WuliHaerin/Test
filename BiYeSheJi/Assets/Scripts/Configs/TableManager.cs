 using System;using System.Collections.Generic;using UnityEngine;
class TableManager
{
    private static ArmorTable m_ArmorTable = null;
    public static ArmorTable armorTable
    {
        get
        {
            if (m_ArmorTable == null)
            {
                m_ArmorTable = new ArmorTable();
                m_ArmorTable.LoadConfig("Configs/Armor");
            }
            return m_ArmorTable;
        }
    }
    private static GunTable m_GunTable = null;
    public static GunTable gunTable
    {
        get
        {
            if (m_GunTable == null)
            {
                m_GunTable = new GunTable();
                m_GunTable.LoadConfig("Configs/Gun");
            }
            return m_GunTable;
        }
    }
    private static PotionTable m_PotionTable = null;
    public static PotionTable potionTable
    {
        get
        {
            if (m_PotionTable == null)
            {
                m_PotionTable = new PotionTable();
                m_PotionTable.LoadConfig("Configs/Potion");
            }
            return m_PotionTable;
        }
    }
    private static ShopTable m_ShopTable = null;
    public static ShopTable shopTable
    {
        get
        {
            if (m_ShopTable == null)
            {
                m_ShopTable = new ShopTable();
                m_ShopTable.LoadConfig("Configs/UIShop");
            }
            return m_ShopTable;
        }
    }

}