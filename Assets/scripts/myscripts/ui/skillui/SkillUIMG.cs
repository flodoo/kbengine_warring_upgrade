using UnityEngine;
using System.Collections;
using KBEngine;
using System.Collections.Generic;

public class SkillUIMG : MonoBehaviour {

    public UIGrid grid;
	// Use this for initialization
    void Awake()
    {
        if (grid == null)
        {
            grid = transform.Find("bg/Scroll View/Grid").GetComponent<UIGrid>();
        }
    }
	void Start () {
	
	}
    void OnEnable()
    {
        InitSkillList();
    }
    void OnDisable()
    {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void InitSkillList()
    {
        List<Skill> skills = SkillBox.inst.skills;
        int count = skills.Count;
        gridNumAdd(count);
        int gridnum = grid.transform.childCount;
        UnityEngine.GameObject item;
        for (int i = 0; i < gridnum; i++)
        {
            if (i < count)
            {
                item = grid.transform.GetChild(i).gameObject;
                item.SetActive(true);
                SkillDropItem sdi = item.GetComponent<SkillDropItem>();
                if (sdi == null)
                    sdi = item.AddComponent<SkillDropItem>();
                sdi.skillItem = skills[i];
                item.transform.Find("Sprite").GetComponent<UISprite>().spriteName = skills[i].icon;
                item.transform.Find("name").GetComponent<UILabel>().text = skills[i].name;
                item.transform.Find("lv").GetComponent<UILabel>().text = "LV :1";
            }
            else
            {
                grid.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        grid.repositionNow = true;
    }

    void gridNumAdd(int datalen)
    {
        int cha = datalen - grid.transform.childCount;

        if (cha > 0)
        {
           UnityEngine.GameObject item;
            for (int i = 0; i < cha; i++)
            {
                item = ResourceManager.loadItem("ui/skillItem");
                item.transform.parent = grid.transform;
                item.transform.localPosition = Vector3.zero;
                item.transform.localScale = Vector3.one;
                UIEventListener.Get(item).onClick += onItemClick;
            }
        }
    }

    void onItemClick(UnityEngine.GameObject go)
    {
        Debug.Log("SkillItem click");
    }
}
