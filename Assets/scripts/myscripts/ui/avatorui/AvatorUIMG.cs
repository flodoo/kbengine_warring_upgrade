using UnityEngine;
using System.Collections;
using KBEngine;
using System.Collections.Generic;

public class AvatorUIMG : MonoBehaviour {
    //public  ActionSetProperty<object> onChangehp;
    //public  ActionSetProperty<object> onChangemp;
    //public  ActionSetProperty<object> onChangehpmax;
    //public  ActionSetProperty<object> onChangempmax;
    //public  ActionSetProperty<object> onChangeexp;
    //public  ActionSetProperty<object> onChangelevel;
    //public  ActionSetProperty<object> onChangePhyAtack;
    //public  ActionSetProperty<object> onChangeMagicAtack;
    //public  ActionSetProperty<object> onChangePhyDef;
    //public  ActionSetProperty<object> onChangeMagicDef;

    public UILabel PhyDef;
    public UILabel MagicAtack;
    public UILabel MagicDef;
    public UILabel PhyAtack;
    public UILabel level;
    public UILabel exp;
    public UILabel mp;
    public UILabel hp;
    public UILabel name;
    KBEngine.Entity player;
    void InitAvatorInfo()
    {
        player = KBEngineApp.app.player();
        if (player != null && KBEEventProc.inst != null)
        {
            name.text = player.getDefinedPropterty("name") + "";
            PhyDef.text = player.getDefinedPropterty("PhyDef") + "";
            MagicAtack.text = player.getDefinedPropterty("MagicAtack") + "";
            MagicDef.text = player.getDefinedPropterty("MagicDef") + "";
            PhyAtack.text = player.getDefinedPropterty("PhyAtack") + "";
            level.text = player.getDefinedPropterty("level") + "";
            exp.text = player.getDefinedPropterty("Exp") + "";
            mp.text = player.getDefinedPropterty("MP") + "/" + player.getDefinedPropterty("MP_Max");
            hp.text = player.getDefinedPropterty("HP") + "/" + player.getDefinedPropterty("HP_Max");

        }
        //name.text = AvatorInfo.inst.name;
        //PhyDef.text = AvatorInfo.inst.phdef + "";
        //MagicAtack.text = AvatorInfo.inst.magicattack + "";
        //MagicDef.text = AvatorInfo.inst.madef + "";
        //PhyAtack.text = AvatorInfo.inst.phattack + "";
        //level.text = AvatorInfo.inst.level + "";
        //exp.text = AvatorInfo.inst.exp + "";
        //mp.text = AvatorInfo.inst.mp + "/" + AvatorInfo.inst.mpmax;
        //hp.text = AvatorInfo.inst.hp + "/" + AvatorInfo.inst.hpmax;
    }
    void Awake()
    {
        PhyDef = transform.Find("bg/PhyDefLabel").GetComponent<UILabel>();
        MagicAtack = transform.Find("bg/MagicAtackLabel").GetComponent<UILabel>();
        MagicDef = transform.Find("bg/MagicDefLabel").GetComponent<UILabel>();
        PhyAtack = transform.Find("bg/PhyAtackLabel").GetComponent<UILabel>();
        level = transform.Find("bg/levelLabel").GetComponent<UILabel>();
        exp = transform.Find("bg/expLabel").GetComponent<UILabel>();
        mp = transform.Find("bg/mpLabel").GetComponent<UILabel>();
        hp = transform.Find("bg/hpLabel").GetComponent<UILabel>();
        name = transform.Find("bg/nameLabel").GetComponent<UILabel>();
    }
    void OnEnable()
    {
        InitAvatorInfo();
        EnableDelegates();
    }
    void OnDisable()
    {
        DisableDelegates();
    }

	// Update is called once per frame
	void Update () {
	
	}
    void EnableDelegates()
    {
        KBEEventProc.onChangename += onChangename;
        KBEEventProc.onChangehp += onChangehp;
        KBEEventProc.onChangemp +=onChangemp;
        KBEEventProc.onChangehpmax +=onChangehpmax;
        KBEEventProc.onChangempmax +=onChangempmax;
        KBEEventProc.onChangeexp +=onChangeexp;
        KBEEventProc.onChangelevel +=onChangelevel;
        KBEEventProc.onChangePhyAtack +=onChangePhyAtack;
        KBEEventProc.onChangeMagicAtack +=onChangeMagicAtack;
        KBEEventProc.onChangePhyDef +=onChangePhyDef;
        KBEEventProc.onChangeMagicDef += onChangeMagicDef;
    }
    void DisableDelegates()
    {
        KBEEventProc.onChangename -= onChangename;
        KBEEventProc.onChangehp -= onChangehp;
        KBEEventProc.onChangemp -= onChangemp;
        KBEEventProc.onChangehpmax -= onChangehpmax;
        KBEEventProc.onChangempmax -= onChangempmax;
        KBEEventProc.onChangeexp -= onChangeexp;
        KBEEventProc.onChangelevel -= onChangelevel;
        KBEEventProc.onChangePhyAtack -= onChangePhyAtack;
        KBEEventProc.onChangeMagicAtack -= onChangeMagicAtack;
        KBEEventProc.onChangePhyDef -= onChangePhyDef;
        KBEEventProc.onChangeMagicDef -= onChangeMagicDef;
    }

    void onChangename(object o)
    {
        name.text = o+"";
    }

    void onChangehp(object o)
    {
        hp.text = o + "/" + player.getDefinedPropterty("HP_Max");
    }
    void onChangemp(object o)
    {
        mp.text = o + "/" + player.getDefinedPropterty("MP_Max");
    }
    void onChangehpmax(object o)
    {
        hp.text = player.getDefinedPropterty("HP")+"/"+o;
    }
    void onChangempmax(object o)
    {
        mp.text =  player.getDefinedPropterty("MP")+"/"+o;
    }
    void onChangeexp(object o)
    {
        exp.text = o + "";
    }
    void onChangelevel(object o)
    {
        level.text = o + "";
    }
    void onChangePhyAtack(object o)
    {
        PhyAtack.text = o + "";
    }
    void onChangeMagicAtack(object o)
    {
        MagicAtack.text = o + "";
    }
    void onChangePhyDef(object o)
    {
        PhyDef.text = o + "";
    }
    void onChangeMagicDef(object o)
    {
        MagicDef.text = o + "";
    }
}
