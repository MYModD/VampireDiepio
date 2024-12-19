using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHP : MonoBehaviour
{
    [Header("EnemyのHP")]
    [SerializeField] private int _hp;

    [Header("HP設定値 , 初期化するHP")]
    [SerializeField] private int _initialHP;


    private PoolableEnemy _poolableEnemy;



    void Awake()
    {
        _poolableEnemy = GetComponent<PoolableEnemy>();

    }



    public void DamegedByEnemy(int damageValue)
    {
        _hp -= damageValue;

        // ここにHPバーの処理内容を書く 



        if (_hp <= 0)
        {

            
            ScoreManager.Instance.AddScore(gameObject.tag);
            _poolableEnemy.ReturnToPool();
        }
    }

    /// <summary>
    /// HPの初期化
    /// </summary>
    public void InitializeHP()
    {
        _hp = _initialHP;
    }


}
