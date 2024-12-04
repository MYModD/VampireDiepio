using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletObjectPoolManager : ObjectPoolBase<PoolableBullet>
{
    protected override PoolableBullet Create()
    {
        PoolableBullet poolableBullet =  base.Create();
        BulletComponentManager.Instance.RegisterComponents(poolableBullet.gameObject);
        return poolableBullet;
    }

    public GameObject GetBulletObject()
    {
        return ObjectPool.Get().gameObject;
    }

}
