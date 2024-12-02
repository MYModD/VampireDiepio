using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DebrisObjectPoolManager : ObjectPoolBase<PoolableDebris>
{

    protected override PoolableDebris Create()
    {
        PoolableDebris poolableDebris = base.Create();
        DebrisComponentManager.Instance.RegisterDebris(poolableDebris.gameObject);
        return poolableDebris;
    }

    protected override void Awake()
    {
        base.Awake();
        
    }
}
