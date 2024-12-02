using UnityEngine;

public class DebrisMove : MonoBehaviour
{

    [Header("�����x")]
    [SerializeField] private float _deceleration = 2f;
    [Header("���x�x�N�g��")]
    public Vector2 _currentVelocity = default;
    [Header("�f�u���Փˎ��̌�����")]
    [SerializeField, Range(0, 1f)] private float _debrisDeceleration = 0.5f;


    [Header("�d�Ȃ�h�~")]
    [SerializeField] private float _pushForce = 5f;
    [SerializeField] private float _minPushDistance = 0.1f;  // ���̋����ȉ��Ȃ牟���o��
    [SerializeField] private float _maxPushVelocity = 5f;
    private void FixedUpdate()
    {

        _currentVelocity = Vector2.Lerp(_currentVelocity, Vector2.zero, _deceleration * Time.fixedDeltaTime);
        transform.position += (Vector3)_currentVelocity * Time.fixedDeltaTime;
    }

    /// <summary>
    /// �d�Ȃ��Ă���f�u���������o������
    /// </summary>
    /// <param name="otherDebris">����̃f�u��</param>
    /// <param name="otherVelocity">����̑��x</param>
    public void ResolvePenetration(Transform otherDebris, Vector2 otherVelocity)
    {
        Vector2 direction = (transform.position - otherDebris.position).normalized;
        float distance = Vector2.Distance(transform.position, otherDebris.position);

        if (distance < _minPushDistance)
        {
            if (_currentVelocity.magnitude <= otherVelocity.magnitude)
            {
                // �ʒu�̒���
                transform.position += (Vector3)(direction * _pushForce * Time.fixedDeltaTime);

                // ���x�̒����i�����ݒ�j
                Vector2 pushVelocity = direction * _pushForce * Time.fixedDeltaTime;
                _currentVelocity += pushVelocity;

                // ���x�ɏ����ݒ�
                if (_currentVelocity.magnitude > _maxPushVelocity)
                {
                    _currentVelocity = _currentVelocity.normalized * _maxPushVelocity;
                }

                // ���Ε����ւ̑��x���L�����Z���i���S�ɏd�Ȃ������̔��˖h�~�j
                if (Vector2.Dot(_currentVelocity, otherVelocity) < 0)
                {
                    _currentVelocity *= _debrisDeceleration;
                }
            }
        }
    }

    // �����̃��\�b�h
    public void BouncedPlayer(Vector2 playerVelocity)
    {
        _currentVelocity += playerVelocity;
    }

    public void BouncedDebris(Vector2 debrisVelocity)
    {
        _currentVelocity += debrisVelocity;
    }

    public void OnDebrisCollisionRecoil()
    {
        Debug.Log($"{gameObject.name}");
        _currentVelocity *= _debrisDeceleration;
    }

}