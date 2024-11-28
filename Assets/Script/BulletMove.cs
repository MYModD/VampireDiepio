using UnityEngine;

public class BulletMove : MonoBehaviour
{
    
    [Header("減速度")]
    [SerializeField] private float _deceleration = 2f;

    [Header("最高速度")]
    [SerializeField] private float _maxSpeed = 10f;

    [Header("タイマー")]
    [SerializeField] private float _timer = 5f;

    [Header("速度ベクトル")]
    public Vector2 _velocity = default;

    private float _timerValue = default; // 時間計算用
    
    
    private void FixedUpdate()
    {
        // タイマーが0になったらオブジェクトOFF
        _timerValue = _timerValue - Time.fixedDeltaTime;
        if (_timerValue <= 0)
        {
            this.gameObject.SetActive(false);
            Debug.Log("BulletMoveをOFFにしました");
        }

        // 慣性を持たせるために減速を追加
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
