using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHP : MonoBehaviour
{
    [Header("Enemy��HP")]
    [SerializeField] private int _hp;

    [Header("HP�ݒ�l , ����������HP")]
    [SerializeField] private int _initialHP;


    private PoolableEnemy _poolableEnemy;



    void Awake()
    {
        _poolableEnemy = GetComponent<PoolableEnemy>();

    }



    public void DamegedByEnemy(int damageValue)
    {
        _hp -= damageValue;
        if (_hp <= 0)
        {

            
            ScoreManager.Instance.AddScore(gameObject.tag);
            _poolableEnemy.ReturnToPool();
        }
    }

    /// <summary>
    /// HP�̏�����
    /// </summary>
    public void InitializeHP()
    {
        _hp = _initialHP;
    }


}
