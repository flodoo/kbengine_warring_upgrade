using UnityEngine;
using KBEngine;
using System.Collections;
using System;
using System.Xml;
using System.Collections.Generic;

public delegate void ActionSetProperty<T>(T obj);
public delegate void ActionKnapsackOp<T>(T obj1, T obj2);
public class KBEEventProc
{
    public static Dictionary<UInt16, Int32> Exp = new Dictionary<UInt16, Int32>{	
    {1,50},
	{2,100},
	{3,200},
	{4,300},
	{5,500},
	{6,1000},
	{7,1500},
	{8,2500},
	{9,5000},
	{10,8000},
	{11,12000},
	{12,20000},
	{13,30000},
	{14,50000},
	{15,100000}
    };
	public static KBEEventProc inst = null;
	public static bool enterSpace = false;
	public static List<Int32> pendingEnterEntityIDs = new List<Int32>();

    public static ActionSetProperty<object> onChangename;
    public static ActionSetProperty<object> onChangehp;
    public static ActionSetProperty<object> onChangemp;
    public static ActionSetProperty<object> onChangehpmax;
    public static ActionSetProperty<object> onChangempmax;
    public static ActionSetProperty<object> onChangeexp;
    public static ActionSetProperty<object> onChangelevel;
    public static ActionSetProperty<object> onChangePhyAtack;
    public static ActionSetProperty<object> onChangeMagicAtack;
    public static ActionSetProperty<object> onChangePhyDef;
    public static ActionSetProperty<object> onChangeMagicDef;
    public static ActionKnapsackOp<object> onAddBagItem;
    public static ActionKnapsackOp<object> onDelBagItem;
    public static ActionKnapsackOp<object> onEquiptedItem;
	
	public KBEEventProc()
	{
		KBEEventProc.inst = this;
		KBEngine.Event.registerOut("addSpaceGeometryMapping", KBEEventProc.inst, "addSpaceGeometryMapping");
		
		KBEngine.Event.registerOut("onAvatarEnterWorld", KBEEventProc.inst, "onAvatarEnterWorld");
		KBEngine.Event.registerOut("onEnterWorld", KBEEventProc.inst, "onEnterWorld");
		KBEngine.Event.registerOut("onLeaveWorld", KBEEventProc.inst, "onLeaveWorld");
		
		KBEngine.Event.registerOut("set_HP", KBEEventProc.inst, "set_HP");
		KBEngine.Event.registerOut("set_MP", KBEEventProc.inst, "set_MP");
		KBEngine.Event.registerOut("set_HP_Max", KBEEventProc.inst, "set_HP_Max");
		KBEngine.Event.registerOut("set_MP_Max", KBEEventProc.inst, "set_MP_Max");
        KBEngine.Event.registerOut("set_exp", KBEEventProc.inst, "set_exp");
        KBEngine.Event.registerOut("set_PhyAtack", KBEEventProc.inst, "set_PhyAtack");
        KBEngine.Event.registerOut("set_MagicAtack", KBEEventProc.inst, "set_MagicAtack");
        KBEngine.Event.registerOut("set_PhyDef", KBEEventProc.inst, "set_PhyDef");
        KBEngine.Event.registerOut("set_MagicDef", KBEEventProc.inst, "set_MagicDef");
		KBEngine.Event.registerOut("set_level", KBEEventProc.inst, "set_level");
		KBEngine.Event.registerOut("set_name", KBEEventProc.inst, "set_name");
		KBEngine.Event.registerOut("set_state", KBEEventProc.inst, "set_state");
		KBEngine.Event.registerOut("set_moveSpeed", KBEEventProc.inst, "set_moveSpeed");
		KBEngine.Event.registerOut("set_modelScale", KBEEventProc.inst, "set_modelScale");
		KBEngine.Event.registerOut("set_modelID", KBEEventProc.inst, "set_modelID");
		KBEngine.Event.registerOut("set_position", KBEEventProc.inst, "set_position");
		KBEngine.Event.registerOut("set_direction", KBEEventProc.inst, "set_direction");
		KBEngine.Event.registerOut("update_position", KBEEventProc.inst, "update_position");
		KBEngine.Event.registerOut("recvDamage", KBEEventProc.inst, "recvDamage");
		KBEngine.Event.registerOut("otherAvatarOnJump", KBEEventProc.inst, "otherAvatarOnJump");
        KBEngine.Event.registerOut("AddBagItem", KBEEventProc.inst, "AddBagItem");
        KBEngine.Event.registerOut("DelBagItem", KBEEventProc.inst, "DelBagItem");
        KBEngine.Event.registerOut("EquiptedItem", KBEEventProc.inst, "EquiptedItem");
	}
	
