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
            //�����ɔ������鏈��������
            DebrisComponents hitDebris = DebrisComponentManager.Instance.GetDebrisComponents(collision.gameObject);

            // �f�u�����m���Ƃ��݂��ɏ������s���̂ŁA���x������������������
            if (_debrisMove._currentVelocity.magnitude > hitDebris.debrisMove._currentVelocity.magnitude)
            {
                // �f�u�����������ɏ����𕪂���

                // �Ԃ������̔��������{�Ԃ��鑤�̔�������
                _debrisMove.OnDebrisCollisionRecoil();
                hitDebris.debrisMove.BouncedDebris(_debrisMove._currentVelocity);
            }

        }

    }


    public override void OnCustomCollisionStay(SimpleShapeCollider2D collision)
    {
        if (collision.gameObject.CompareTag(_debrisTag))
        {

            // �e�X�g�p�ϐ�
            float _pushForce = 1.0f;

            DebrisComponents hitDebris = DebrisComponentManager.Instance.GetDebrisComponents(collision.gameObject);

            // �d�Ȃ����������������v�Z
            Vector2 direction = transform.position - collision.transform.position;
            direction.Normalize();

            // ��葬�x�̒x�����������o�����
            if (_debrisMove._currentVelocity.magnitude < hitDebris.debrisMove._currentVelocity.magnitude)
            {
                // �����������o��
                transform.position += (Vector3)(direction * _pushForce * Time.deltaTime);

                // �K�v�ɉ����đ��x������
                _debrisMove._currentVelocity += (Vector2)(direction * _pushForce * Time.deltaTime);
            }
            else
            {
                // ����������o��
                collision.transform.position -= (Vector3)(direction * _pushForce * Time.deltaTime);

                // ����̑��x������
                hitDebris.debrisMove._currentVelocity -= (Vector2)(direction * _pushForce * Time.deltaTime);
            }
        }
    }
    public override void OnCustomCollisionExit(SimpleShapeCollider2D collision)
    {
        // Implement the logic for OnCustomCollisionExit
    }

}
