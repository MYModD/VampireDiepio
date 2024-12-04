using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SliderObjectPoolManager : ObjectPoolBase<PoolableSlider>
{
    [Header("親オブジェクト選択")]
    [SerializeField] private Transform _pearentObjct;
    protected override PoolableSlider Create()
    {
        PoolableSlider slider = base.Create();
        slider.transform.parent = _pearentObjct;
        return slider;
    }

}