	public void addSpaceGeometryMapping(string respath)
	{
		loader.inst.enterScene(respath);
		enterSpace = true;
		
		foreach(Int32 id in pendingEnterEntityIDs)
		{
			KBEngine.Entity entity = KBEngineApp.app.findEntity(id);
			if(entity != null)
				onEnterWorld(entity);
		}
		
		pendingEnterEntityIDs.Clear();
	}
	
	public void onAvatarEnterWorld(UInt64 rndUUID, Int32 eid, KBEngine.Avatar avatar)
	{
		onEnterWorld(avatar);
	}
	
	public void onEnterWorld(KBEngine.Entity entity)
	{
		if(enterSpace == false)
		{
			pendingEnterEntityIDs.Add(entity.id);
			return;
		}
        SceneEntityObject obj = SceneEntityObjectManager.CreateSceneEntityObject(entity);
	}
	
	public void onLeaveWorld(KBEngine.Entity entity)
	{
		if(enterSpace == false)
		{
			pendingEnterEntityIDs.Remove(entity.id);
			return;
		}
		
		Scene scene = null;
		if(!loader.inst.scenes.TryGetValue(loader.inst.currentSceneName, out scene))
		{
			Common.ERROR_MSG("KBEEventProc::onLeaveWorld: not found scene(" + loader.inst.currentSceneName + ")!");
			return;
		}
		
		scene.removeSceneObject("_entity_" + entity.id); 
		((SceneEntityObject)entity.renderObj).kbentity = null;
		entity.renderObj = null;
	}
	
	public void set_position(KBEngine.Entity entity)
	{
		if(entity.renderObj != null)
		{
			Common.calcPositionY(entity.position, out entity.position.y, false);
			((SceneObject)entity.renderObj).position = entity.position;
            ((SceneEntityObject)entity.renderObj).destPosition = entity.position;
		}
	
		if(entity.isPlayer() == false)
			return;
		
		entity.position = (Vector3)entity.getDefinedPropterty("position");
		
		RPG_Controller.initPos.x = entity.position.x;
		RPG_Controller.initPos.y = entity.position.y;
		RPG_Controller.initPos.z = entity.position.z;

		if(RPG_Controller.instance != null)
		{
			RPG_Controller.instance.transform.position = RPG_Controller.initPos;
			Common.DEBUG_MSG("KBEEventProc::set_position: entity.position=" + entity.position + " " + entity.getDefinedPropterty("position") + ", RPG_Controller.position=" + RPG_Controller.instance.transform.position);
		}
	}

	public void update_position(KBEngine.Entity entity)
	{
		if(enterSpace == false)
			return;
		
		if(entity.renderObj != null)
		{
            ((SceneEntityObject)entity.renderObj).destPosition = (entity.position);
		}
	}
	
	public void set_direction(KBEngine.Entity entity)
	{
		if(enterSpace == false)
			return;
		
		if(entity.isPlayer() == false)
		{
			if(entity.renderObj != null)
			{
                ((SceneEntityObject)entity.renderObj).destDirection = new Vector3(entity.direction.y, entity.direction.z, entity.direction.x);
				//((SceneObject)entity.renderObj).rotation = new Vector3(entity.direction.y, entity.direction.z, entity.direction.x);
			}
			return;
		}
	
		RPG_Controller.initRot = new Vector3(entity.direction.y, entity.direction.z, entity.direction.x);
		if(RPG_Controller.instance != null)
		{
			RPG_Controller.instance.transform.Rotate(RPG_Controller.rotation);
			Common.DEBUG_MSG("KBEEventProc::set_direction: RPG_Controller.rotation=" + RPG_Controller.instance.transform.rotation);
		}
	}

