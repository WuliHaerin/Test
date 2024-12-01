using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : Item
{
    public int hpBuff;
    public int ackBuff;
    public int defenseBuff;
    public int speedBuff;
}

public class ArmorItem: Item
{
    public int defense;
    public int speedBuff;
}

public class BuffItem : Item
{
    public string className;
    public float duration;
    public float interval;
    public BuffType buffType;
}

public class ShopItem : TableItem
{
    public float price;
    public int itemID;
    public int num;
}


public class GunItem : Item
{
    public int bulletID;

}

class PotionTable : Table<PotionItem>
{

}

class ArmorTable: Table<ArmorItem>
{

}

class GunTable:Table<GunItem>
{

}

class BuffTable : Table<BuffItem>
{
}

class ShopTable : Table<ShopItem>
{

}
