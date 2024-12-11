using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolableEnemy : MonoBehaviour, IPooledObject<PoolableEnemy>
{
    public IObjectPool<PoolableEnemy> ObjectPool { private get; set; }

    private EnemyHP _enemyHP;

    private void Awake()
    {
        _enemyHP = GetComponent<EnemyHP>();
        Application.targetFrameRate = 1000;
    }

    public void Initialize()
    {
        _enemyHP.InitializeHP();
    }


    /// <summary>
    /// �v�[���ɕԋp����Ƃ��̏����A�ԋp�̑O�ɏ�����
    /// </summary>
    public void ReturnToPool()
    {
        Initialize();
        ObjectPool.Release(this);
    }
}
