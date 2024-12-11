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
    /// �e�̏�����
    /// </summary>
    public void Initialize()
    {
        _bulletMove.InitialVelocity();

    }

    /// <summary>
    /// �v�[���ɕԋp����Ƃ��̏����A�ԋp�̌�ɏ������������s��
    /// </summary>
    public void ReturnToPool()
    {
        Initialize();
        ObjectPool.Release(this);
    }
}
