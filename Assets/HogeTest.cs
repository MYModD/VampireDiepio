using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class HogeTest : SerializedMonoBehaviour
{
    public List<Quaternion> _hoge = new();
    
    void Awake()
    {
        
    }

    
    void Update()
    {
        
    }

    [Button]
    public void DebugTest()
    {
        Debug.Log("‚Å‚«‚½‚æ!!");
        Debug.Log(_hoge[0]);
        Debug.Log(_hoge[0].eulerAngles);
    }
}
