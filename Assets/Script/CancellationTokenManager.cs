using Diepio;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class CancellationTokenManager : Singleton<CancellationTokenManager>
{

    // Tokenを取得するとき_tokenSourceのTokenを返す 簡潔なGetter
    private CancellationTokenSource _tokenSource;
    public CancellationToken Token => _tokenSource.Token;

    protected override void Awake()
    {
        base.Awake();
        // TokenSourceの初期化
        _tokenSource = new CancellationTokenSource();
    }

    protected override void OnDestroy()
    {
        if (Instance == this)
        {
            // destroy時にキャンセル Monobeheviourが削除されても
            // Unitaskはキャンセルされないため
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }
        base.OnDestroy();
    }
}
