namespace KBEngine
{
    using UnityEngine;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class KnapSackInfo
    {

        public static KnapSackInfo inst = null;
        //装备
        public Dictionary<Byte, KnapsakItem> equipItems = new Dictionary<byte, KnapsakItem>();
        //背包物品
        public Dictionary<Byte, Dictionary<UInt16, KnapsakItem>> allKanpItems = new Dictionary<Byte, Dictionary<UInt16, KnapsakItem>>();
        //public Dictionary<string, KnapsakBehaviorBase> knapsakBehaviorDic = new Dictionary<string, KnapsakBehaviorBase>();
        public KnapSackInfo()
        {
            inst = this;
        }
    }

    public class KnapsakItem{
        public UInt64 serialnum;
		public UInt32 tableId;
		public Byte bagFrameIndex;
		public UInt16 bagItemIndex;
		public Byte count;
		public string name;
		public UInt32 altnasIndex;
		public UInt32 icon;
		public UInt32 modelId;
        public Byte stdMode;
        public Byte weight;
        public Byte func;
        public UInt16 PA1;
        public UInt16 PA2;
        public UInt16 MA1;
        public UInt16 MA2;
        public UInt16 PD1;
        public UInt16 PD2;
        public UInt16 MD1;
        public UInt16 MD2;
        public UInt32 HP;
        public UInt32 MP;
        public Byte Accurate;
        public Byte lucky;
        public Byte holiness;
        public Byte HPgen;
        public Byte MPgen;
        public Byte crit;
        public Byte need;
        public Byte needLevel;
        public UInt32 price;
        public Byte stock;
        public string script;
        public virtual void OnUse()
        {
            KBEngine.Avatar player = (KBEngine.Avatar)KBEngineApp.app.player();
            player.baseCall("ItemUse", new object[] { serialnum });

        }
        //new

    }

    //public class KnapsakItem
    //{
    //    public UInt64 serialnum;
    //    public UInt32 tableId;
    //    public Byte bagFrameIndex;
    //    public UInt16 bagItemIndex;
    //    public Byte count;
    //    public string name;
    //    public UInt32 altnasIndex;
    //    public UInt32 icon;
    //    public UInt32 modelId;
    //    public virtual void OnUse()
    //    {
           
    //    }
    //    //new

    //}

    public class SupplyItem
    {
        public virtual void OnUse()
        {
            KBEngine.Avatar player = (KBEngine.Avatar)KBEngineApp.app.player();
            player.cellCall("SupplyItem", new object[] { });
        }
    }
    //public class KnapsakBehaviorBase
    //{
    //    public virtual 
    //}
}