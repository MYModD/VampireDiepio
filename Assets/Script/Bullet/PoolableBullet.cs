using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolableBullet : MonoBehaviour, IPooledObject<PoolableBullet>
{
    public IObjectPool<PoolableBullet> ObjectPool { private get; set; }

    [SerializeField] private BulletMove _bulletMove;
    /// <summary>
    /// èâä˙âª
    /// </summary>
    public void Initialize()
    {
        _bulletMove._currentVelocity = Vector2.zero;   
    }


    //ï‘ãpèàóù
    public void ReturnToPool()
    {
        ObjectPool.Release(this);
    }
}
