using UnityEngine;
using KBEngine;
using System; 
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class game_ui_autopos : MonoBehaviour 
{
    public static Dictionary<UInt32, string> DesDic = new Dictionary<UInt32, string>{	
    {1,"test"},
	{2,"test"},
	{3,"test"},
	{4,"test"},
	{5,"test"},
	{6,"test"},
	{7,"test"},
	{8,"test"},
	{9,"test"},
	{10,"test"},
	{11,"test"},
	{12,"test"},
	{13,"test"},
	{14,"test"},
	{15,"test"}
    };

    public static Dictionary<UInt32, string> PowerDic = new Dictionary<UInt32, string>{	
    {1,"test"},
	{2,"test"},
	{3,"test"},
	{4,"test"},
	{5,"test"},
	{6,"test"},
	{7,"test"},
	{8,"test"},
	{9,"test"},
	{10,"test"},
	{11,"test"},
	{12,"test"},
	{13,"test"},
	{14,"test"},
	{15,"test"}
    };
	public Transform minimapCamera = null;
	public Transform minimapPoint = null;
	public UILabel minimap_pos = null;
    UnityEngine.GameObject minimap = null;
	
	public static UILabel hp_label = null;
	public static UILabel mp_label = null;
	public static UILabel target_hp_label = null;
	public static UILabel target_mp_label = null;
	
	public static UISlider hp_ProgressBar = null;
	public static UISlider target_hp_ProgressBar = null;
	public static UISlider mp_ProgressBar = null;
	public static UISlider target_mp_ProgressBar = null;

    public static UISlider exp_ProgressBar;
    public static UILabel exp_label;
	
	public static UILabel name_label = null;
	public static UILabel target_name_label = null;
	public static UILabel level_label = null;
	public static UILabel target_level_label = null;
	
	public static UnityEngine.GameObject target_bar = null;
	public static UnityEngine.GameObject ui_relive = null;

    public static UISkillDragDropContainer F1;
    public static UISkillDragDropContainer F2;
    public static UISkillDragDropContainer F3;
    public static UISkillDragDropContainer F4;
    public static UISkillDragDropContainer F5;
    public static UISkillDragDropContainer F6;


    //快捷物品
    public static UISkillDragDropContainer K1;
    public static UISkillDragDropContainer K2;
    public static UISkillDragDropContainer K3;
    public static UISkillDragDropContainer K4;
    public static UISkillDragDropContainer K5;
    public static UISkillDragDropContainer K6;

    //信息弹框
    public static UnityEngine.GameObject tipsInfo;
    public static UILabel name;
    public static UILabel des;
    public static UILabel power;
    public static UILabel price;

    //技能面板
    public static UnityEngine.GameObject skillUIbtn;
    public static UnityEngine.GameObject skillui;
    public static bool isShowSkillUI = false;

    //人物属性面板
    public static UnityEngine.GameObject avatorUIbtn;
    public static UnityEngine.GameObject avatorUI;
    public static bool isShowAvatorUI = false;

    //背包面板
    public static UnityEngine.GameObject BagUIbtn;
    public static UnityEngine.GameObject BagUI;
    public static bool isShowBagUI = false;
	
	void Awake ()     
	{
	}
	
    void OnDisable()
    {
        UI_KeyManager.combeKeysAlt_E -= onAlt_E;
        UI_KeyManager.combeKeysAlt_C -= onAlt_C;
    }
    void OnEnable()
    {
        UI_KeyManager.combeKeysAlt_E += onAlt_E;
        UI_KeyManager.combeKeysAlt_C += onAlt_C;
    }

    void onAlt_E(){
        ShowSkillUI(null);
    }

    void onAlt_C()
    {
        ShowAvatorUI(null);
    }

    void ShowSkillUI(UnityEngine.GameObject go)
    {
        if (isShowSkillUI == true)
        {
            skillui.SetActive(false);
            isShowSkillUI = false;
        }
        else
        {
            skillui.SetActive(true);
            isShowSkillUI = true;
        }
    }

    void ShowAvatorUI(UnityEngine.GameObject go)
    {
        if (isShowAvatorUI == true)
        {
            avatorUI.SetActive(false);
            isShowAvatorUI = false;
        }
        else
        {
            avatorUI.SetActive(true);
            isShowAvatorUI = true;
        }
    }

    void ShowBagUI(UnityEngine.GameObject go)
    {
        if (isShowBagUI == true)
        {
            BagUI.SetActive(false);
            isShowBagUI = false;
        }
        else
        {
            BagUI.SetActive(true);
            isShowBagUI = true;
        }
    }

	// Use this for initialization
	void Start () {
        //信息弹框
        tipsInfo = transform.Find("Tips/popInfo").gameObject;
        name = tipsInfo.transform.Find("name").GetComponent<UILabel>();
        des = tipsInfo.transform.Find("des").GetComponent<UILabel>();
        power = tipsInfo.transform.Find("power").GetComponent<UILabel>();
        price = tipsInfo.transform.Find("price").GetComponent<UILabel>();
        tipsInfo.SetActive(false);

        //技能
        skillui = transform.Find("skillUI").gameObject;
        skillui.SetActive(false);
        skillUIbtn = transform.Find("sysctrls/Container/Scroll View/Grid/sysctrl_skill").gameObject;
        UIEventListener.Get(skillUIbtn).onClick = ShowSkillUI;

        //人物属性
        avatorUI = transform.Find("avatorUI").gameObject;
        avatorUI.SetActive(false);
        avatorUIbtn = transform.Find("sysctrls/Container/Scroll View/Grid/sysctrl_avatar").gameObject;
        UIEventListener.Get(avatorUIbtn).onClick = ShowAvatorUI;

        //背包
        BagUIbtn = transform.Find("sysctrls/Container/Scroll View/Grid/sysctrl_bag").gameObject;
        BagUI = transform.Find("knapsackUI").gameObject;
        BagUI.SetActive(false);
        UIEventListener.Get(BagUIbtn).onClick = ShowBagUI;

        F1 = transform.Find("sysitemboxs/ContainerF/sys_itemslotsF/Grid/sys_itemslotF1").GetComponent<UISkillDragDropContainer>();
        F1.keyEventName = KeyCode.F1;
        F2 = transform.Find("sysitemboxs/ContainerF/sys_itemslotsF/Grid/sys_itemslotF2").GetComponent<UISkillDragDropContainer>();
        F2.keyEventName = KeyCode.F2;
        F3 = transform.Find("sysitemboxs/ContainerF/sys_itemslotsF/Grid/sys_itemslotF3").GetComponent<UISkillDragDropContainer>();
        F3.keyEventName = KeyCode.F3;
        F4 = transform.Find("sysitemboxs/ContainerF/sys_itemslotsF/Grid/sys_itemslotF4").GetComponent<UISkillDragDropContainer>();
        F4.keyEventName = KeyCode.F4;
        F5 = transform.Find("sysitemboxs/ContainerF/sys_itemslotsF/Grid/sys_itemslotF5").GetComponent<UISkillDragDropContainer>();
        F5.keyEventName = KeyCode.F5;
        F6 = transform.Find("sysitemboxs/ContainerF/sys_itemslotsF/Grid/sys_itemslotF6").GetComponent<UISkillDragDropContainer>();
        F6.keyEventName = KeyCode.F6;

        minimap = transform.Find("minimap").gameObject;
        hp_label = transform.Find("my_power_bar/hp_ProgressBar/Label").GetComponent<UILabel>();
        mp_label = transform.Find("my_power_bar/mp_ProgressBar/Label").GetComponent<UILabel>();
        target_hp_label = transform.Find("target_power_bar/target_hp_ProgressBar/Label").GetComponent<UILabel>();
        target_mp_label = transform.Find("target_power_bar/target_mp_ProgressBar/Label").GetComponent<UILabel>();

        exp_ProgressBar = transform.Find("exp_ProgressBar").GetComponent<UISlider>();
        exp_label = exp_ProgressBar.transform.Find("Label").GetComponent<UILabel>();

        hp_ProgressBar = transform.Find("my_power_bar/hp_ProgressBar").GetComponent<UISlider>();
        target_hp_ProgressBar = transform.Find("target_power_bar/target_hp_ProgressBar").GetComponent<UISlider>();
        mp_ProgressBar = transform.Find("my_power_bar/mp_ProgressBar").GetComponent<UISlider>();
        target_mp_ProgressBar = transform.Find("target_power_bar/target_mp_ProgressBar").GetComponent<UISlider>();

        name_label = transform.Find("my_power_bar/ename").GetComponent<UILabel>();
        level_label = transform.Find("my_power_bar/elevel").GetComponent<UILabel>();
        target_name_label = transform.Find("target_power_bar/target_ename").GetComponent<UILabel>();
        target_level_label = transform.Find("target_power_bar/target_elevel").GetComponent<UILabel>();

        target_bar = transform.Find("target_power_bar").gameObject;
		hideTargetBar();
        ui_relive = transform.Find("relive").gameObject;
        UnityEngine.GameObject gobackrelive = ui_relive.transform.Find("gobackrelive").gameObject;
		UIEventListener.Get(gobackrelive).onClick = on_gobackreliveClick;
        UnityEngine.GameObject localrelive = ui_relive.transform.Find("localrelive").gameObject;
		UIEventListener.Get(localrelive).onClick = on_localreliveClick;  
		hideRelivePanel();
		
		KBEngine.Entity player = KBEngineApp.app.player();
		if(player != null && KBEEventProc.inst != null)
		{
			KBEEventProc.inst.set_HP(player, player.getDefinedPropterty("HP"));
			KBEEventProc.inst.set_HP_Max(player, player.getDefinedPropterty("HP_Max"));
			KBEEventProc.inst.set_MP(player, player.getDefinedPropterty("MP"));
			KBEEventProc.inst.set_MP_Max(player, player.getDefinedPropterty("MP_Max"));
			KBEEventProc.inst.set_level(player, player.getDefinedPropterty("level"));
			KBEEventProc.inst.set_name(player, player.getDefinedPropterty("name"));
            KBEEventProc.inst.set_exp(player, player.getDefinedPropterty("Exp"));
			
			if((SByte)player.getDefinedPropterty("state") == 1)
				showRelivePanel();
		}
	}
	
	void OnDestroy()
	{
		KBEngine.Event.deregisterOut(this);
        transform.Find("knapsackUI").GetComponent<knapsackUIMG>().DisableDelegates();
		reset();
	}
	
	void installEvents()
	{
	}
	
	void on_gobackreliveClick(UnityEngine.GameObject item)
	{
		Common.DEBUG_MSG("on_gobackreliveClick: " + item.name);
		KBEngine.Entity player = KBEngineApp.app.player();
		if(player != null && player.className == "Avatar")
		{
			((KBEngine.Avatar)player).relive(0);
			hideRelivePanel();
		}
	}

	void on_localreliveClick(UnityEngine.GameObject item)
	{
		Common.DEBUG_MSG("on_localreliveClick: " + item.name);
		KBEngine.Entity player = KBEngineApp.app.player();
		if(player != null && player.className == "Avatar")
		{
			((KBEngine.Avatar)player).relive(1);
			hideRelivePanel();
		}
	}
	
	public static void showRelivePanel()
	{
		if(ui_relive != null)
			NGUITools.SetActive(ui_relive, true);
	}
	
	public static void hideRelivePanel()
	{
		if(ui_relive != null)
			NGUITools.SetActive(ui_relive, false);
	}
	
	void OnGUI()
	{
        if(minimap == null)
            minimap = transform.Find("minimap").gameObject;
		//UnityEngine.GameObject minimap = UnityEngine.GameObject.Find("minimap");
		Vector3 pos = minimap.transform.localPosition;
		pos.x = (Screen.width / 2 - 78.0f);
		pos.y = (Screen.height / 2 - 10.0f);
		minimap.transform.localPosition = pos;
	}
	
	public static void showTargetBar(SceneEntityObject seo)
	{
		if(seo == null || seo.kbentity == null || target_bar == null)
			return;

		NGUITools.SetActive(target_bar, true);
		
		KBEngine.Entity entity = seo.kbentity;
		string name = (string)entity.getDefinedPropterty("name");
		object level = entity.getDefinedPropterty("level");
		object hp = entity.getDefinedPropterty("HP");
		object hpmax = entity.getDefinedPropterty("HP_Max");
		object mp = entity.getDefinedPropterty("MP");
		object mpmax = entity.getDefinedPropterty("MP_Max");
			
		target_name_label.text = name;
		if(level != null)
			target_level_label.text = "lv:" + level;
		else
			target_level_label.text = "";
		
		updateTargetBar_HP(hp, hpmax);
		updateTargetBar_MP(mp, mpmax);
	}

	public static void updatePower_Progress(UISlider progress, UILabel label, object v, object v_max)
	{
        if (progress == null || v == null || v_max == null)
            return;

        if ((Int32)v_max <= 0)
            return;

        // double va = (double)(Int64)v ;
        float pv;
        if (progress == exp_ProgressBar)
        {
            UInt64 uv = (UInt64)v;
            string y = uv.ToString();
            float z = float.Parse(y);
            //double va = (Int64)v.con;
            Debug.LogWarning("va is " + z);
            pv = z / (float)(Int32)v_max;
        }
        else
        {
            pv = (float)(Int32)v / (float)(Int32)v_max;
        }
        //float pv = (float)(Int32)v / (float)(Int32)v_max;
        //float pv = (float)(Int32)50 / (float)(Int32)v_max;
        //float pv = (float)(Int32)1000 / (float)(Int32)2000;
        progress.value = pv;

        label.text = v + "/" + v_max;


        //if(progress == null || v == null || v_max == null)
        //    return;
		
        //if((Int32)v_max <= 0)
        //    return;
		
        //float pv = (float)(Int32)v / (float)(Int32)v_max;
        //progress.sliderValue = pv;
		
        //label.text = v + "/" + v_max;
	}
	
	public static void updateTargetBar_HP(object hp, object hpmax)
	{
		game_ui_autopos.updatePower_Progress(game_ui_autopos.target_hp_ProgressBar, game_ui_autopos.target_hp_label, hp, hpmax);
	}

	public static void updateTargetBar_MP(object mp, object mpmax)
	{
		game_ui_autopos.updatePower_Progress(game_ui_autopos.target_mp_ProgressBar, game_ui_autopos.target_mp_label, mp, mpmax);
	}
	
	public static void hideTargetBar()
	{
		NGUITools.SetActive(target_bar, false);
	}
	
	public static void updatePlayerBar_HP(object hp, object hpmax)
	{
		game_ui_autopos.updatePower_Progress(game_ui_autopos.hp_ProgressBar, game_ui_autopos.hp_label, hp, hpmax);
	}

    //added by zcg
    public static void updatePlayerBar_Exp(object exp, object expmax)
    {
        game_ui_autopos.updatePower_Progress(game_ui_autopos.exp_ProgressBar, game_ui_autopos.exp_label, exp, expmax);
    }

	public static void updatePlayerBar_MP(object mp, object mpmax)
	{
		game_ui_autopos.updatePower_Progress(game_ui_autopos.mp_ProgressBar, game_ui_autopos.mp_label, mp, mpmax);
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void reset()
	{
		if(UnityEngine.GameObject.Find("minimap") == null)
			return;
		
		UnityEngine.GameObject minimapCameraObj = UnityEngine.GameObject.Find("minimapCamera");
		if(minimapCameraObj != null)
		{
			minimapCamera = minimapCameraObj.transform;
			minimapPoint = UnityEngine.GameObject.Find("minimap_point").transform;
			minimap_pos = UnityEngine.GameObject.Find("minimap_pos").GetComponent<UILabel>();
			
			Camera c = minimapCameraObj.GetComponent<Camera>();
			Material materialTex = UnityEngine.GameObject.Find("minimap_texture").GetComponent<UITexture>().material;
			c.targetTexture = materialTex.GetTexture("_MainTex") as RenderTexture;
			c.isOrthoGraphic = true;
			c.orthographicSize = 128;
			c.orthographic = true;
			
			UnityEngine.GameObject minimap_frame = UnityEngine.GameObject.Find("minimap_frame");
			minimap_frame.GetComponent<UITexture>().mainTexture = materialTex.GetTexture("_Mask");
			Vector3 scale = UnityEngine.GameObject.Find("minimap_texture").transform.localScale;
			
			minimap_frame.transform.localScale = scale;
		}
	}

    public static void ShowTips(string vname, UInt32 typeid, UInt32 powerinfo, UInt32 vprice, Vector3 position)
    {
        tipsInfo.SetActive(true);
        tipsInfo.transform.position = new Vector3(position.x, position.y, position.z);
        name.text = vname;
        des.text = DesDic[typeid];
        power.text = PowerDic[powerinfo];
        price.text = "出售价格：" + vprice;
    }

    public static void HideTips()
    {
        tipsInfo.SetActive(false);
    }
	
	void FixedUpdate()
	{
		if(RPG_Animation.instance != null)
		{
			if(minimapCamera != null)
			{
				Vector3 playerpos = RPG_Animation.instance.transform.position;
				playerpos.y += 256f;
				
				minimapCamera.position = playerpos;
				minimap_pos.text = ((int)playerpos.x) + ":" + ((int)playerpos.z);
				minimapPoint.rotation = Quaternion.Euler(0, 0, -RPG_Animation.instance.transform.rotation.eulerAngles.y);
			}
			else
			{
				reset();
			}
		}
	}
}
