using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PerformanceTest : MonoBehaviour
{
    private BulletComponentsManager _componentManager;
    private List<GameObject> _testObjects;
    private const int TEST_COUNT = 500;
    private const int ITERATION_COUNT = 100;

    private void Start()
    {
        _componentManager = BulletComponentsManager.Instance;
        _testObjects = new List<GameObject>();

        // テスト用オブジェクトの作成と登録
        for (int i = 0; i < TEST_COUNT; i++)
        {
            var obj = new GameObject($"TestBullet_{i}");
            obj.AddComponent<BulletMove>();
            _componentManager.RegisterComponents(obj);
            _testObjects.Add(obj);
        }

        // 各テストを実行
        TestGetComponent();
        TestComponentManager();
        TestFindObjectsByType();
    }

    private void TestGetComponent()
    {
        var stopwatch = new Stopwatch();
        double totalMs = 0;

        for (int iteration = 0; iteration < ITERATION_COUNT; iteration++)
        {
            stopwatch.Reset();
            stopwatch.Start();

            foreach (var obj in _testObjects)
            {
                var component = obj.GetComponent<BulletMove>();
            }

            stopwatch.Stop();
            totalMs += stopwatch.Elapsed.TotalMilliseconds;
        }

        Debug.Log($"GetComponent 平均実行時間: {totalMs / ITERATION_COUNT}ms (対象: {TEST_COUNT}個)");
    }

    private void TestComponentManager()
    {
        var stopwatch = new Stopwatch();
        double totalMs = 0;

        for (int iteration = 0; iteration < ITERATION_COUNT; iteration++)
        {
            stopwatch.Reset();
            stopwatch.Start();

            foreach (var obj in _testObjects)
            {
                var components = _componentManager.GetComponents(obj);
            }

            stopwatch.Stop();
            totalMs += stopwatch.Elapsed.TotalMilliseconds;
        }

        Debug.Log($"ComponentManager 平均実行時間: {totalMs / ITERATION_COUNT}ms (対象: {TEST_COUNT}個)");
    }

    private void TestFindObjectsByType()
    {
        var stopwatch = new Stopwatch();
        double totalMs = 0;

        for (int iteration = 0; iteration < ITERATION_COUNT; iteration++)
        {
            stopwatch.Reset();
            stopwatch.Start();

            var objects = Object.FindObjectsByType<BulletMove>(FindObjectsSortMode.None);

            stopwatch.Stop();
            totalMs += stopwatch.Elapsed.TotalMilliseconds;
        }

        Debug.Log($"FindObjectsByType 平均実行時間: {totalMs / ITERATION_COUNT}ms (対象: {TEST_COUNT}個)");
    }

    private void OnDestroy()
    {
        // テストオブジェクトの削除
        foreach (var obj in _testObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        _testObjects.Clear();
    }

}
