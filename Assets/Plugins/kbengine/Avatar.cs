namespace KBEngine
{
  	using UnityEngine; 
	using System; 
	using System.Collections; 
	using System.Collections.Generic;
	
    public class Avatar : KBEngine.GameObject   
    {
    	public CombatImpl combat = null;
    	
    	public static SkillBox skillbox = new SkillBox();

        public static AvatorInfo avatorInfo = new AvatorInfo();

        public static KnapSackInfo knapsackdInfo = new KnapSackInfo();
    	
		public Avatar()
		{
		}
		
		public override void __init__()
		{
			Event.fireOut("onAvatarEnterWorld", new object[]{KBEngineApp.app.entity_uuid, id, this});
			combat = new CombatImpl(this);
		}
		
		public void relive(Byte type)
		{
			cellCall("relive", new object[]{type});
		}
		
		public bool useTargetSkill(Int32 skillID, Int32 targetID)
		{
			Skill skill = SkillBox.inst.get(skillID);
			if(skill == null)
				return false;

			SCEntityObject scobject = new SCEntityObject(targetID);
			if(skill.validCast(this, scobject))
			{
				skill.use(this, scobject);
			}

			return true;
		}
		
		public void jump()
		{
			cellCall("jump", new object[]{});
		}
		
		public override void onEnterWorld()
		{
			base.onEnterWorld();

			if(isPlayer())
			{
				SkillBox.inst.pull();
			}
		}

        //设置物品在物品栏的位置
        public void setBagItemPos(UInt64 serialnum, Byte FrameIndex, UInt16 BagIndex)
        {
            baseCall("setBagItemPos", new object[] { serialnum, FrameIndex, BagIndex });
        }

        //置换物品在物品栏的位置
        public void rePlaceBagItemPos(UInt64 new_serialnum, UInt64 old_serialnum)
        {
            baseCall("rePlaceBagItemPos", new object[] { new_serialnum, old_serialnum});
        }

        //丢弃一个物品
        public void DropBagItem(UInt64 serialnum)
        {
            baseCall("DropBagItem", new object[] { serialnum});
        }

        //捡起一个物品
        public void pickUpRequest(Int32 id)
        {
            cellCall("pickUpRequest", new object[] {id});
        }

        //装备
        public void EquipItem(UInt64 serialnum, Byte index)
        {
            Debug.LogWarning("equip index is " + index);
            baseCall("EquipItem", new object[] { serialnum, index});
        }

    }
} 