	public void set_HP(KBEngine.Entity entity, object v)
	{
		object vv = entity.getDefinedPropterty("HP_Max");

		if(entity.renderObj != null)
		{
			object oldvv = entity.getDefinedPropterty("old_HP");
			if(oldvv != null)
			{
				Int32 diff = (Int32)vv - (Int32)oldvv;
			
				if(diff != 0)
				{
                    ((SceneMonsterObject)entity.renderObj).addHP(diff);
				}
				
				entity.setDefinedPropterty("old_HP", vv);
			}
			else
				entity.addDefinedPropterty("old_HP", vv);
		}
		
		if(entity.isPlayer())
		{
			game_ui_autopos.updatePlayerBar_HP(v, vv);
            if (onChangehp != null)
                onChangehp(v);
		}
		else
		{
			game_ui_autopos.updateTargetBar_HP(v, vv);
		}
       
	}
	
	public void set_MP(KBEngine.Entity entity, object v)
	{
		object vv = entity.getDefinedPropterty("MP_Max");

		if(entity.renderObj != null)
		{
			object oldvv = entity.getDefinedPropterty("old_MP");
			if(oldvv != null)
			{
				Int32 diff = (Int32)vv - (Int32)oldvv;
				
				if(diff > 0)
				{
                    ((SceneMonsterObject)entity.renderObj).addMP(diff);
				}
				
				entity.setDefinedPropterty("old_MP", vv);
			}
			else
				entity.addDefinedPropterty("old_MP", vv);
		}
		
		if(entity.isPlayer())
		{
			game_ui_autopos.updatePlayerBar_MP(v, vv);
            if (onChangemp != null)
                onChangemp(v);
		}
		else
		{
			game_ui_autopos.updateTargetBar_MP(v, vv);
		}
        
	}
	
	public void set_HP_Max(KBEngine.Entity entity, object v)
	{
		object vv = entity.getDefinedPropterty("HP");
		if(entity.isPlayer())
		{
			game_ui_autopos.updatePlayerBar_HP(vv, v);
            if (onChangehpmax != null)
                onChangehpmax(v);
		}
		else
		{
			game_ui_autopos.updateTargetBar_HP(vv, v);
		}
        
	}
	
	public void set_MP_Max(KBEngine.Entity entity, object v)
	{
		object vv = entity.getDefinedPropterty("MP");
		if(entity.isPlayer())
		{
			game_ui_autopos.updatePlayerBar_MP(vv, v);
            if (onChangempmax != null)
                onChangempmax(v);
		}
		else
		{
			game_ui_autopos.updateTargetBar_MP(vv, v);
		}
        
	}
	
	public void set_level(KBEngine.Entity entity, object v)
	{
		if(game_ui_autopos.level_label == null)
			return;
		
		if(entity.isPlayer())
		{
			game_ui_autopos.level_label.text = "lv: " + v;
            if (onChangelevel != null)
                onChangelevel(v);
		}
		else
		{
			game_ui_autopos.target_level_label.text = "lv:" + v;
		}
        
	}

    public void set_exp(KBEngine.Entity entity, object v)
    {
        //Debug.Log("set Exp ");
        if (!entity.isPlayer())
        {
            return;
        }
        // Debug.Log("set Exp " + v);
        object level = entity.getDefinedPropterty("level");
        Debug.LogWarning("level is " + (UInt16)level);
        object vv = Exp[(UInt16)level];
        //object vv = 2000;
        Debug.LogWarning("vv is " + (float)(Int32)vv);
        if (entity.isPlayer())
        {
            game_ui_autopos.updatePlayerBar_Exp(v, vv);
            if (onChangeexp != null)
                onChangeexp(v);
        }
        
    }

    public void set_PhyAtack(KBEngine.Entity entity, object v)
    {
        if (!entity.isPlayer())
        {
            return;
        }
        if (onChangePhyAtack != null)
            onChangePhyAtack(v);
        
    }
    public void set_MagicAtack(KBEngine.Entity entity, object v)
    {
        if (!entity.isPlayer())
        {
            return;
        }
        if (onChangeMagicAtack != null)
            onChangeMagicAtack(v);

    }
    public void set_PhyDef(KBEngine.Entity entity, object v)
    {
        if (!entity.isPlayer())
        {
            return;
        }
        if (onChangePhyDef != null)
            onChangePhyDef(v);

    }
    public void set_MagicDef(KBEngine.Entity entity, object v)
    {
        if (!entity.isPlayer())
        {
            return;
        }
        if (onChangeMagicDef != null)
            onChangeMagicDef(v);

    }

