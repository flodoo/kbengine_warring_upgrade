using UnityEngine;
using KBEngine;
using System.Collections;
using System;
using System.Xml;
using System.Collections.Generic;

public class SceneMonsterObject : SceneEntityObject
{
    GameEntityCtrl gameEntityCtrl = null;
    public void updateHPBar(Int32 hp, Int32 hpmax)
    {
        if (hud_infosObj != null)
            hudinfos.updateHPBar(hp, hpmax);
    }
    public override void onAssetAsyncLoadObjectCB(string name, UnityEngine.Object obj, Asset asset)
    {
        base.onAssetAsyncLoadObjectCB(name, obj, asset);
        gameEntityCtrl = (GameEntityCtrl)gameEntity;
        gameEntityCtrl.smo = this;
        if (isPlayer == false)
        {
            attachHeadInfo();
            destPosition = position;
            destDirection = eulerAngles;
            Common.calcPositionY(destPosition, out destPosition.y, false);
            gameEntity.gameObject.transform.position = destPosition;
        }
        else
        {
            if (gameObject.GetComponent<RPG_Animation>() == null)
                gameObject.AddComponent<RPG_Animation>();

            gameObject.name = "entity";

            BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
            if (boxCollider != null)
                UnityEngine.Object.Destroy(boxCollider);

            if (gameObject.GetComponent<MeshCollider>() != null)
                gameObject.AddComponent<MeshCollider>();

            UnityEngine.GameObject PlayerChar = UnityEngine.GameObject.Find("PlayerChar");
            if (PlayerChar != null)
                gameObject.transform.parent = PlayerChar.transform;
        }
    }

    public void update_position(Vector3 destPos)
    {
        destPosition = destPos;
    }

    public void set_moveSpeed(float v)
    {
        speed = v;
        if (gameEntityCtrl != null)
        {
            gameEntityCtrl.set_moveSpeed(v);
        }
    }

    public void addHP(Int32 v)
    {
        if (hud_infosObj != null)
        {
            if (v > 0)
                hudinfos.addHP(v);
            else
                hudinfos.addDamage(v);
        }
    }

    public void addMP(Int32 v)
    {
        if (hud_infosObj != null)
            hudinfos.addHP(v);
    }

    public void set_state(SByte v)
    {
        state = v;
        if (hudinfos != null)
        {
            hudinfos.set_state(v);
        }

        if (gameEntityCtrl != null)
        {
            switch (state)
            {
                case 0:
                    gameEntityCtrl.playIdleAnimation();
                    break;
                case 1:
                    gameEntityCtrl.playDeadAnimation();
                    break;
                case 2:
                    gameEntityCtrl.playIdleAnimation();
                    break;
                case 3:
                    gameEntityCtrl.playIdleAnimation();
                    break;
                default:
                    gameEntityCtrl.playIdleAnimation();
                    break;
            };
        }
    }
    public void attack(Int32 skillID, Int32 damageType, SceneEntityObject receiver)
    {
        if (gameEntityCtrl != null)
        {
            gameEntityCtrl.playAttackAnimation();
        }

        if (receiver == null || receiver.gameEntity == null)
            return;

        if (particles.inst != null)
        {
            Vector3 v = position;
            UnityEngine.GameObject pobj = null;

            switch (skillID)
            {
                case 7000101:
                    pobj = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(particles.inst.allpartis[37], gameEntity.gameObject.transform.FindChild("attackPoint").transform.position, rotation);
                    SM_moveToEntity sm = pobj.GetComponent<SM_moveToEntity>();
                    sm.moveSpeed = 10.0f;
                    sm.target = receiver.gameEntity.gameObject;
                    break;
                default:
                    break;
            };

            if (pobj)
                pobj.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        }
    }

    public void recvDamage(Int32 skillID, Int32 damageType, Int32 damage)
    {
        if (gameEntityCtrl != null)
        {
            gameEntityCtrl.playDamageAnimation();
        }

        if (hud_infosObj != null)
            hudinfos.addDamage(damage);

        if (particles.inst != null)
        {
            Vector3 v = position;
            UnityEngine.GameObject pobj = null;
            v.y += 1f;

            switch (skillID)
            {
                case 1:
                    break;
                case 1000101:
                    pobj = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(particles.inst.allpartis[47], v, rotation);
                    break;
                case 2000101:
                    v.y += 1.5f;
                    pobj = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(particles.inst.allpartis[21], v, rotation);
                    break;
                case 3000101:
                    pobj = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(particles.inst.allpartis[32], v, rotation);
                    break;
                case 4000101:
                    pobj = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(particles.inst.allpartis[45], v, rotation);
                    break;
                case 5000101:
                    v.y -= 1f;
                    pobj = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(particles.inst.allpartis[14], v, rotation);
                    break;
                case 6000101:
                    v.y += 0.5f;
                    pobj = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(particles.inst.allpartis[9], v, rotation);
                    pobj.transform.parent = gameObject.transform;
                    break;
                case 7000101:
                    //pobj = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(particles.inst.allpartis[37], v, rotation);
                    //pobj.transform.parent = gameObject.transform;
                    break;
                default:
                    break;
            };

            if (pobj)
                pobj.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        }
    }

    public void playAnimation(string ani)
    {
        if (gameEntityCtrl != null)
        {
            gameEntityCtrl.playAnimation(ani);
        }
    }

    public void stopPlayAnimation(string ani)
    {
        if (gameEntityCtrl != null)
        {
            gameEntityCtrl.stopPlayAnimation(ani);
        }
    }

    public void playJumpAnimation()
    {
        if (gameEntityCtrl != null)
        {
            gameEntityCtrl.playJumpAnimation();
        }
    }

    public void playIdleAnimation()
    {
        if (gameEntityCtrl != null)
        {
            gameEntityCtrl.playIdleAnimation();
        }
    }

    public void playWalkAnimation()
    {
        if (gameEntityCtrl != null)
        {
            gameEntityCtrl.playWalkAnimation();
        }
    }

    public void playRunAnimation()
    {
        if (gameEntityCtrl != null)
        {
            gameEntityCtrl.playRunAnimation();
        }
    }

    public void playAttackAnimation()
    {
        if (gameEntityCtrl != null)
        {
            gameEntityCtrl.playAttackAnimation();
        }
    }
}
