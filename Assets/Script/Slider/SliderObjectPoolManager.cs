using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SliderObjectPoolManager : BaseObjectPool<PoolableSlider>
{
    [Header("�e�I�u�W�F�N�g�I��")]
    [SerializeField] private Transform _pearentObjct;
    protected override PoolableSlider Create()
    {
        PoolableSlider slider = base.Create();
        //slider.transform.SetParent(_pearentObjct);
        return slider;
    }

}
