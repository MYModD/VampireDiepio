using NaughtyAttributes;
using UnityEngine;


public class BulletCollisionEvent : CollisionEventBase
{
    [SerializeField, Tag] private string _enemyTag;
    [SerializeField, Tag] private string _debrisTag;

    private BulletMove _bulletMove;

    protected override void Awake()
    {
        base.Awake();
        _bulletMove = GetComponent<BulletMove>();
    }
    public override void OnCustomCollisionEnter(SimpleShapeCollider2D collision)
    {
        if (collision.gameObject.CompareTag(_debrisTag)){

            DebrisComponents debris = DebrisComponentManager.Instance.GetDebrisComponents(collision.gameObject);
            // HPテスト用なので後で変更 デブリの反動処理は必要ない

            debris.debrisHP.DegreeHP(10);
            

        }
        else if (collision.gameObject.CompareTag(_enemyTag))
        {

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
