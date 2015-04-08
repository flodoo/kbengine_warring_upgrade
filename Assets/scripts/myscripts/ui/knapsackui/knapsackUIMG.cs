using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using KBEngine;
using System.Threading;

public class knapsackUIMG : MonoBehaviour {

    public class FrameBagIndex
    {
        public Byte FrameIndex = 0;
        public UInt16 BagIndex= 0;
        public bool isUse = false;
    }
    public static knapsackUIMG inst = null;
    public const int MaxKnapsackNum = 60;
    UIGrid knapsackGrid = null;
    Dictionary<Byte, Dictionary<UInt16, UnityEngine.GameObject>> knapsackFrameGoDic = null;//所有物品go
    Dictionary<UnityEngine.GameObject, FrameBagIndex> frameBagDic = new Dictionary<UnityEngine.GameObject, FrameBagIndex>();
    Dictionary<UnityEngine.GameObject, UInt64> itemsDic = new Dictionary<UnityEngine.GameObject, UInt64>();

    //装备上的go
    Dictionary<Byte, UnityEngine.GameObject> equipFrameGoDic = new Dictionary<byte,UnityEngine.GameObject>();//装备格子
    //Dictionary<UnityEngine.GameObject, UInt64> equipDic = new Dictionary<UnityEngine.GameObject, ulong>();
    //Dictionary<GoodsItem, GameObject> knapsackItemsDic = null;
    bool isInitBagItems = false;
    bool isInitEquipItems = false;
    bool isEnableDelegates = false;
    void Awake()
    {
        inst = this;
        InitknapsackGrid();
    }

    void OnEnable()
    {
        InitKnapsackItems();
        InitEquipItems();
        EnableDelegates();
    }
    void OnDisable()
    {
        //DisableDelegates();
    }

    void InitknapsackGrid()
    {
        if (knapsackGrid == null)
            knapsackGrid = transform.Find("bg/items/Grid").GetComponent<UIGrid>();
        if (knapsackFrameGoDic == null)
        {
            knapsackFrameGoDic = new Dictionary<Byte, Dictionary<UInt16, UnityEngine.GameObject>>();
            Dictionary<UInt16, UnityEngine.GameObject> bagFrame0 = new Dictionary<UInt16, UnityEngine.GameObject>();
            for (UInt16 i = 0; i < MaxKnapsackNum; i++)
            {
                UnityEngine.GameObject item = newItemByName("knapsackitembg", knapsackGrid.transform);
                item.name = "" + i;
                bagFrame0.Add(i, item);
                frameBagDic.Add(item, new FrameBagIndex { BagIndex = i, FrameIndex = 0, isUse = false });
                //UIEventListener.Get(item).onClick += onItemClick;
            }
            knapsackFrameGoDic.Add(0, bagFrame0);
        }
        UnityEngine.GameObject efgo = transform.Find("bg/equips/Head").gameObject;
        efgo.GetComponent<UIEquipmentContainer>().funcCode = 0;
        efgo.GetComponent<UIEquipmentContainer>().index = 0;
        equipFrameGoDic.Add(0, efgo);
        efgo = transform.Find("bg/equips/Weapon").gameObject;
        efgo.GetComponent<UIEquipmentContainer>().funcCode = 1;
        efgo.GetComponent<UIEquipmentContainer>().index = 1;
        equipFrameGoDic.Add(1, efgo);
        efgo = transform.Find("bg/equips/Cloth").gameObject;
        efgo.GetComponent<UIEquipmentContainer>().funcCode = 2;
        efgo.GetComponent<UIEquipmentContainer>().index =2;
        equipFrameGoDic.Add(2, efgo);
        efgo = transform.Find("bg/equips/Necklace").gameObject;
        efgo.GetComponent<UIEquipmentContainer>().funcCode = 3;
        efgo.GetComponent<UIEquipmentContainer>().index = 3;
        equipFrameGoDic.Add(3, efgo);
        efgo = transform.Find("bg/equips/BraceletL").gameObject;
        efgo.GetComponent<UIEquipmentContainer>().funcCode = 4;
        efgo.GetComponent<UIEquipmentContainer>().index = 4;
        equipFrameGoDic.Add(4, efgo);
        efgo = transform.Find("bg/equips/BraceletR").gameObject;
        efgo.GetComponent<UIEquipmentContainer>().funcCode = 4;
        efgo.GetComponent<UIEquipmentContainer>().index = 5;
        equipFrameGoDic.Add(5, efgo);
        efgo = transform.Find("bg/equips/RingL").gameObject;
        efgo.GetComponent<UIEquipmentContainer>().funcCode = 5;
        efgo.GetComponent<UIEquipmentContainer>().index = 6;
        equipFrameGoDic.Add(6, efgo);
        efgo = transform.Find("bg/equips/RingR").gameObject;
        efgo.GetComponent<UIEquipmentContainer>().funcCode = 5;
        efgo.GetComponent<UIEquipmentContainer>().index = 7;
        equipFrameGoDic.Add(7, efgo);
    }

