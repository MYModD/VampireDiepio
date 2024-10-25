using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class DebugTest : MonoBehaviour
{
    private async void hoge()
    {
        // CancellationTokenSource�̐���  
        var cts = new CancellationTokenSource();

        // CancellationToken��CancellationTokenSource����擾  
        CancellationToken token = cts.Token;

        // UniTask��Token�������n��  
        await Wait5SecUniTask(token);

        await UniTask.Delay(TimeSpan.FromSeconds(1));
        // 1�b��ɃL�����Z�������s  
        cts.Cancel();
    }

    // 5�b��ɁuComplete!�v�ƃ��O���o�͂���UniTask  
    async UniTask Wait5SecUniTask(CancellationToken token)
    {
        await UniTask.Delay(5000);
        Debug.Log("Complete!");
    }


    async void Start()
    {
        await Task.WhenAll(AsyncSample1(), AsyncSample2());
        Debug.Log("All Completed.");


    }

    async Task AsyncSample1()
    {
        Debug.Log("AsyncSample1 Start.");
        await Task.Delay(1000);
        Debug.Log("AsyncSample1 End.");
    }

    async Task AsyncSample2()
    {
        Debug.Log("AsyncSample2 Start.");
        await Task.Delay(1000);
        Debug.Log("AsyncSample2 End.");
    }

}
