using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DebrisHP : MonoBehaviour
{
    public int _hp = 10;


    public void DegreeHP(int degreeValue)
    {
        Debug.Log("�e�X�g".Warning());
        _hp -= degreeValue;
        if (_hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}