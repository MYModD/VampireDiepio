using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolableBullet : MonoBehaviour, IPooledObject<PoolableBullet>
{
    public IObjectPool<PoolableBullet> ObjectPool { private get; set; }

    private BulletMove _bulletMove;

    private void Awake()
    {
        _bulletMove = GetComponent<BulletMove>();
    }

    /// <summary>
    /// íeÇÃèâä˙âª
    /// </summary>
    public void Initialize()
    {
        _bulletMove._currentVelocity = Vector2.zero;   
    }


    //ï‘ãpèàóù
    public void ReturnToPool()
    {
        ObjectPool.Release(this);
        Initialize();
    }
}
