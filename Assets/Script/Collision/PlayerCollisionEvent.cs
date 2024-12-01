using UnityEngine;
using NaughtyAttributes;

public class PlayerCollisionEvent : CollisionEventBase
{
    [SerializeField,Tag] private string _debrisTag;
    [SerializeField,Tag] private string _enemyTag;
    [SerializeField,Tag] private string _bossTag;
    [SerializeField,Tag] private string _enemyBulletTag;

    [SerializeField] private PlayerMove _playerMove;
    public override void OnCustomCollisionEnter(SimpleShapeCollider2D collision)
    {

        // switch���Ŏg���Ȃ��̂�if�� ���p�x���������̂ɂ���Ə����������Ȃ�
        if (collision.gameObject.CompareTag(_debrisTag))
        {
            //�����Ƀf�u����HP���炷����+
            Debug.Log("�f�u���Ƀq�b�g������");
            _playerMove.OnDebrisCollision();



        }
        else if (collision.gameObject.CompareTag(_enemyTag))
        {
            Debug.Log($"{collision.gameObject.name}:�Ƀq�b�g������");
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
