using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SliderValue : MonoBehaviour
{
    [SerializeField] private float _currentSliderValue;

    private Slider _slider;


    private const float SLIDER_MAX_VALUE = 1f;

    private void Awake()
    {
       _slider = GetComponent<Slider>();  
    }

    public void OnDecreaseValue(int beforeHP,int afterHP)
    {

    }

    public void OnIncreaseValue()
    {


    }



    /// <summary>
    /// スライダーの値の初期化
    /// </summary>
    public void InitializedSliderValue()
    {
        _slider.value = SLIDER_MAX_VALUE;
    }


}
