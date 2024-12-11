using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletObjectPoolManager : BaseObjectPool<PoolableBullet>
{
    protected override PoolableBullet Create()
    {
        PoolableBullet poolableBullet =  base.Create();
        BulletComponentsManager.Instance.RegisterComponents(poolableBullet.gameObject);
        return poolableBullet;
    }

    public GameObject GetBulletObject()
    {
        return ObjectPool.Get().gameObject;
    }

}
