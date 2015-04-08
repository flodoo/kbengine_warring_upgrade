using UnityEngine;
using System.Collections;
using System;

[AddComponentMenu("MyDropDrag/Knapsack/knapsackDragDropItem")]
public class knapsackDragDropItem : UIDragDropItem
{
    //public Byte index = 0;
    public UInt64 serialnum = 0;
    public Byte stdMode = 0;
    public Byte func = 0;
    protected override void OnDragDropRelease(GameObject surface)
    {
        //base.OnDragDropRelease(surface);
        Debug.LogWarning("on drag release");
        if (mButton != null) mButton.isEnabled = true;
        else if (mCollider != null) mCollider.enabled = true;
        else if (mCollider2D != null) mCollider2D.enabled = true;
        if (surface.transform.parent == null)
        {
            Debug.LogWarning("diu qi");
            knapsackUIMG.inst.DropBagItem(gameObject);
            NGUITools.Destroy(gameObject);
            game_ui_autopos.HideTips();
            return;
        }
        //装备
        if (surface.tag == "knapsackEquipFrame")
        {
            if (stdMode == 1)
            {
                UIEquipmentContainer ec = surface.GetComponent<UIEquipmentContainer>();
                if (ec != null && ec.eqgo == null && ec.funcCode == func)
                {
                    Debug.LogWarning("equip");
                    knapsackUIMG.inst.EquipItem(ec.index, gameObject);
                    NGUITools.Destroy(gameObject);
                    game_ui_autopos.HideTips();
                    //transform.parent = surface.transform;
                    //transform.localPosition = Vector3.zero;
                    //transform.localScale = Vector3.one;
                    return;
                }
            }
        }

        if (surface.tag != "knapsackFrame" && surface.tag != "knapsackItem")
        {
            Debug.LogWarning("on drag return");
            Debug.LogWarning("surface is " + surface.transform.parent);
            mTrans.parent = mParent;
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            OnDragDropEnd();
            return;
        }
        if (surface.tag == "knapsackFrame")
        {
            Debug.LogWarning("on drag release");
            if (surface.transform.childCount > 0)
            {
                mTrans.parent = mParent;
                transform.localPosition = Vector3.zero;
                transform.localScale = Vector3.one;
                return;
            }
            knapsackUIMG.inst.SetBagItemPos(surface, gameObject);
            transform.parent = surface.transform;
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
        }
        else if (surface.tag == "knapsackItem")
        {
            knapsackUIMG.inst.ReplaceBagItemPos(surface, gameObject);
            Transform selfParent = mParent;
            transform.parent = surface.transform.parent;
            surface.transform.parent = selfParent;
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            surface.transform.localPosition = Vector3.zero;
            surface.transform.localScale = Vector3.one;
        }
        
    }
}
