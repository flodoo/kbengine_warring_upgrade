using UnityEngine;
using System.Collections;

public class EntityBehavior : MonoBehaviour {

    public SceneEntityObject seo = null;
    public bool isPlayer = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    //void Update () {
	
    //}

    public virtual void OnClick()
    {
        Debug.LogWarning("entity is clicked");
    }
}
