using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class SliderController : MonoBehaviour
{
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

        StartReturnTimer();
    }

    public void OnSliderChangeTrackingStart()
    {
        // スライダー値変更時にタイマーをリセットここにスライダーのポジション追従する処理
        StartReturnTimer();
        
    }

    public async void StartReturnTimer()
    {
        _localTokenSource?.Cancel();
        _localTokenSource = new CancellationTokenSource();

        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_returnTime), cancellationToken: _localTokenSource.Token);

            // SliderColorで色を変更するところで帰ってきたらプールに返却
            await _sliderColor.UniTaskColorChange(_localTokenSource.Token);

            _poolableSlider.ReturnToPool();
        }
        catch (OperationCanceledException)
        {
            // キャンセルされた場合は何もしない
            // これがないとエラーが起こす
        }
    }

    public void ResetValue()
    {

        
    }
}