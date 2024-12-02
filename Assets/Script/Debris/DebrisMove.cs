using UnityEngine;

public class DebrisMove : MonoBehaviour
{

    [Header("減速度")]
    [SerializeField] private float _deceleration = 2f;
    [Header("速度ベクトル")]
    public Vector2 _currentVelocity = default;
    [Header("デブリ衝突時の減衰率")]
    [SerializeField, Range(0, 1f)] private float _debrisDeceleration = 0.5f;


    [Header("重なり防止")]
    [SerializeField] private float _pushForce = 5f;
    [SerializeField] private float _minPushDistance = 0.1f;  // この距離以下なら押し出す
    [SerializeField] private float _maxPushVelocity = 5f;
    private void FixedUpdate()
    {

        _currentVelocity = Vector2.Lerp(_currentVelocity, Vector2.zero, _deceleration * Time.fixedDeltaTime);
        transform.position += (Vector3)_currentVelocity * Time.fixedDeltaTime;
    }

    /// <summary>
    /// 重なっているデブリを押し出す処理
    /// </summary>
    /// <param name="otherDebris">相手のデブリ</param>
    /// <param name="otherVelocity">相手の速度</param>
    public void ResolvePenetration(Transform otherDebris, Vector2 otherVelocity)
    {
        Vector2 direction = (transform.position - otherDebris.position).normalized;
        float distance = Vector2.Distance(transform.position, otherDebris.position);

        if (distance < _minPushDistance)
        {
            if (_currentVelocity.magnitude <= otherVelocity.magnitude)
            {
                // 位置の調整
                transform.position += (Vector3)(direction * _pushForce * Time.fixedDeltaTime);

                // 速度の調整（上限を設定）
                Vector2 pushVelocity = direction * _pushForce * Time.fixedDeltaTime;
                _currentVelocity += pushVelocity;

                // 速度に上限を設定
                if (_currentVelocity.magnitude > _maxPushVelocity)
                {
                    _currentVelocity = _currentVelocity.normalized * _maxPushVelocity;
                }

                // 反対方向への速度をキャンセル（完全に重なった時の発射防止）
                if (Vector2.Dot(_currentVelocity, otherVelocity) < 0)
                {
                    _currentVelocity *= _debrisDeceleration;
                }
            }
        }
    }

    // 既存のメソッド
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