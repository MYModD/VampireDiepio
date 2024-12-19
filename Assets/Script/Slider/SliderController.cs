using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class SliderController : MonoBehaviour
{

    [Header("�X���C�_�[���v�[���ɕԋp�����܂ł̎���")]
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
    /// �^�C�}�[���J�n,0�ɂȂ�����F�𓧖��ɂ��ăv�[���ɕԋp
    /// </summary>
    public async void StartReturnTimer()
    {
        _localTokenSource?.Cancel();
        _localTokenSource = new CancellationTokenSource();

        try
        {

            // ��莞�Ԍ�Ƀv�[���ɕԋp
            await UniTask.Delay(TimeSpan.FromSeconds(_returnTime), cancellationToken: _localTokenSource.Token);

            // ��莞�Ԍ�o�߂ŐF�𓧖���
            await _sliderColor.UniTaskColorChange(_localTokenSource.Token);

            // ���̊�HP�ύX���Ȃ��ꍇ���Z�b�g
            // �����Ȃ�������v�[���ɕԋp��

            InitializeControllValue();
            _poolableSlider.ReturnToPool();
        }
        catch (OperationCanceledException)
        {
            // �L�����Z�����ꂽ�ꍇ�͉������Ȃ�
            // ���ꂪ�Ȃ��ƃG���[���N����
        }
    }



    /// <summary>
    /// �l��������
    /// </summary>
    public void InitializeControllValue()
    {

        _sliderValue.InitializedSliderValue();
        _sliderPostioin.InitializeSliderPostion();
    }
}