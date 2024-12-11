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

        // �e�X�g�p�I�u�W�F�N�g�̍쐬�Ɠo�^
        for (int i = 0; i < TEST_COUNT; i++)
        {
            var obj = new GameObject($"TestBullet_{i}");
            obj.AddComponent<BulletMove>();
            _componentManager.RegisterComponents(obj);
            _testObjects.Add(obj);
        }

        // �e�e�X�g�����s
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

        Debug.Log($"GetComponent ���ώ��s����: {totalMs / ITERATION_COUNT}ms (�Ώ�: {TEST_COUNT}��)");
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

        Debug.Log($"ComponentManager ���ώ��s����: {totalMs / ITERATION_COUNT}ms (�Ώ�: {TEST_COUNT}��)");
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

        Debug.Log($"FindObjectsByType ���ώ��s����: {totalMs / ITERATION_COUNT}ms (�Ώ�: {TEST_COUNT}��)");
    }

    private void OnDestroy()
    {
        // �e�X�g�I�u�W�F�N�g�̍폜
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
