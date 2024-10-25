using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Multicpu : MonoBehaviour
{
    private bool Flag_loop = true;//無限ループ制御用フラグを設定し、揚げる

    void Start()
    {
        Thread_1();
    }

    void OnApplicationQuit()//アプリ終了時の処理（無限ループを解放）
    {
        Flag_loop = false;//無限ループフラグを下げる
    }

    public void Thread_1()//無限ループ本体
    {
        Task.Run(() =>
        {
            while (Flag_loop)//無限ループフラグをチェック
            {
                try
                {
                    Debug.Log("Test");
                }
                catch (System.Exception e)//例外をチェック
                {
                    Debug.LogWarning(e);//エラーを表示
                }
            }
        });
    }
}
