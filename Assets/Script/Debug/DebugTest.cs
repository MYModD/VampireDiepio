using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class DebugTest : MonoBehaviour
{
    private async void hoge()
    {
        // CancellationTokenSourceの生成  
        var cts = new CancellationTokenSource();

        // CancellationTokenをCancellationTokenSourceから取得  
        CancellationToken token = cts.Token;

        // UniTaskにTokenを引き渡す  
        await Wait5SecUniTask(token);

        await UniTask.Delay(TimeSpan.FromSeconds(1));
        // 1秒後にキャンセルを実行  
        cts.Cancel();
    }

    // 5秒後に「Complete!」とログを出力するUniTask  
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
