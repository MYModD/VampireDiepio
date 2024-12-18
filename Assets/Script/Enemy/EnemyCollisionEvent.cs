using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyCollisionEvent : BaseCollisionEvent
{
    [SerializeField, Tag] private string _playerTag;

    private EnemyHP _enemyHP;
    private EnemyAttack _enemyAttack;
    private PoolableEnemy _poolableEnemy;

    protected override void Awake()
    {
        base.Awake();
        _enemyHP = GetComponent<EnemyHP>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _poolableEnemy = GetComponent<PoolableEnemy>();
    }

    public override void OnCustomCollisionEnter(SimpleShapeCollider2D collision)
    {

        if (collision.gameObject.CompareTag(_playerTag))
        {

            
            _enemyAttack.AttackToPlayer(collision.gameObject.GetComponent<PlayerHP>());
            _poolableEnemy.ReturnToPool();

        }
    }

    public override void OnCustomCollisionStay(SimpleShapeCollider2D collision)
    {
        // Implement your logic here
    }

    public override void OnCustomCollisionExit(SimpleShapeCollider2D collision)
    {
        // Implement your logic here
    }
}
