using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolableSlider : MonoBehaviour, IPooledObject<PoolableSlider>
{
    public IObjectPool<PoolableSlider> ObjectPool { private get; set; }


    public void Initialize()
    {
        // ここの初期化する値を設定する
        // ImagesのAlpha値 sliderの値 Postioinは大丈夫
    }

    public void ReturnToPool()
    {
        ObjectPool.Release(this);
    }
}
