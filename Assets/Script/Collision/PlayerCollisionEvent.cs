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

        // switch���Ŏg���Ȃ��̂�if�� ���p�x���������̂ɂ���Ə����������Ȃ�
        if (collision.gameObject.CompareTag(_debrisTag))
        {
            //�v���C���[�̔��������A�v���C���[��HP�����A�f�u���̔��������A�f�u����HP����
            _playerMove.OnDebrisCollision();
            _playerHP.OnDebirsDamage();



        }
        else if (collision.gameObject.CompareTag(_enemyTag))
        {
            // �G�l�~�[��HP�����A�G�l�~�[�̔��������A�v���C���[�̔��������A�v���C���[��HP����
            _playerMove.OnEnemyCollision();


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
