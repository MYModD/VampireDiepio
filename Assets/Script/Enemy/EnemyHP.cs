using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHP : MonoBehaviour
{
    [Header("Enemy‚ÌHP")]
    [SerializeField] private int _hp;

    [Header("HPİ’è’l , ‰Šú‰»‚·‚éHP")]
    [SerializeField] private int _initialHP;

    //[SerializeField] private ScoreManager _scoreManger;

    private PoolableEnemy _poolableEnemy;
    void Awake()
    {
        _poolableEnemy = GetComponent<PoolableEnemy>();
        // _scoreManger = GetComponent<ScoreManager>();

    }

    
    void Update()
    {
        
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
    /// HP‚Ì‰Šú‰»
    /// </summary>
    public void InitializeHP()
    {
        _hp = _initialHP;
    }


}