    void InitKnapsackItems()
    {
        if (isInitBagItems)
            return;
        if (KnapSackInfo.inst.allKanpItems.Count <= 0)
            return;
        foreach(var v in KnapSackInfo.inst.allKanpItems){
            foreach(var vb in v.Value){
                Debug.LogWarning("init bag items");
                UnityEngine.GameObject frame = knapsackFrameGoDic[v.Key][vb.Key];
                UnityEngine.GameObject goItem = newItemByName("knapsackitem", frame.transform);
                goItem.GetComponent<UISprite>().spriteName = vb.Value.icon+"";
                goItem.GetComponent<knapsackDragDropItem>().serialnum = vb.Value.serialnum;
                goItem.GetComponent<knapsackDragDropItem>().stdMode= vb.Value.stdMode;
                goItem.GetComponent<knapsackDragDropItem>().func = vb.Value.func;
                UIEventListener.Get(goItem).onDoubleClick = onBagItemDoubleClick;
                UIEventListener.Get(goItem).onHover = onBagItemHover;
                itemsDic.Add(goItem, vb.Value.serialnum);
            }
        }
        isInitBagItems = true;
    }

    void InitEquipItems()
    {
        if (isInitEquipItems == false)
        {
            //equipFrameGoDic = new Dictionary<byte, UnityEngine.GameObject>();
            foreach(var v in KnapSackInfo.inst.equipItems){
                UnityEngine.GameObject frame = equipFrameGoDic[(Byte)v.Key];
                UnityEngine.GameObject goItem = newItemByName("knapsackitem", frame.transform);
                frame.GetComponent<UIEquipmentContainer>().eqgo = goItem;
                goItem.GetComponent<UISprite>().spriteName = v.Value.icon + "";
                //UIEventListener.Get(goItem).onDoubleClick = onBagItemDoubleClick;
                //UIEventListener.Get(goItem).onHover = onBagItemHover;
            }
            isInitEquipItems = true;
        }
    }

    public void onBagItemDoubleClick(UnityEngine.GameObject go)
    {
        Debug.Log("double click");
        //FrameBagIndex fbi = frameBagDic[go.transform.parent.gameObject];
        //KnapsakItem ki = KnapSackInfo.inst.allKanpItems[fbi.FrameIndex][fbi.BagIndex];
        //ki.OnUse();
        UInt64 serialnum = go.GetComponent<knapsackDragDropItem>().serialnum;
        KBEngine.Avatar player = (KBEngine.Avatar)KBEngineApp.app.player();
        player.baseCall("ItemUse", new object[] { serialnum });
    }
    public void onBagItemHover(UnityEngine.GameObject go, bool state)
    {
        if (state){
            FrameBagIndex fbi = frameBagDic[go.transform.parent.gameObject];
            KnapsakItem ki = KnapSackInfo.inst.allKanpItems[fbi.FrameIndex][fbi.BagIndex];
            Debug.LogWarning("on hover tableid is " + ki.tableId);
            game_ui_autopos.ShowTips(ki.name, ki.tableId, ki.tableId, 500, go.transform.position);
        }
        else{
            Debug.LogWarning("not hover");
            game_ui_autopos.HideTips();
        }
    }

    UnityEngine.GameObject newItemByName(string name, Transform parent)
    {
        UnityEngine.GameObject item = ResourceManager.loadItem("ui/" + name);
        item.transform.parent = parent;
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = Vector3.one;
        return item;
    }

