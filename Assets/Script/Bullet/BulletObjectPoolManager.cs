using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletObjectPoolManager : ObjectPoolBase<PoolableBullet>
{
    

    public GameObject GetBulletObject()
    {
        return ObjectPool.Get().gameObject;
    }

}
