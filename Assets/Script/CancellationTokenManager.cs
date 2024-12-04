using Diepio;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class CancellationTokenManager : Singleton<CancellationTokenManager>
{

    // Token���擾����Ƃ�_tokenSource��Token��Ԃ� �Ȍ���Getter
    private CancellationTokenSource _tokenSource;
    public CancellationToken Token => _tokenSource.Token;

    protected override void Awake()
    {
        base.Awake();
        // TokenSource�̏�����
        _tokenSource = new CancellationTokenSource();
    }

    protected override void OnDestroy()
    {
        if (Instance == this)
        {
            // destroy���ɃL�����Z�� Monobeheviour���폜����Ă�
            // Unitask�̓L�����Z������Ȃ�����
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }
        base.OnDestroy();
    }
}
