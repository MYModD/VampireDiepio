using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHP : MonoBehaviour
{
    
    [SerializeField]
    private int _hp = 100;

    [Header("デブリ衝突ダメージ")]
    [SerializeField]
    private int _hitDebrisDamage = 1;

    public void OnDebirsDamage()
    {
        _hp -= _hitDebrisDamage;
        if (_hp <= 0)
        {
            Debug.Log("GameOver".Warning());
        }
    }

}
