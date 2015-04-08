using UnityEngine;
using System.Collections;
using KBEngine;
using System.Threading;

public class BagItemGameObject : EntityBehavior
{
    //public SceneItemObject seo = null;
	// Use this for initialization
    public SceneItemObject sio = null;
	void Start () {
	
	}
	
	// Update is called once per frame
    //void Update () {
	
    //}

    public override void OnClick()
    {
        Debug.LogWarning("bag item is clicked");
        PickUpItem();
    }

    void PickUpItem()
    {
        Monitor.Enter(KBEngineApp.app.entities);
        KBEngine.Avatar player = (KBEngine.Avatar)KBEngineApp.app.player();
        player.pickUpRequest(seo.kbentity.id);
        Monitor.Exit(KBEngineApp.app.entities);
    }
}
