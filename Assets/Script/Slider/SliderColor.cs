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

    


    [Header("“§–¾‚É‚È‚éŽžŠÔ ‚»‚Ì‚ ‚Æ•Ô‹p")]
    [SerializeField] private float _chengeColorTime = 5f;

    // “§–¾‚ÌF
    private readonly Color TRANSPARENT_COLOR = new Color(0, 0, 0, 0);

    private Color _backGroundStartColor;
    private Color _fillStartColor;

    private void Awake()
    {
        _backGroundStartColor = _imageBackground.color;
        _fillStartColor = _imagesFill.color;

    }

    public async UniTask UniTaskColorChange(CancellationToken cancellationToken)
    {
        try
        {
            float stopwatch = 0;
            while (stopwatch > _chengeColorTime)
            {
                float lerpT = stopwatch / _chengeColorTime;

                Color backGroundColor = Color.Lerp(_backGroundStartColor, TRANSPARENT_COLOR, lerpT);
                _imageBackground.color = backGroundColor;

                Color fillColor = Color.Lerp(_fillStartColor, TRANSPARENT_COLOR, lerpT);
                _imagesFill.color = fillColor;

                stopwatch += Time.deltaTime;
                await UniTask.Yield();
            }
        }
        catch (System.Exception)
        {
            throw;
        }
    }

  
}
