using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SliderObjectPoolManager : BaseObjectPool<PoolableSlider>
{
    [Header("親オブジェクト選択")]
    [SerializeField] private Transform _pearentObjct;
    protected override PoolableSlider Create()
    {
        PoolableSlider slider = base.Create();
        //slider.transform.SetParent(_pearentObjct);
        return slider;
    }

}
