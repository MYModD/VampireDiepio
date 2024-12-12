using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolableEnemy : MonoBehaviour, IPooledObject<PoolableEnemy>
{
    public IObjectPool<PoolableEnemy> ObjectPool { private get; set; }

    private EnemyHealth _enemyHP;

    private void Awake()
    {
        _enemyHP = GetComponent<EnemyHealth>();
    }

    public void Initialize()
    {
        _enemyHP.InitializeHP();
    }


    /// <summary>
    /// プールに返却するときの処理、返却の前に初期化
    /// </summary>
    public void ReturnToPool()
    {
        Initialize();
        ObjectPool.Release(this);
    }
}
