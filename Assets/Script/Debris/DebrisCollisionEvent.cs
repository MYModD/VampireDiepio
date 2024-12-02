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
            // コンポーネントたちを取得
            DebrisComponents hitDebris = DebrisComponentManager.Instance.GetDebrisComponents(collision.gameObject);

            // デブリ同士だとお互いに処理を行うので、速度が速い方が処理する
            if (_debrisMove._currentVelocity.magnitude > hitDebris.debrisMove._currentVelocity.magnitude)
            {
                
                // ぶつけた側の反発処理＋ぶつかる側の反発処理
                _debrisMove.OnDebrisCollisionRecoil();
                hitDebris.debrisMove.BouncedDebris(_debrisMove._currentVelocity);
            }

        }

    }


    public override void OnCustomCollisionStay(SimpleShapeCollider2D collision)
    {
        if (collision.gameObject.CompareTag(_debrisTag))
        {
            DebrisComponents hitDebris = DebrisComponentManager.Instance.GetDebrisComponents(collision.gameObject);

            // 両方のデブリで重なり解消処理を実行
            _debrisMove.ResolvePenetration(collision.transform, hitDebris.debrisMove._currentVelocity);
            hitDebris.debrisMove.ResolvePenetration(transform, _debrisMove._currentVelocity);
        }
    }
    public override void OnCustomCollisionExit(SimpleShapeCollider2D collision)
    {
        // Implement the logic for OnCustomCollisionExit
    }

}
