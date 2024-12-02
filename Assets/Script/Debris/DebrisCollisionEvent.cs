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
            // �R���|�[�l���g�������擾
            DebrisComponents hitDebris = DebrisComponentManager.Instance.GetDebrisComponents(collision.gameObject);

            // �f�u�����m���Ƃ��݂��ɏ������s���̂ŁA���x������������������
            if (_debrisMove._currentVelocity.magnitude > hitDebris.debrisMove._currentVelocity.magnitude)
            {
                
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
            DebrisComponents hitDebris = DebrisComponentManager.Instance.GetDebrisComponents(collision.gameObject);

            // �����̃f�u���ŏd�Ȃ�������������s
            _debrisMove.ResolvePenetration(collision.transform, hitDebris.debrisMove._currentVelocity);
            hitDebris.debrisMove.ResolvePenetration(transform, _debrisMove._currentVelocity);
        }
    }
    public override void OnCustomCollisionExit(SimpleShapeCollider2D collision)
    {
        // Implement the logic for OnCustomCollisionExit
    }

}
