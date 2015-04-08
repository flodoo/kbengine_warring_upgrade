using UnityEngine;
using KBEngine;
using System; 

public class UI_TextMap : MonoBehaviour {
	public UILabel targetText;
	public TextMesh textMesh;
	
	void Awake ()     
	{
		textMesh = GetComponent<TextMesh>();
	}
	
	// Use this for initialization
	void Start () {
	}

	void Update()
	{
		textMesh.text = targetText.text;
	}
}
 