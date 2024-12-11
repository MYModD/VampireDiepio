using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyCollisionEvent : BaseCollisionEvent
{
    private EnemyHP _enemyHP;
    protected override void Awake()
    {
        base.Awake();
        _enemyHP = GetComponent<EnemyHP>();
    }

    public override void OnCustomCollisionEnter(SimpleShapeCollider2D collision)
    {
        // Implement your logic here
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
