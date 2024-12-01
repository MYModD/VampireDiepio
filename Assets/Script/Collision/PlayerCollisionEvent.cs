using UnityEngine;
using NaughtyAttributes;

public class PlayerCollisionEvent : CollisionEventBase
{
    [SerializeField,Tag] private string _debrisTag;
    [SerializeField,Tag] private string _enemyTag;
    [SerializeField,Tag] private string _bossTag;
    [SerializeField,Tag] private string _enemyBulletTag;

    [SerializeField] private PlayerMove _playerMove;
    [SerializeField,Tag] private PlayerHP _playerHP;
    public override void OnCustomCollisionEnter(SimpleShapeCollider2D collision)
    {

        // switch文で使えないのでif文 上を頻度が高いものにすると処理が早くなる
        if (collision.gameObject.CompareTag(_debrisTag))
        {
            //プレイヤーの反発処理、プレイヤーのHP処理、デブリの反発処理、デブリのHP処理
            _playerMove.OnDebrisCollision();
            _playerHP.OnDebirsDamage();



        }
        else if (collision.gameObject.CompareTag(_enemyTag))
        {
            // エネミーのHP処理、エネミーの反発処理、プレイヤーの反発処理、プレイヤーのHP処理
            _playerMove.OnEnemyCollision();


        }
        else if (collision.gameObject.CompareTag(_bossTag))
        {
            Debug.Log($"{collision.gameObject.name}:にヒットしたよ");
        }else if(collision.gameObject.CompareTag(_enemyBulletTag))
        {
            Debug.Log($"{collision.gameObject.name}:にヒットしたよ");
        }
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
