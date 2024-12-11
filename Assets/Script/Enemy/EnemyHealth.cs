using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy��HP")]
    [SerializeField] private int _hp;

    [Header("HP�ݒ�l , ����������HP")]
    [SerializeField] private int _initialHP;
    void Awake()
    {
        
    }

    
    void Update()
    {
        
    }

    public void DamegedByEnemy(int damageValue)
    {
        _hp -= damageValue;
        if (_hp <= 0)
        {

            //�Ƃ�ܔj��
            Destroy(gameObject);
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
