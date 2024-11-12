using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DebugGizzle : MonoBehaviour
{
    
    void Awake()
    {
        
    }

    
    void Update()
    {
        transform.position = Random.insideUnitCircle * transform.forward;
    }
}
