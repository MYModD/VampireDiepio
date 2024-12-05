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

    [Header("Backgroundの最初の色と最後の色")]
    [SerializeField] private Color _backGroundColor = default;

    [Header("Fillの最初の色と最後の色")]
    [SerializeField] private Color _fillColor = default;

    [SerializeField] private float _chengeColorTime = 5f;

    private void Awake()
    {
        _backGroundColor = _imageBackground.color;
        _fillColor = _imagesFill.color;
    }

    public async UniTask UniTaskColorChange(CancellationToken cancellationToken)
    {
        //変え始めてから始まるタイマー ／ 変える時間のT(0~1)でLerpで透明度をもたせる

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