    void EnableDelegates()
    {
        if (!isEnableDelegates)
        {
            KBEEventProc.onAddBagItem += onAddBagItem;
            KBEEventProc.onDelBagItem += onDelBagItem;
            KBEEventProc.onEquiptedItem += onEquiptedItem;
            isEnableDelegates = true;
        }
        
    }

    public void DisableDelegates()
    {
        if (knapsackGrid != null)
        {
            KBEEventProc.onAddBagItem -= onAddBagItem;
            KBEEventProc.onDelBagItem -= onDelBagItem;
            KBEEventProc.onEquiptedItem -= onEquiptedItem;
        }
    }

    public void onAddBagItem(object framIndex, object itemIndex)
    {
        Debug.LogWarning("onAddBagItem");
        UnityEngine.GameObject frame = knapsackFrameGoDic[(Byte)framIndex][(UInt16)itemIndex];
        UnityEngine.GameObject goItem = newItemByName("knapsackitem", frame.transform);
        KnapsakItem ki = KnapSackInfo.inst.allKanpItems[(Byte)framIndex][(UInt16)itemIndex];
        goItem.GetComponent<UISprite>().spriteName = ki.icon + "";
        knapsackDragDropItem kddi = goItem.GetComponent<knapsackDragDropItem>();
        kddi.func = ki.func;
        kddi.stdMode = ki.stdMode;
        kddi.serialnum = ki.serialnum;
        UIEventListener.Get(goItem).onDoubleClick = onBagItemDoubleClick;
        UIEventListener.Get(goItem).onHover = onBagItemHover;
        itemsDic.Add(goItem, KnapSackInfo.inst.allKanpItems[(Byte)framIndex][(UInt16)itemIndex].serialnum);
    }

    public void onDelBagItem(object seialnum, object notuse)
    {
        UnityEngine.GameObject go = null;
        UInt64 delnum = (UInt64)seialnum;
        foreach (var v in itemsDic)
        {
            if (v.Value == delnum)
            {
                go = v.Key;
                break;
            }
        }
        if (go != null)
        {
            itemsDic.Remove(go);
            Destroy(go);
        }
    }

    //装备
    public void onEquiptedItem(object framIndex, object notuse)
    {
        Debug.LogWarning("onEquiptedItem");
        UnityEngine.GameObject frame = equipFrameGoDic[(Byte)framIndex];
        UnityEngine.GameObject goItem = newItemByName("knapsackitem", frame.transform);
        frame.GetComponent<UIEquipmentContainer>().eqgo = goItem;
        goItem.GetComponent<UISprite>().spriteName = KnapSackInfo.inst.equipItems[(Byte)framIndex].icon + "";
        //UIEventListener.Get(goItem).onDoubleClick = onBagItemDoubleClick;
        UIEventListener.Get(goItem).onHover = onBagItemHover;
        //itemsDic.Add(goItem, KnapSackInfo.inst.allKanpItems[(Byte)framIndex][(UInt16)itemIndex].serialnum);
    }

	// Update is called once per frame
	void Update () {
	
	}
    public void SetBagItemPos(UnityEngine.GameObject surface, UnityEngine.GameObject old)
    {
        Debug.LogWarning("setBagItemPos");
        Monitor.Enter(KBEngineApp.app.entities);
        KBEngine.Avatar player = (KBEngine.Avatar)KBEngineApp.app.player();
        UInt64 serialnum = itemsDic[old];
        FrameBagIndex new_fbi = frameBagDic[surface];
        player.setBagItemPos(serialnum, new_fbi.FrameIndex, new_fbi.BagIndex);
        Monitor.Exit(KBEngineApp.app.entities);
        Byte frame = 0;
        UInt16 bagindex = 0;
        foreach (var v in KnapSackInfo.inst.allKanpItems)
        {
            foreach (var mv in v.Value)
            {
                if (mv.Value.serialnum == serialnum)
                {
                    frame = v.Key;
                    bagindex = mv.Key;
                }
            }
        }
        KnapsakItem ksi = KnapSackInfo.inst.allKanpItems[frame][bagindex];
        KnapSackInfo.inst.allKanpItems[frame].Remove(bagindex);
        ksi.bagFrameIndex = new_fbi.FrameIndex;
        ksi.bagItemIndex = new_fbi.BagIndex;
        KnapSackInfo.inst.allKanpItems[new_fbi.FrameIndex].Add(new_fbi.BagIndex, ksi);
    }

