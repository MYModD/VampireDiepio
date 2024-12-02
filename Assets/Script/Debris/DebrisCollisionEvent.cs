using NaughtyAttributes;
using UnityEngine;


public class DebrisCollisionEvent : CollisionEventBase
{

    [SerializeField, Tag] private string _debrisTag;
    [SerializeField] private DebrisMove _debrisMove;
    public override void OnCustomCollisionEnter(SimpleShapeCollider2D collision)
    {
        if (collision.gameObject.CompareTag(_debrisTag))
        {
            Debug.Log($"{collision.gameObject.name}");
            //‚±‚±‚É”½”­‚·‚éˆ—‚ğ‘‚­
            DebrisComponents hitDebris = DebrisComponentManager.Instance.GetDebrisComponents(collision.gameObject);


            if (_debrisMove._currentVelocity.magnitude > hitDebris.debrisMove._currentVelocity.magnitude)
            {
                // ƒfƒuƒŠ‚ª‘‚¢•û‚Éˆ—‚ğ•ª‚¯‚é
                _debrisMove.OnDebrisCollisionRecoil();
                hitDebris.debrisMove.BouncedDebris(_debrisMove._currentVelocity);
            }




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
