using NaughtyAttributes;
using System.Collections;   
using System.Collections.Generic;
using UnityEngine;


public class DebrisCollisionEvent : CollisionEventBase
{

    [SerializeField,Tag] private string _debrisTag;
    [SerializeField] private DebrisMove _debrisMove;
    public override void OnCustomCollisionEnter(SimpleShapeCollider2D collision)
    {   
       if(collision.gameObject.CompareTag(_debrisTag))
        {
            //Ç±Ç±Ç…îΩî≠Ç∑ÇÈèàóùÇèëÇ≠
            DebrisComponents hitDebris = DebrisComponentManager.Instance.GetDebrisComponents(collision.gameObject);

            _debrisMove.OnDebrisCollision();
            hitDebris.debrisMove.BouncedDebris(_debrisMove._currentVelocity);
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
