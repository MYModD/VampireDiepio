using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHP : MonoBehaviour
{

    [Header("EnemyのHP")]
    [SerializeField] private int _hp;

    [Header("HP設定値 , 初期化するHP")]
    [SerializeField] private int _initialHP;

    [Header("デブリ衝突ダメージ")]
    [SerializeField]
    private int _hitToDebrisDamage = 1;

    [SerializeField] private Slider _slider;



    private void Awake()
    {
        _slider.value = 1;
        _hp = _initialHP;
    }

    public void OnDebirsDamage()
    {
        _hp -= _hitToDebrisDamage;
        _slider.value = _hp / _initialHP;

        if (_hp <= 0)
        {
            GameStateManager.Instance.ChengeGameOverState();
        }
    }


    public void DegreeHP(int damage)
    {
        _hp -= damage;
        _slider.value = _hp / _initialHP;

        if (_hp <= 0)
        {
            GameStateManager.Instance.ChengeGameOverState();
        }
    }

}
