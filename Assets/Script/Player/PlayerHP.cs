using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHP : MonoBehaviour
{

    [Header("Enemy��HP")]
    [SerializeField] private int _hp;

    [Header("HP�ݒ�l , ����������HP")]
    [SerializeField] private int _initialHP;

    [Header("�f�u���Փ˃_���[�W")]
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
