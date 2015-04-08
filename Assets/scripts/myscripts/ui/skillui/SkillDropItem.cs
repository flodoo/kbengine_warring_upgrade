using UnityEngine;
using System.Collections;
using KBEngine;

public class SkillItem
{
    public string icon;
    public string skillId;
}
[AddComponentMenu("MyDropDrag/Skill/SkillDropItem")]

public class SkillDropItem : UIDragDropItem
{
    /// <summary>
    /// Prefab object that will be instantiated on the DragDropSurface if it receives the OnDrop event.
    /// </summary>

    public Skill skillItem;

    protected virtual void Start ()
	{
        base.Start();
        cloneOnDrag = true;
        onDosomething = ddi =>
        {
            ((SkillDropItem)ddi).skillItem = skillItem;
        };
    }
    /// <summary>
    /// Drop a 3D game object onto the surface.
    /// </summary>

    protected override void OnDragDropRelease(UnityEngine.GameObject surface)
    {
        if (surface != null)
        {
            Debug.Log("onrelease");
            // Re-enable the collider

            // Is there a droppable container?
            UISkillDragDropContainer container = surface ? NGUITools.FindInParents<UISkillDragDropContainer>(surface) : null;
            Debug.Log("container" + container);
            if (container != null)
            {
                // Container found -- parent this object to the container
                if(container.ico ==null)
                    container.ico = container.transform.Find("ico").GetComponent<UISprite>();
                if (mTrans.GetComponent<SkillDropItem>().skillItem == null)
                {
                    Debug.LogWarning("skillitem is null");
                }
                else
                {
                    container.skillItem = mTrans.GetComponent<SkillDropItem>().skillItem;
                    container.ico.spriteName = container.skillItem.icon;
                    container.RegisterKeyEvent();
                }
                
            }
            

            // Update the grid and table references
            //mParent = mTrans.parent;
            //mGrid = NGUITools.FindInParents<UIGrid>(mParent);
            //mTable = NGUITools.FindInParents<UITable>(mParent);

            // Re-enable the drag scroll view script
            //if (mDragScrollView != null)
            //    StartCoroutine(EnableDragScrollView());

            // Notify the widgets that the parent has changed
            //NGUITools.MarkParentAsChanged(gameObject);

            //if (mTable != null) mTable.repositionNow = true;
            //if (mGrid != null) mGrid.repositionNow = true;

            // We're now done
            OnDragDropEnd();
        }
        Debug.Log("destroy");
        Destroy(gameObject);
    }
}
