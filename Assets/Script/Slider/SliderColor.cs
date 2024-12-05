using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class SliderColor : MonoBehaviour
{
    [SerializeField] private Image _imageBackground;
    [SerializeField] private Image _imagesFill;

    [Header("Background�̍ŏ��̐F�ƍŌ�̐F")]
    [SerializeField] private Color _backGroundColor = default;

    [Header("Fill�̍ŏ��̐F�ƍŌ�̐F")]
    [SerializeField] private Color _fillColor = default;

    [SerializeField] private float _chengeColorTime = 5f;

    private void Awake()
    {
        _backGroundColor = _imageBackground.color;
        _fillColor = _imagesFill.color;
    }

    public async UniTask UniTaskColorChange(CancellationToken cancellationToken)
    {
        //�ς��n�߂Ă���n�܂�^�C�}�[ �^ �ς��鎞�Ԃ�T(0~1)��Lerp�œ����x����������

        float stopwatch = 0; 
        while (stopwatch > _chengeColorTime) {
            float t = stopwatch / _chengeColorTime;
            // _imageBackground.color = Color.RGBToHSV(new Color(0.9f, 0.7f, 0.1f, 1.0F));
            // _imagesFill.color = Color.Lerp(_fillStartColor, _fillEndColor, t);

            stopwatch += Time.deltaTime;
            await UniTask.Yield();
        }

        // _imageBackground.color = _backGroundEndColor;
        // _imagesFill.color = _fillEndColor;

    }

    void Update()
    {
    }
}
