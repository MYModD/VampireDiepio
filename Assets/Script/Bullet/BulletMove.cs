using UnityEngine;

public class BulletMove : MonoBehaviour
{
    
    [Header("減速度")]
    [SerializeField] private float _deceleration = 2f;

    [Header("タイマー")]
    [SerializeField] private float _timer = 5f;

    [Header("速度ベクトル")]
    public Vector2 _currentVelocity = default;



    private float _timerValue = default; // 時間計算用
    private PoolableBullet _poolableBullet = default;



    private void Awake()
    {
        _poolableBullet = GetComponent<PoolableBullet>();
    }

    private void FixedUpdate()
    {
        // タイマーが0になったらオブジェクトOFF
        _timerValue = _timerValue - Time.fixedDeltaTime;
        if (_timerValue <= 0)
        {
            this.gameObject.SetActive(false);
            _poolableBullet.ReturnToPool();
            
        }

        // 慣性を持たせるために減速を追加
        _currentVelocity = Vector2.Lerp(_currentVelocity, Vector2.zero, _deceleration * Time.fixedDeltaTime);

        transform.position += (Vector3)_currentVelocity * Time.fixedDeltaTime;
          

    }
    

    public void AddForce(Vector2 force)
    {
        _currentVelocity += force;

    }

    /// <summary>
    /// 速度の初期化
    /// </summary>
    public void InitialVelocity()
    {
        _currentVelocity = Vector2.zero;
    }

    /// <summary>
    /// タイマーの初期化
    /// </summary>
    public void InitialTimer()
    {
        _timerValue = _timer;
    }

}
