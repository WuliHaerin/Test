using System;
using System.Collections.Generic;
using UnityEngine;
public class ArmorItem : Item
{
    public int defense;
		public int speedBuff;
		public string fatherClass;
		
}
public class ArmorTable : Table<ArmorItem>
{
}
public class GunItem : Item
{
    public int bulletID;
		public string fatherClass;
		
}
public class GunTable : Table<GunItem>
{
}
public class PotionItem : Item
{
    public int ackBuff;
		public int defenseBuff;
		public int speedBuff;
		public int hpBuff;
		public string fatherClass;
		
}
public class PotionTable : Table<PotionItem>
{
}
public class ShopItem : TableItem
{
    public float price;
		public int  itemID;
		public int num;
		public string fatherClass;
		
}
public class ShopTable : Table<ShopItem>
{
}
