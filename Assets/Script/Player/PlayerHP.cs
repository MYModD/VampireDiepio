using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHP : MonoBehaviour
{

    [Header("Enemy��HP")]
    [SerializeField] private int _enemyHP;

    [Header("HP�ݒ�l , ����������HP")]
    [SerializeField] private int _initialHP;

    [Header("�f�u���Փ˃_���[�W")]
    [SerializeField]
    private int _hitToDebrisDamage = 1;

    [Header("�v���C���[��p�̃X���C�_�[")]
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
