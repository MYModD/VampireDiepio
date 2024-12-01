using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PerformanceTest : MonoBehaviour
{
    private Dictionary<int, SimpleShapeCollider2D> healthDict = new Dictionary<int, SimpleShapeCollider2D>();
    private GameObject[] testObjects;
    private int testCount = 1000000; // テスト回数

    void Start()
    {
        // テストオブジェクトを生成
        testObjects = new GameObject[100]; // テストする要素数
        for (int i = 0; i < testObjects.Length; i++)
        {
            var obj = new GameObject($"Test_{i}");
            var health = obj.AddComponent<SimpleShapeCollider2D>();
            healthDict[obj.GetInstanceID()] = health;
            testObjects[i] = obj;
        }

        StartCoroutine(RunTest());
    }

    IEnumerator RunTest()
    {
        yield return new WaitForSeconds(5); // 初期化待ち
        Debug.Log("Start Test".Warning());

        // Dictionary検索のテスト
        var dictStartTime = Time.realtimeSinceStartup;
        for (int i = 0; i < testCount; i++)
        {
            var obj = testObjects[i % testObjects.Length];
            var health = healthDict[obj.GetInstanceID()];
        }
        var dictTime = Time.realtimeSinceStartup - dictStartTime;

        // GetComponentのテスト
        var getCompStartTime = Time.realtimeSinceStartup;
        for (int i = 0; i < testCount; i++)
        {
            var obj = testObjects[i % testObjects.Length];
            var health = obj.GetComponent<SimpleShapeCollider2D>();
        }
        var getCompTime = Time.realtimeSinceStartup - getCompStartTime;

        Debug.Log($"Dictionary Time: {dictTime}s");
        Debug.Log($"GetComponent Time: {getCompTime}s");

        


        
        
    }



    
}
