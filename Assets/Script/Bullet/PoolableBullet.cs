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
    /// 弾の初期化
    /// </summary>
    public void Initialize()
    {
        _bulletMove.InitialVelocity();

    }

    /// <summary>
    /// プールに返却するときの処理、返却の後に初期化処理を行う
    /// </summary>
    public void ReturnToPool()
    {
        Initialize();
        ObjectPool.Release(this);
    }
}
