using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SliderValue : MonoBehaviour
{
    [SerializeField] private float _currentSliderValue;

    private Slider _slider;
    private PoolableSlider _poolableSlider;

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

    
}
