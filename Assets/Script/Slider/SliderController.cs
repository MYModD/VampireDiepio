using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class SliderController : MonoBehaviour
{

    [Header("スライダーがプールに返却されるまでの時間")]
    [SerializeField] private float _returnTime = 5f;

    private SliderPostion _sliderPostioin;
    private SliderValue _sliderValue;
    private SliderColor _sliderColor;
    private PoolableSlider _poolableSlider;

    private CancellationTokenSource _localTokenSource;


    private void Awake()
    {
        _sliderPostioin = GetComponent<SliderPostion>();
        _sliderValue = GetComponent<SliderValue>();
        _sliderColor = GetComponent<SliderColor>();
        _poolableSlider = GetComponent<PoolableSlider>();

       
    }


    [Button]
    public void OnSliderChangeTrackingStart()
    {
       



        StartReturnTimer();
        
    }


    /// <summary>
    /// タイマーを開始,0になったら色を透明にしてプールに返却
    /// </summary>
    public async void StartReturnTimer()
    {
        _localTokenSource?.Cancel();
        _localTokenSource = new CancellationTokenSource();

        try
        {

            // 一定時間後にプールに返却
            await UniTask.Delay(TimeSpan.FromSeconds(_returnTime), cancellationToken: _localTokenSource.Token);

            // 一定時間後経過で色を透明に
            await _sliderColor.UniTaskColorChange(_localTokenSource.Token);

            // その間HP変更がない場合リセット
            // 何もなかったらプールに返却↓

            InitializeControllValue();
            _poolableSlider.ReturnToPool();
        }
        catch (OperationCanceledException)
        {
            // キャンセルされた場合は何もしない
            // これがないとエラーが起こす
        }
    }



    /// <summary>
    /// 値を初期化
    /// </summary>
    public void InitializeControllValue()
    {

        _sliderValue.InitializedSliderValue();
        _sliderPostioin.InitializeSliderPostion();
    }
}