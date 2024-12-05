using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolableSlider : MonoBehaviour, IPooledObject<PoolableSlider>
{
    public IObjectPool<PoolableSlider> ObjectPool { private get; set; }


    public void Initialize()
    {
        // �����̏���������l��ݒ肷��
        // Images��Alpha�l slider�̒l Postioin�͑��v
    }

    public void ReturnToPool()
    {
        ObjectPool.Release(this);
    }
}
