using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHP : MonoBehaviour
{
    
    [SerializeField]
    private int _hp = 100;

    [Header("�f�u���Փ˃_���[�W")]
    [SerializeField]
    private int _hitToDebrisDamage = 1;

    public void OnDebirsDamage()
    {
        _hp -= _hitToDebrisDamage;
        if (_hp <= 0)
        {
            Debug.Log("GameOver".BoldYellow());
        }
    }

}
