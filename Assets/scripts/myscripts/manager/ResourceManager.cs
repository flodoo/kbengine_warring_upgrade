using UnityEngine;
using System.Collections;

public class ResourceManager
{
    private static string prefabPrefix = "Prefabs/";
	
		public static GameObject loadItem (string item)
		{
                //Debug.Log("prefab:"+prefabPrefix+item);
				GameObject go = GameObject.Instantiate (Resources.Load (prefabPrefix + item), new Vector3 (0, 0, 0), Quaternion.identity) as  GameObject;
				return go;
		}
	
		public static GameObject loadItem2 (string item)
		{
				GameObject go = GameObject.Instantiate (Resources.Load (prefabPrefix + item)) as  GameObject;
				return go;
		}
	
}
