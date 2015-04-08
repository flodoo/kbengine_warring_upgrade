using UnityEngine;
using System.Collections;
using System;
using KBEngine;

[AddComponentMenu("MyDropDrag/Skill/UIEquipmentContainer")]
public class UIEquipmentContainer : UIDragDropContainer
{
    public UnityEngine.GameObject eqgo = null;
    public Byte funcCode = 0;
    public Byte index = 0;

    public bool CanBeEquip(Byte code)
    {
        return code == funcCode;
    }
	
}