    public void ReplaceBagItemPos(UnityEngine.GameObject newItem, UnityEngine.GameObject oldItem)
    {
        Debug.LogWarning("setBagItemPos");
        Monitor.Enter(KBEngineApp.app.entities);
        KBEngine.Avatar player = (KBEngine.Avatar)KBEngineApp.app.player();
        UInt64 old_serialnum = itemsDic[oldItem];
        UInt64 new_serialnum = itemsDic[newItem];
        player.rePlaceBagItemPos(new_serialnum, old_serialnum);
        Monitor.Exit(KBEngineApp.app.entities);
        Byte new_frame = 0;
        UInt16 new_bagindex = 0;
        foreach (var v in KnapSackInfo.inst.allKanpItems)
        {
            foreach (var mv in v.Value)
            {
                if (mv.Value.serialnum == new_serialnum)
                {
                    new_frame = v.Key;
                    new_bagindex = mv.Key;
                }
            }
        }
        KnapsakItem new_ksi = KnapSackInfo.inst.allKanpItems[new_frame][new_bagindex];
        KnapSackInfo.inst.allKanpItems[new_frame].Remove(new_bagindex);

        Byte old_frame = 0;
        UInt16 old_bagindex = 0;
        foreach (var v in KnapSackInfo.inst.allKanpItems)
        {
            foreach (var mv in v.Value)
            {
                if (mv.Value.serialnum == old_serialnum)
                {
                    old_frame = v.Key;
                    old_bagindex = mv.Key;
                }
            }
        }
        KnapsakItem old_ksi = KnapSackInfo.inst.allKanpItems[old_frame][old_bagindex];
        KnapSackInfo.inst.allKanpItems[old_frame].Remove(old_bagindex);

        old_ksi.bagFrameIndex = new_frame;
        old_ksi.bagItemIndex = new_bagindex;
        new_ksi.bagFrameIndex = old_frame;
        new_ksi.bagItemIndex = old_bagindex;
        KnapSackInfo.inst.allKanpItems[old_frame].Add(old_bagindex, new_ksi);
        KnapSackInfo.inst.allKanpItems[new_frame].Add(new_bagindex, old_ksi);
    }

    public void DropBagItem(UnityEngine.GameObject dropGameObject)
    {
        Monitor.Enter(KBEngineApp.app.entities);
        KBEngine.Avatar player = (KBEngine.Avatar)KBEngineApp.app.player();
        UInt64 serialnum = itemsDic[dropGameObject];
        player.DropBagItem(serialnum);
        Monitor.Exit(KBEngineApp.app.entities);
        Byte frame = 0;
        UInt16 bagindex = 0;
        foreach (var v in KnapSackInfo.inst.allKanpItems)
        {
            foreach (var mv in v.Value)
            {
                if (mv.Value.serialnum == serialnum)
                {
                    frame = v.Key;
                    bagindex = mv.Key;
                }
            }
        }
        KnapSackInfo.inst.allKanpItems[frame].Remove(bagindex);
    }

    public void EquipItem(Byte index, UnityEngine.GameObject ego)
    {
        Monitor.Enter(KBEngineApp.app.entities);
        KBEngine.Avatar player = (KBEngine.Avatar)KBEngineApp.app.player();
        UInt64 serialnum = itemsDic[ego];
        player.EquipItem(serialnum, index);
        Monitor.Exit(KBEngineApp.app.entities);
        Byte frame = 0;
        UInt16 bagindex = 0;
        foreach (var v in KnapSackInfo.inst.allKanpItems)
        {
            foreach (var mv in v.Value)
            {
                if (mv.Value.serialnum == serialnum)
                {
                    frame = v.Key;
                    bagindex = mv.Key;
                }
            }
        }
        KnapSackInfo.inst.allKanpItems[frame].Remove(bagindex);
    }
}

public class GoodsItem
{
    public string id;
    public int index;
    public string name;
    public int type;
    public string icon;
}