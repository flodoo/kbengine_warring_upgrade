using UnityEngine;
using System.Collections;

public class SceneItemObject : SceneEntityObject
{
    public BagItemGameObject itemEntity = null;
    public override void onAssetAsyncLoadObjectCB(string name, UnityEngine.Object obj, Asset asset)
    {
        base.onAssetAsyncLoadObjectCB(name, obj, asset);
        itemEntity = (BagItemGameObject)gameEntity;
        itemEntity.gameObject.transform.localScale = new Vector3(1, 1, 1);
        itemEntity.sio = this;
        attachHeadInfo();
        destPosition = position;
        destDirection = eulerAngles;
        Common.calcPositionY(destPosition, out destPosition.y, false);
        gameEntity.gameObject.transform.position = destPosition;
    }

}
