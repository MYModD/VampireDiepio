using UnityEngine;
using NaughtyAttributes;

public class PlayerCollisionEvent : CollisionEventBase
{
    [SerializeField,Tag] private string _debrisTag;
    [SerializeField,Tag] private string _enemyTag;
    [SerializeField,Tag] private string _bossTag;
    [SerializeField,Tag] private string _enemyBulletTag;


    private PlayerMove _playerMove;
    private PlayerHP _playerHP;

    protected override void Awake()
    {
        base.Awake();
        _playerMove = GetComponent<PlayerMove>();
        _playerHP = GetComponent<PlayerHP>();
    }
    public override void OnCustomCollisionEnter(SimpleShapeCollider2D collision)
    {

        // switch文が使えないのでif文
        if (collision.gameObject.CompareTag(_debrisTag))
        {
            //プレイヤーの反発処理、プレイヤーのHP処理、デブリの反発処理、デブリのHP処理
            _playerMove.ReduceVelocityOnDebrisHit();
            _playerHP.OnDebirsDamage();


            if (DebrisComponentManager.Instance == null) Debug.LogError("原因ここ");   


            DebrisComponents debris = DebrisComponentManager.Instance.GetComponents(collision.gameObject);
            

            //ここ直す
            debris.debrisMove.BouncedPlayer(_playerMove._currentVelocity);
            debris.debrisHP.DecreaseHP(10);


        }
        else if (collision.gameObject.CompareTag(_enemyTag))
        {
            // エネミーのHP処理、エネミーの反発処理、プレイヤーの反発処理、プレイヤーのHP処理
            _playerMove.ReduceVelocityOnEnemyHit();


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
