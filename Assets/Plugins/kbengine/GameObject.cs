namespace KBEngine
{
  	using UnityEngine; 
	using System; 
	using System.Collections; 
	using System.Collections.Generic;
	
    public class GameObject : if_Entity_error_use______git_submodule_update_____kbengine_plugins_______open_this_file_and_I_will_tell_you 
    {
		public GameObject()
		{
		}

		public virtual void set_HP(object old)
		{
			object v = getDefinedPropterty("HP");
            AvatorInfo.inst.hp = (Int32)v;
			// Dbg.DEBUG_MSG(className + "::set_HP: " + old + " => " + v); 
			Event.fireOut("set_HP", new object[]{this, v});
		}
		
		public virtual void set_MP(object old)
		{
			object v = getDefinedPropterty("MP");
            AvatorInfo.inst.mp = (Int32)v;
			// Dbg.DEBUG_MSG(className + "::set_MP: " + old + " => " + v); 
			Event.fireOut("set_MP", new object[]{this, v});
		}
		
		public virtual void set_HP_Max(object old)
		{
			object v = getDefinedPropterty("HP_Max");
            AvatorInfo.inst.hpmax = (Int32)v;
			// Dbg.DEBUG_MSG(className + "::set_HP_Max: " + old + " => " + v); 
			Event.fireOut("set_HP_Max", new object[]{this, v});
		}
		
		public virtual void set_MP_Max(object old)
		{
			object v = getDefinedPropterty("MP_Max");
            AvatorInfo.inst.mpmax = (Int32)v;
			// Dbg.DEBUG_MSG(className + "::set_MP_Max: " + old + " => " + v); 
			Event.fireOut("set_MP_Max", new object[]{this, v});
		}
		
		public virtual void set_level(object old)
		{
			object v = getDefinedPropterty("level");
			// Dbg.DEBUG_MSG(className + "::set_level: " + old + " => " + v); 
            AvatorInfo.inst.level = (UInt16)v;
			Event.fireOut("set_level", new object[]{this, v});
		}

        public virtual void set_Exp(object old)
        {
            object v = getDefinedPropterty("Exp");
            Dbg.DEBUG_MSG(className + "::set_Exp: " + old + " => " + v);
            AvatorInfo.inst.exp = (UInt64)v;
            Event.fireOut("set_exp", new object[] { (KBEngine.Entity)this, v });
        }

        public virtual void set_PhyAtack(object old)
        {
            object v = getDefinedPropterty("PhyAtack");
            Dbg.DEBUG_MSG(className + "::set_PhyAtack: " + old + " => " + v);
            AvatorInfo.inst.phattack = (UInt32)v;
            Event.fireOut("set_PhyAtack", new object[] { (KBEngine.Entity)this, v });
        }

        public virtual void set_MagicAtack(object old)
        {
            object v = getDefinedPropterty("MagicAtack");
            Dbg.DEBUG_MSG(className + "::set_MagicAtack: " + old + " => " + v);
            AvatorInfo.inst.magicattack = (UInt32)v;
            Event.fireOut("set_MagicAtack", new object[] { (KBEngine.Entity)this, v });
        }

        public virtual void set_PhyDef(object old)
        {
            object v = getDefinedPropterty("PhyDef");
            Dbg.DEBUG_MSG(className + "::set_PhyDef: " + old + " => " + v);
            AvatorInfo.inst.phdef = (UInt32)v;
            Event.fireOut("set_PhyDef", new object[] { (KBEngine.Entity)this, v });
        }

        public virtual void set_MagicDef(object old)
        {
            object v = getDefinedPropterty("MagicDef");
            Dbg.DEBUG_MSG(className + "::set_MagicDef: " + old + " => " + v);
            AvatorInfo.inst.madef = (UInt32)v;
            Event.fireOut("set_MagicDef", new object[] { (KBEngine.Entity)this, v });
        }
		
		public virtual void set_name(object old)
		{
			object v = getDefinedPropterty("name");
            AvatorInfo.inst.name = (string)v;
			// Dbg.DEBUG_MSG(className + "::set_name: " + old + " => " + v); 
			Event.fireOut("set_name", new object[]{this, v});
		}
		
		public virtual void set_state(object old)
		{
			object v = getDefinedPropterty("state");
			// Dbg.DEBUG_MSG(className + "::set_state: " + old + " => " + v); 
			Event.fireOut("set_state", new object[]{this, v});
		}

        public virtual void set_SkillList(object old)
        {
            object v = getDefinedPropterty("SkillList");
            Dbg.DEBUG_MSG(className + "::set_SkillList: " + old + " => " + v); 
            //Event.fireOut("set_state", new object[] { this, v });
        }
		
		public virtual void set_subState(object old)
		{
			// Dbg.DEBUG_MSG(className + "::set_subState: " + getDefinedPropterty("subState")); 
		}
		
		public virtual void set_utype(object old)
		{
			// Dbg.DEBUG_MSG(className + "::set_utype: " + getDefinedPropterty("utype")); 
		}
		
		public virtual void set_uid(object old)
		{
			// Dbg.DEBUG_MSG(className + "::set_uid: " + getDefinedPropterty("uid")); 
		}
		
		public virtual void set_spaceUType(object old)
		{
			// Dbg.DEBUG_MSG(className + "::set_spaceUType: " + getDefinedPropterty("spaceUType")); 
		}
		
		public virtual void set_moveSpeed(object old)
		{
			object v = getDefinedPropterty("moveSpeed");
			// Dbg.DEBUG_MSG(className + "::set_moveSpeed: " + old + " => " + v); 
			Event.fireOut("set_moveSpeed", new object[]{this, v});
		}
		
		public virtual void set_modelScale(object old)
		{
			object v = getDefinedPropterty("modelScale");
			// Dbg.DEBUG_MSG(className + "::set_modelScale: " + old + " => " + v); 
			Event.fireOut("set_modelScale", new object[]{this, v});
		}
		
		public virtual void set_modelID(object old)
		{
			object v = getDefinedPropterty("modelID");
			// Dbg.DEBUG_MSG(className + "::set_modelID: " + old + " => " + v); 
			Event.fireOut("set_modelID", new object[]{this, v});
		}
		
		public virtual void set_forbids(object old)
		{
			// Dbg.DEBUG_MSG(className + "::set_forbids: " + getDefinedPropterty("forbids")); 
		}
		
		public virtual void recvDamage(Int32 attackerID, Int32 skillID, Int32 damageType, Int32 damage)
		{
			// Dbg.DEBUG_MSG(className + "::recvDamage: attackerID=" + attackerID + ", skillID=" + skillID + ", damageType=" + damageType + ", damage=" + damage);
			
			Entity entity = KBEngineApp.app.findEntity(attackerID);

			Event.fireOut("recvDamage", new object[]{this, entity, skillID, damageType, damage});
		}
		
		public virtual void onJump()
		{
			Dbg.DEBUG_MSG(className + "::onJump: " + id);
			Event.fireOut("otherAvatarOnJump", new object[]{this});
		}
		
		public virtual void onAddSkill(Int32 skillID)
		{
			Dbg.DEBUG_MSG(className + "::onAddSkill(" + skillID + ")"); 
			Event.fireOut("onAddSkill", new object[]{this});
			
			Skill skill = new Skill();
			skill.id = skillID;
			skill.name = skillID + " ";
			switch(skillID)
			{
				case 1:
					break;
				case 1000101:
					skill.canUseDistMax = 20f;
                    skill.icon = "skill_1_1";
                    skill.name = "天地一绝";
                    skill.descr = "思呢？我们可以在某个脚本中将组件";
					break;
				case 2000101:
					skill.canUseDistMax = 20f;
                    skill.icon = "skill_1_2";
                    skill.name = "自高无上";
                    skill.descr = "一下讲解：最先执行的方法是Awake";
					break;
				case 3000101:
					skill.canUseDistMax = 20f;
                    skill.icon = "skill_2_1";
                    skill.name = "潜龙勿用";
                    skill.descr = "里虽然可以使用C#来写代码";
					break;
				case 4000101:
					skill.canUseDistMax = 20f;
                    skill.icon = "skill_2_2";
                    skill.name = "弹指神通";
                    skill.descr = "脚本进行验证（两个脚本添加到";
					break;
				case 5000101:
					skill.canUseDistMax = 20f;
                    skill.icon = "skill_3_1";
                    skill.name = "神之挽歌";
                    skill.descr = "是这个类构造对象的生命周期";
					break;
				case 6000101:
					skill.canUseDistMax = 20f;
                    skill.icon = "skill_3_2";
                    skill.name = "暗影一击";
                    skill.descr = "觉得有不对之处，欢迎指";
					break;
				default:
					break;
			};

			SkillBox.inst.add(skill);
		}
		
		public virtual void onRemoveSkill(Int32 skillID)
		{
			Dbg.DEBUG_MSG(className + "::onRemoveSkill(" + skillID + ")"); 
			Event.fireOut("onRemoveSkill", new object[]{this});
			SkillBox.inst.remove(skillID);
		}

        public void onAddBagItem(Dictionary<string, object> item)
        {
            Debug.LogWarning("get bag item " + item["name"] + " index is " + item["bagItemIndex"] + " count " + item["count"] + " stdMode " + item["stdMode"] + " func " + item["func"]);
            KnapsakItem bagItem = new KnapsakItem{ serialnum = (UInt64)item["serialnum"], tableId = (UInt32)item["tableId"], name = (string)item["name"], count = (Byte)item["count"],
                     altnasIndex = (UInt32)item["altnasIndex"], bagFrameIndex = (Byte)item["bagFrameIndex"], bagItemIndex = (UInt16)item["bagItemIndex"], icon = (UInt32)item["icon"], modelId = (UInt32)item["modelId"],
                                                   stdMode = (Byte)item["stdMode"],
                                                   weight = (Byte)item["weight"],
                                                   func = (Byte)item["func"],
                                                   PA1 = (UInt16)item["PA1"],
                                                   PA2 = (UInt16)item["PA2"],
                                                   MA1 = (UInt16)item["MA1"],
                                                   MA2 = (UInt16)item["MA2"],
                                                   PD1 = (UInt16)item["PD1"],

                                                   PD2 = (UInt16)item["PD2"],
                                                   MD1 = (UInt16)item["MD1"],
                                                   MD2 = (UInt16)item["MD2"],
                                                   HP = (UInt32)item["HP"],
                                                   MP = (UInt32)item["MP"],
                                                   Accurate = (Byte)item["Accurate"],
                                                   lucky = (Byte)item["lucky"],
                                                   holiness = (Byte)item["holiness"],
                                                   HPgen = (Byte)item["HPgen"],
                                                   MPgen = (Byte)item["MPgen"],
                                                   crit = (Byte)item["crit"],
                                                   need = (Byte)item["need"],
                                                   needLevel = (Byte)item["needLevel"],
                                                   price = (UInt32)item["price"],
                                                   stock = (Byte)item["stock"],
                                                   script = (string)item["script"],
                                                   
            };
            //KnapSackInfo.inst.allKanpItems.Add(bagItem.bagFrameIndex, )
            if (!KnapSackInfo.inst.allKanpItems.ContainsKey(bagItem.bagFrameIndex))
            {
                KnapSackInfo.inst.allKanpItems.Add(bagItem.bagFrameIndex, new Dictionary<UInt16, KnapsakItem>());
            }
            KnapSackInfo.inst.allKanpItems[bagItem.bagFrameIndex].Add(bagItem.bagItemIndex, bagItem);
            Event.fireOut("AddBagItem", new object[] { this, bagItem.bagFrameIndex, bagItem.bagItemIndex});
        }

        public void onDelBagItem(UInt64 serialnum)
        {
            // cellCall("pickUpRequest", new object[] { id });
            Debug.LogWarning("del a item");
            KnapsakItem ki = null;
            Byte frame = 0;
            UInt16 index = 0;
            
            foreach (var v in KnapSackInfo.inst.allKanpItems)
            {
                foreach (var n in KnapSackInfo.inst.allKanpItems[v.Key])
                {
                    if (serialnum == n.Value.serialnum)
                    {
                        ki = n.Value;
                        frame = v.Key;
                        index = n.Key;
                        break;
                    }
                }
            }
            KnapSackInfo.inst.allKanpItems[frame].Remove(index);
            Debug.LogWarning("del a item frame is " + frame + " index " + index);
            Event.fireOut("DelBagItem", new object[] { this, serialnum});
        }

        public void onEquiptedItem(Dictionary<string, object> item)
        {
            Debug.LogWarning("get Equipted item " + item["name"] + " index is " + item["bagFrameIndex"] + " count " + item["count"] + " icon " + item["count"]);
            KnapsakItem bagItem = new KnapsakItem
            {
                serialnum = (UInt64)item["serialnum"],
                tableId = (UInt32)item["tableId"],
                name = (string)item["name"],
                count = (Byte)item["count"],
                altnasIndex = (UInt32)item["altnasIndex"],
                bagFrameIndex = (Byte)item["bagFrameIndex"],
                bagItemIndex = (UInt16)item["bagItemIndex"],
                icon = (UInt32)item["icon"],
                modelId = (UInt32)item["modelId"],
                stdMode = (Byte)item["stdMode"],
                weight = (Byte)item["weight"],
                func = (Byte)item["func"],
                PA1 = (UInt16)item["PA1"],
                PA2 = (UInt16)item["PA2"],
                MA1 = (UInt16)item["MA1"],
                MA2 = (UInt16)item["MA2"],
                PD1 = (UInt16)item["PD1"],

                PD2 = (UInt16)item["PD2"],
                MD1 = (UInt16)item["MD1"],
                MD2 = (UInt16)item["MD2"],
                HP = (UInt32)item["HP"],
                MP = (UInt32)item["MP"],
                Accurate = (Byte)item["Accurate"],
                lucky = (Byte)item["lucky"],
                holiness = (Byte)item["holiness"],
                HPgen = (Byte)item["HPgen"],
                MPgen = (Byte)item["MPgen"],
                crit = (Byte)item["crit"],
                need = (Byte)item["need"],
                needLevel = (Byte)item["needLevel"],
                price = (UInt32)item["price"],
                stock = (Byte)item["stock"],
                script = (string)item["script"],
            };
            //KnapSackInfo.inst.allKanpItems.Add(bagItem.bagFrameIndex, )
            KnapSackInfo.inst.equipItems[bagItem.bagFrameIndex] = bagItem;
            Event.fireOut("EquiptedItem", new object[] { this, bagItem.bagFrameIndex });
        }
    }
    
} 
