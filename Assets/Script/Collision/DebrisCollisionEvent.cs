using System.Collections;   
using System.Collections.Generic;
using UnityEngine;


public class DebrisCollisionEvent : CollisionEventBase
{
    [SerializeField] private DebrisHP _debrisHP;
    [Header("��Œ��� �����l")]
    [SerializeField] private int  _degreeValue;
    public override void OnCustomCollisionEnter(SimpleShapeCollider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log($"{collision.gameObject.name}:�Ƀq�b�g������");
            _debrisHP.DegreeHP(_degreeValue);

        }
            
    }

    public override void OnCustomCollisionStay(SimpleShapeCollider2D collision)
    {
        // Implement the logic for OnCustomCollisionStay
    }

    public override void OnCustomCollisionExit(SimpleShapeCollider2D collision)
    {
        // Implement the logic for OnCustomCollisionExit
    }
}
