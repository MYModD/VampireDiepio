using UnityEngine;

public class BulletMove : MonoBehaviour
{
    
    [Header("�����x")]
    [SerializeField] private float _deceleration = 2f;

    [Header("�ō����x")]
    [SerializeField] private float _maxSpeed = 10f;

    [Header("�^�C�}�[")]
    [SerializeField] private float _timer = 5f;

    [Header("���x�x�N�g��")]
    public Vector2 _velocity = default;

    private float _timerValue = default; // ���Ԍv�Z�p
    
    
    private void FixedUpdate()
    {
        // �^�C�}�[��0�ɂȂ�����I�u�W�F�N�gOFF
        _timerValue = _timerValue - Time.fixedDeltaTime;
        if (_timerValue <= 0)
        {
            this.gameObject.SetActive(false);
            Debug.Log("BulletMove��OFF�ɂ��܂���");
        }

        // �������������邽�߂Ɍ�����ǉ�
        _velocity = Vector2.Lerp(_velocity, Vector2.zero, _deceleration * Time.fixedDeltaTime);

        transform.position += (Vector3)_velocity * Time.fixedDeltaTime;

    }
    public void AddForce(Vector2 force)
    {
        _velocity += force;

    }
    private void OnEnable()
    {
        _timerValue = _timer;
    }


}
