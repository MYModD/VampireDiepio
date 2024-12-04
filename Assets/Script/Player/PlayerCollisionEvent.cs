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

        // switch�����g���Ȃ��̂�if��
        if (collision.gameObject.CompareTag(_debrisTag))
        {
            //�v���C���[�̔��������A�v���C���[��HP�����A�f�u���̔��������A�f�u����HP����
            _playerMove.ReduceVelocityOnDebrisHit();
            _playerHP.OnDebirsDamage();


            if (DebrisComponentManager.Instance == null) Debug.LogError("��������");   


            DebrisComponents debris = DebrisComponentManager.Instance.GetComponents(collision.gameObject);
            

            //��������
            debris.debrisMove.BouncedPlayer(_playerMove._currentVelocity);
            debris.debrisHP.DecreaseHP(10);


        }
        else if (collision.gameObject.CompareTag(_enemyTag))
        {
            // �G�l�~�[��HP�����A�G�l�~�[�̔��������A�v���C���[�̔��������A�v���C���[��HP����
            _playerMove.ReduceVelocityOnEnemyHit();


        }
        else if (collision.gameObject.CompareTag(_bossTag))
        {
            Debug.Log($"{collision.gameObject.name}:�Ƀq�b�g������");

        }else if(collision.gameObject.CompareTag(_enemyBulletTag))
        {

            Debug.Log($"{collision.gameObject.name}:�Ƀq�b�g������");
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
