using UnityEngine;
using System.Collections;
using KBEngine;

[AddComponentMenu("MyDropDrag/Skill/UISkillDragDropContainer")]
public class UISkillDragDropContainer : UIDragDropContainer
{
    const string classname = "UISkillDragDropContainer";
    public UISprite ico = null;
    public Skill skillItem = null;
    public KeyCode keyEventName ;

    protected virtual void Start() {
        base.Start();
        if(ico ==null)
            ico = transform.Find("ico").GetComponent<UISprite>();
    }

    public void RegisterKeyEvent()
    {
        Debug.Log("resgister envet key is " + keyEventName);
        UI_KeyManager.eventDic[keyEventName] = EventFun;
    }

    void EventFun()
    {
        if (skillItem == null)
            return;
        Debug.Log("take skill id :" + skillItem.id);
        if (TargetManger.target == null)
            return;
        KBEngine.Entity player = KBEngineApp.app.player();
        if (player != null && TargetManger.target != null)
            ((KBEngine.Avatar)player).useTargetSkill(skillItem.id, TargetManger.target.kbentity.id);
    }

}
