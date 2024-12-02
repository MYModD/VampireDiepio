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
            //ここに反発する処理を書く
            DebrisComponents hitDebris = DebrisComponentManager.Instance.GetDebrisComponents(collision.gameObject);

            // デブリ同士だとお互いに処理を行うので、速度が速い方が処理する
            if (_debrisMove._currentVelocity.magnitude > hitDebris.debrisMove._currentVelocity.magnitude)
            {
                // デブリが早い方に処理を分ける

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

            // テスト用変数
            float _pushForce = 1.0f;

            DebrisComponents hitDebris = DebrisComponentManager.Instance.GetDebrisComponents(collision.gameObject);

            // 重なりを解消する方向を計算
            Vector2 direction = transform.position - collision.transform.position;
            direction.Normalize();

            // より速度の遅い方が押し出される
            if (_debrisMove._currentVelocity.magnitude < hitDebris.debrisMove._currentVelocity.magnitude)
            {
                // 自分を押し出す
                transform.position += (Vector3)(direction * _pushForce * Time.deltaTime);

                // 必要に応じて速度も調整
                _debrisMove._currentVelocity += (Vector2)(direction * _pushForce * Time.deltaTime);
            }
            else
            {
                // 相手を押し出す
                collision.transform.position -= (Vector3)(direction * _pushForce * Time.deltaTime);

                // 相手の速度も調整
                hitDebris.debrisMove._currentVelocity -= (Vector2)(direction * _pushForce * Time.deltaTime);
            }
        }
    }
    public override void OnCustomCollisionExit(SimpleShapeCollider2D collision)
    {
        // Implement the logic for OnCustomCollisionExit
    }

}
