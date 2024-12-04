using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DebrisObjectPoolManager : ObjectPoolBase<PoolableDebris>
{
    public Transform _setPostion;

    protected override PoolableDebris Create()
    {
        PoolableDebris poolableDebris = base.Create();
        DebrisComponentManager.Instance.RegisterComponents(poolableDebris.gameObject);
        return poolableDebris;
    }

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < 30; i++)
        {
            var hoge = ObjectPool.Get();
            hoge.gameObject.transform.position = Random.insideUnitCircle * 10 * _setPostion.position;
        }
        
    }
}
