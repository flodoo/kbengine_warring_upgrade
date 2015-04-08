using UnityEngine;
using System;
using System.Collections;
using KBEngine;

public class SceneEntityObjectManager {
    public static SceneEntityObject CreateSceneEntityObject(Entity entity)
    {
        UInt32 modelid = (UInt32)entity.getDefinedPropterty("modelID");//以后要改成类型
        if (modelid <= 10)
        {
            CreateItemObject(entity);
        }else if(modelid > 10){
            CreateMonsterObject(entity);
        }
        
        return null;
    }

    //可以用抽象模式来优化
    public static void CreateItemObject(Entity entity)
    {
        Asset newasset = Scene.findAsset(entity.getDefinedPropterty("modelID") + ".unity3d", true, "");
        newasset.createAtScene = loader.inst.currentSceneName;
        SceneItemObject sio = new SceneItemObject();
        sio.kbentity = entity;
        sio.create();
        entity.renderObj = sio;
        Scene scene = null;
        if (!loader.inst.scenes.TryGetValue(loader.inst.currentSceneName, out scene))
        {
            Common.ERROR_MSG("KBEEventProc::onEnterWorld: not found scene(" + loader.inst.currentSceneName + ")!");
            return;
        }
        newasset.loadLevel = Asset.LOAD_LEVEL.LEVEL_IDLE;
        sio.asset = newasset;
        sio.idkey = "_entity_" + entity.id;
        sio.position = entity.position;
        sio.eulerAngles = new Vector3(entity.direction.y, entity.direction.z, entity.direction.x);
        object name = entity.getDefinedPropterty("name");
        if (name != null)
            sio.setName((string)name);
        newasset.refs.Add(sio.idkey);
        scene.addSceneObject(sio.idkey, sio);

        if (newasset.isLoaded || newasset.bundle != null)
        {
            sio.Instantiate();
            newasset.refs.Remove(sio.idkey);
        }
        else
        {
            loader.inst.loadPool.addLoad(newasset);
            loader.inst.loadPool.start();
        }
    }

    public static void CreateMonsterObject(Entity entity)
    {
        Asset newasset = Scene.findAsset(entity.getDefinedPropterty("modelID") + ".unity3d", true, "");
        newasset.createAtScene = loader.inst.currentSceneName;
        SceneMonsterObject smo = new SceneMonsterObject();
        smo.kbentity = entity;
        if (entity.isPlayer())
            smo.createPlayer();
        else
            smo.create();
        entity.renderObj = smo;
        Scene scene = null;
        if (!loader.inst.scenes.TryGetValue(loader.inst.currentSceneName, out scene))
        {
            Common.ERROR_MSG("KBEEventProc::onEnterWorld: not found scene(" + loader.inst.currentSceneName + ")!");
            return;
        }
        newasset.loadLevel = Asset.LOAD_LEVEL.LEVEL_IDLE;
        smo.asset = newasset;
        smo.idkey = "_entity_" + entity.id;

        Vector3 pos = smo.position;
        Common.calcPositionY(pos, out pos.y, false);
        if (entity.getDefinedPropterty("modelID") == "20002001")
            pos.y += 15.0f;

        smo.position = pos;

        smo.position = entity.position;
        smo.eulerAngles = new Vector3(entity.direction.y, entity.direction.z, entity.direction.x);
        smo.destDirection = smo.eulerAngles;

        object name = entity.getDefinedPropterty("name");
        if (name != null)
            smo.setName((string)name);

        object hp = entity.getDefinedPropterty("HP");
        if (hp != null)
            smo.updateHPBar((Int32)hp, (Int32)entity.getDefinedPropterty("HP_Max"));

        object state = entity.getDefinedPropterty("state");
        if (state != null)
            KBEEventProc.inst.set_state(entity, state);

        object modelScale = entity.getDefinedPropterty("modelScale");
        if (modelScale != null)
            KBEEventProc.inst.set_modelScale(entity, modelScale);

        object speed = entity.getDefinedPropterty("moveSpeed");
        if (speed != null)
        {
            KBEEventProc.inst.set_moveSpeed(entity, speed);
        }

        if (entity.className == "Avatar")
            newasset.unloadLevel = Asset.UNLOAD_LEVEL.LEVEL_FORBID;

        newasset.refs.Add(smo.idkey);
        scene.addSceneObject(smo.idkey, smo);

        if (newasset.isLoaded || newasset.bundle != null)
        {
            smo.Instantiate();
            newasset.refs.Remove(smo.idkey);
        }
        else
        {
            loader.inst.loadPool.addLoad(newasset);
            loader.inst.loadPool.start();
        }
    }
}
