using UnityEngine;
using KBEngine;
using System.Collections;
using System.Collections.Generic;

public enum KeyEventType
{
    Num1 = 1,
    Num2 = 1,
    Num3 = 1,
    Num4 = 1,
    Num5 = 1,
    Num6 = 1,
    F1 = 11,
    F2 = 12,
    F3 = 13,
    F4 = 14,
    F5 = 15,
    F6 = 16,

}
public delegate void KeyEvent();
public class UI_KeyManager : MonoBehaviour {

    public static UI_KeyManager instance = null;
    public static Dictionary<KeyCode, KeyEvent> eventDic = new Dictionary<KeyCode, KeyEvent> { 
    { KeyCode.Alpha1, EventNotInit },
    { KeyCode.Alpha2, EventNotInit }, 
    { KeyCode.Alpha3, EventNotInit }, 
    { KeyCode.Alpha4, EventNotInit }, 
    { KeyCode.Alpha5, EventNotInit },
    { KeyCode.Alpha6, EventNotInit },
    { KeyCode.F1, EventNotInit },
    { KeyCode.F2, EventNotInit }, 
    { KeyCode.F3, EventNotInit }, 
    { KeyCode.F4, EventNotInit }, 
    { KeyCode.F5, EventNotInit },
    { KeyCode.F6, EventNotInit },};

    public static KeyEvent combeKeysAlt_E = null; //技能UI
    public static KeyEvent combeKeysAlt_C = null; //属性UI
    
    void Awake()
    {
        if (instance == null)
            instance = this;
    }
	
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Common.DEBUG_MSG("UI_Keys::Update: Alpha1");
            if (eventDic[KeyCode.Alpha1] != null)
                eventDic[KeyCode.Alpha1]();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            Common.DEBUG_MSG("UI_Keys::Update: Alpha2");
            if (eventDic[KeyCode.Alpha2] != null)
                eventDic[KeyCode.Alpha2]();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            Common.DEBUG_MSG("UI_Keys::Update: Alpha3");
            if (eventDic[KeyCode.Alpha3] != null)
                eventDic[KeyCode.Alpha3]();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            Common.DEBUG_MSG("UI_Keys::Update: Alpha4");
            if (eventDic[KeyCode.Alpha4] != null)
                eventDic[KeyCode.Alpha4]();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            Common.DEBUG_MSG("UI_Keys::Update: Alpha5");
            if (eventDic[KeyCode.Alpha5] != null)
                eventDic[KeyCode.Alpha5]();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            Common.DEBUG_MSG("UI_Keys::Update: Alpha6");
            if (eventDic[KeyCode.Alpha6] != null)
                eventDic[KeyCode.Alpha6]();
        }
        else if (Input.GetKeyUp(KeyCode.F1))
        {
            Common.DEBUG_MSG("UI_Keys::Update: F1");
            if (eventDic[KeyCode.F1] != null)
                eventDic[KeyCode.F1]();
        }
        else if (Input.GetKeyUp(KeyCode.F2))
        {
            Common.DEBUG_MSG("UI_Keys::Update: F2");
            if (eventDic[KeyCode.F2] != null)
                eventDic[KeyCode.F2]();
        }
        else if (Input.GetKeyUp(KeyCode.F3))
        {
            Common.DEBUG_MSG("UI_Keys::Update: F3");
            if (eventDic[KeyCode.F3] != null)
                eventDic[KeyCode.F3]();
        }
        else if (Input.GetKeyUp(KeyCode.F4))
        {
            Common.DEBUG_MSG("UI_Keys::Update: F4");
            if (eventDic[KeyCode.F4] != null)
                eventDic[KeyCode.F4]();
        }
        else if (Input.GetKeyUp(KeyCode.F5))
        {
            Common.DEBUG_MSG("UI_Keys::Update: F5");
            if (eventDic[KeyCode.F5] != null)
                eventDic[KeyCode.F5]();
        }
        else if (Input.GetKeyUp(KeyCode.F6))
        {
            Common.DEBUG_MSG("UI_Keys::Update: F6");
            if (eventDic[KeyCode.F6] != null)
                eventDic[KeyCode.F6]();
        }
        else if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.E))
        {
            if (combeKeysAlt_E != null)
                combeKeysAlt_E();
        }
        else if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.C))
        {
            if (combeKeysAlt_C != null)
                combeKeysAlt_C();
        }
    }

    public static void EventNotInit()
    {
        Debug.Log("key event not find");
    }
}
