using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DebrisHP : MonoBehaviour
{
    public int _hp = 10;
    void Awake()
    {

    }


    void Update()
    {

    }

    public void DegreeHP(int degreeValue)
    {
        _hp -= degreeValue;
        if (_hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}