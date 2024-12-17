using UnityEngine;

public class BulletMove : MonoBehaviour
{
    
    [Header("�����x")]
    [SerializeField] private float _deceleration = 2f;

    [Header("�^�C�}�[")]
    [SerializeField] private float _timer = 5f;

    [Header("���x�x�N�g��")]
    public Vector2 _currentVelocity = default;



    private float _timerValue = default; // ���Ԍv�Z�p
    private PoolableBullet _poolableBullet = default;



    private void Awake()
    {
        _poolableBullet = GetComponent<PoolableBullet>();
    }

    private void FixedUpdate()
    {
        // �^�C�}�[��0�ɂȂ�����I�u�W�F�N�gOFF
        _timerValue = _timerValue - Time.fixedDeltaTime;
        if (_timerValue <= 0)
        {
            this.gameObject.SetActive(false);
            _poolableBullet.ReturnToPool();
            
        }

        // �������������邽�߂Ɍ�����ǉ�
        _currentVelocity = Vector2.Lerp(_currentVelocity, Vector2.zero, _deceleration * Time.fixedDeltaTime);

        transform.position += (Vector3)_currentVelocity * Time.fixedDeltaTime;
          

    }
    

    public void AddForce(Vector2 force)
    {
        _currentVelocity += force;

    }

    /// <summary>
    /// ���x�̏�����
    /// </summary>
    public void InitialVelocity()
    {
        _currentVelocity = Vector2.zero;
    }

    /// <summary>
    /// �^�C�}�[�̏�����
    /// </summary>
    public void InitialTimer()
    {
        _timerValue = _timer;
    }

}
