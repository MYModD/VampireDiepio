using UnityEngine;


public class PlayerCollisionEvent : CollisionEventBase
{
    public override void OnCustomCollisionEnter(SimpleShapeCollider2D collision)
    {
        Debug.Log($"{collision.gameObject.name}Ç∆è’ìÀÇµÇ‹ÇµÇΩ".Warning());
    }

    public override void OnCustomCollisionStay(SimpleShapeCollider2D collision)
    {
        // Implement collision stay logic here
    }

    public override void OnCustomCollisionExit(SimpleShapeCollider2D collision)
    {
        // Implement collision exit logic here
    }
}
