using UnityEngine;


public class DebrisMove : MonoBehaviour
{
    [Header("�����x")]
    [SerializeField] private float _deceleration = 2f;

    [Header("���x�x�N�g��")]
    public Vector2 _currentVelocity = default;

    [Header("�f�u���Փˎ��̌�����")]
    [SerializeField, Range(0, 1f)] private float _debrisDeceleration = 0.5f;

    private void FixedUpdate()
    {
        // �������������邽�߂Ɍ�����ǉ�
        _currentVelocity = Vector2.Lerp(_currentVelocity, Vector2.zero, _deceleration * Time.fixedDeltaTime);

        transform.position += (Vector3)_currentVelocity * Time.fixedDeltaTime;
    }


    /// <summary>
    /// Debris vs Player �̏���
    /// </summary>
    /// <param name="playerVelocity"></param>
    public void BouncedPlayer(Vector2 playerVelocity)
    {

        _currentVelocity += playerVelocity;
    }

    /// <summary>
    ///  Debris vs Debris �̏���
    /// </summary>
    /// <param name="playerVelocity"></param>
    public void BouncedDebris(Vector2 debrisVelocity)
    {
        _currentVelocity += debrisVelocity;

    }


    /// <summary>
    /// �f�u���i�Ԃ������j�̌�������
    /// </summary>
    public void OnDebrisCollisionRecoil()
    {
        // �S�̓I�Ɍ���������
        Debug.Log($"{gameObject.name}");
        _currentVelocity *= _debrisDeceleration;  // ���͑��x������
    }


}