	public void set_name(KBEngine.Entity entity, object v)
	{
		if(entity.isPlayer())
		{
            if (onChangename != null)
                onChangename(v);
			if(game_ui_autopos.name_label == null)
				return;
			
			game_ui_autopos.name_label.text = (string)v;
            
		}
		else
		{
			if(entity.renderObj != null)
			{
				((SceneEntityObject)entity.renderObj).setName((string)v);
			}
		}
        
	}
	
	public void set_state(KBEngine.Entity entity, object v)
	{
		if(enterSpace == false)
			return;
		
		if(entity.renderObj != null)
		{
            ((SceneMonsterObject)entity.renderObj).set_state((SByte)v);
		}
		
		if(entity.isPlayer())
		{
			RPG_Controller.enabled = ((SByte)v) != 1;
			
			if(RPG_Controller.enabled == false)
				game_ui_autopos.showRelivePanel();
			else
				game_ui_autopos.hideRelivePanel();
		}
	}

	public void set_moveSpeed(KBEngine.Entity entity, object v)
	{
		if(enterSpace == false)
			return;
		
		float fspeed = ((float)(Byte)v) / 10f;
		
		if(entity.isPlayer())
		{
			if(RPG_Controller.instance != null)
				RPG_Controller.instance.walkSpeed = fspeed;
		}
		
		if(entity.renderObj != null)
		{
            ((SceneMonsterObject)entity.renderObj).set_moveSpeed(fspeed);
		}
	}
	
	public void set_modelScale(KBEngine.Entity entity, object v)
	{
		if(enterSpace == false)
			return;

		if(entity.renderObj != null)
		{
			float scale = (((float)(Byte)v) / 10.0f);
			((SceneEntityObject)entity.renderObj).scale = new Vector3(scale, scale, scale);
		}
	}
	
	public void set_modelID(KBEngine.Entity entity, object v)
	{
		if(enterSpace == false)
			return;
	}
	
	public void recvDamage(KBEngine.Entity entity, KBEngine.Entity attacker, Int32 skillID, Int32 damageType, Int32 damage)
	{
		if(enterSpace == false)
			return;

		if(attacker != null)
		{
			if(attacker.renderObj != null)
			{
                ((SceneMonsterObject)attacker.renderObj).attack(skillID, damageType, ((SceneEntityObject)entity.renderObj));
			}
		}

		if(entity.renderObj != null)
		{
			if(CameraTargeting.inst != null && CameraTargeting.inst.lasttargetTransform == null && entity.isPlayer())
			{
				CameraTargeting.inst.setTarget(((SceneEntityObject)attacker.renderObj).gameEntity.transform);
				TargetManger.setTarget((SceneEntityObject)attacker.renderObj);
			}

            ((SceneMonsterObject)entity.renderObj).recvDamage(skillID, damageType, damage);
		}
	}
	
	public void otherAvatarOnJump(KBEngine.Entity entity)
	{
		if(entity.renderObj != null)
		{
            SceneMonsterObject seo = ((SceneMonsterObject)entity.renderObj);
			seo.stopPlayAnimation("");
			seo.playJumpAnimation();
		}
	}

    public void AddBagItem(KBEngine.Entity entity, object framIndex, object itemIndex)
    {
        if (!entity.isPlayer())
        {
            return;
        }
        Debug.Log("AddBagItemaaaaaaaaaaaaaaa");
        if (onAddBagItem != null)
            onAddBagItem(framIndex, itemIndex);
    }

    public void DelBagItem(KBEngine.Entity entity, object serialnum)
    {
        if (!entity.isPlayer())
        {
            return;
        }
        Debug.Log("DelBagItem");
        if (onDelBagItem != null)
            onDelBagItem(serialnum, null);
    }

    //装备
    public void EquiptedItem(KBEngine.Entity entity, object framIndex)
    {
        if (!entity.isPlayer())
        {
            return;
        }
        Debug.Log("EquiptedItem");
        if (onEquiptedItem != null)
            onEquiptedItem(framIndex, null);
    }
}

