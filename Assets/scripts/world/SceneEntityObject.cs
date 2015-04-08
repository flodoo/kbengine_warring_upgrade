using UnityEngine;
using KBEngine;
using System.Collections;
using System;
using System.Xml;
using System.Collections.Generic;

public class SceneEntityObject : SceneObject
{
	public SByte state = 0;
	//public GameEntityCtrl gameEntity = null;
    public EntityBehavior gameEntity = null;
	protected UnityEngine.GameObject hud_infosObj = null;
	public Hud_Infos hudinfos = null;
	public KBEngine.Entity kbentity = null;
	public bool isPlayer = false;
    public Vector3 destDirection = Vector3.zero;
    public Vector3 destPosition = Vector3.zero;
	
	public SceneEntityObject()
	{
	}

    ~SceneEntityObject()  // destructor
    {
    	gameEntity.seo = null;
    	gameEntity = null;
		
		if(hudinfos != null)
			hudinfos.seo = null;
    }
    
	public void setName(string name)
	{
		if(hud_infosObj != null)
		{
			hudinfos.setName(name); 
		}
	}
	
	
	
	public void create() 
	{
		// 设置默认的模型
		gameObject = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(loader.inst.defaultEntityAsset, position, Quaternion.Euler(eulerAngles));
		gameObject.name = name;
		gameObject.transform.localScale = scale;
		
		if(loader.inst.entityHudInfosAsset != null)
		{
			hud_infosObj = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(loader.inst.entityHudInfosAsset);
			hud_infosObj.name = "hud_infos";
			hudinfos = hud_infosObj.GetComponent<Hud_Infos>();
			hudinfos.seo = this;
			attachHeadInfo();
		}
		else
		{
			Common.WARNING_MSG("SceneEntityObject::Start: not found entityHudInfosAsset!");
		}
	}
	
	public void createPlayer() 
	{
		gameObject = UnityEngine.GameObject.Find("PlayerChar/entity");
		isPlayer = true;
	}
	
	public override void Instantiate()
	{
		asset.Instantiate(this, name, position, eulerAngles, scale);
	}
	
	protected void attachHeadInfo()
	{
		if(hud_infosObj == null)
			return;
		
        hud_infosObj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        
		hud_infosObj.transform.parent = gameObject.transform.FindChild("head_info");
		if(hud_infosObj.transform.parent == null)
		{
			hud_infosObj.transform.parent = gameObject.transform;
			hudinfos.offsetY = 3f;
		}
		
		hud_infosObj.transform.position = new Vector3(hud_infosObj.transform.parent.position.x, 
		hud_infosObj.transform.parent.position.y + hudinfos.offsetY, hud_infosObj.transform.parent.position.z);
	}
	
	public override void onAssetAsyncLoadObjectCB(string name, UnityEngine.Object obj, Asset asset)
	{
		this.name = asset.source;
		if(hud_infosObj != null)
			hud_infosObj.transform.parent = null;

		if(gameObject != null)
		{
			UnityEngine.GameObject.Destroy(gameObject);
			gameObject = null;
		}

		base.onAssetAsyncLoadObjectCB(name, obj, asset);

        gameEntity = gameObject.GetComponent<EntityBehavior>();
		gameEntity.seo = this;
		gameEntity.isPlayer = isPlayer;
	}
}

