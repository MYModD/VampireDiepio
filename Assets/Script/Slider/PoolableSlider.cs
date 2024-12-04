using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolableSlider : MonoBehaviour, IPooledObject<PoolableSlider>
{
    public IObjectPool<PoolableSlider> ObjectPool { private get; set; }


    public void Initialize()
    {
        // Initialization code here
    }

    public void ReturnToPool()
    {
        ObjectPool?.Release(this);
    }
}
