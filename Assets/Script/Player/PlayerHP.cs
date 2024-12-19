using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHP : MonoBehaviour
{

    [Header("EnemyのHP")]
    [SerializeField] private int _enemyHP;

    [Header("HP設定値 , 初期化するHP")]
    [SerializeField] private int _initialHP;

    [Header("デブリ衝突ダメージ")]
    [SerializeField]
    private int _hitToDebrisDamage = 1;

    [Header("プレイヤー専用のスライダー")]
    [SerializeField] private Slider _slider;



    private void Awake()
    {
        _slider.value = 1;
        _enemyHP = _initialHP;
    }

    public void OnDebirsDamage()
    {
        _enemyHP -= _hitToDebrisDamage;
        _slider.value = _enemyHP / _initialHP;
        Debug.Log(_slider.value);

        if (_enemyHP <= 0)
        {
            GameStateManager.Instance.ChengeGameOverState();
        }
    }


    public void DegreeHP(int damage)
    {
        _enemyHP -= damage;
        _slider.value = (float)_enemyHP / _initialHP;
        Debug.Log(_slider.value);

        if (_enemyHP <= 0)
        {
            GameStateManager.Instance.ChengeGameOverState();
        }
    }

}
