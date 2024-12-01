using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PerformanceTest : MonoBehaviour
{
    private Dictionary<int, SimpleShapeCollider2D> healthDict = new Dictionary<int, SimpleShapeCollider2D>();
    private GameObject[] testObjects;
    private int testCount = 1000000; // �e�X�g��

    void Start()
    {
        // �e�X�g�I�u�W�F�N�g�𐶐�
        testObjects = new GameObject[100]; // �e�X�g����v�f��
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
        yield return new WaitForSeconds(5); // �������҂�
        Debug.Log("Start Test".Warning());

        // Dictionary�����̃e�X�g
        var dictStartTime = Time.realtimeSinceStartup;
        for (int i = 0; i < testCount; i++)
        {
            var obj = testObjects[i % testObjects.Length];
            var health = healthDict[obj.GetInstanceID()];
        }
        var dictTime = Time.realtimeSinceStartup - dictStartTime;

        // GetComponent�̃e�X�g
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
