using UnityEngine;
using System.Collections;
using System;

public class BagItemBase {

    public const int ITEM_WEAPON = 2;
    public const int ITEM_DEFEN = 3;
    public const int ITEM_NOMARL = 4;
    public string name;
    public UInt64 id;
    public UInt64 tableid;
    public int altnas_num;
    public string icon;

    public int frameIndex;//第几个背包索引0开始
    public int bagIndex;//背包中第几个格子索引， 0开始
    public virtual void use()
    {

    }
    //public const int ITEM_NOMARL = 5;

}

public class normalItem : BagItemBase
{

}

public class Weapon : BagItemBase
{

}