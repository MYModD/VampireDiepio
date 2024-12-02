using UnityEngine;


public class DebrisMove : MonoBehaviour
{
    [Header("減速度")]
    [SerializeField] private float _deceleration = 2f;

    [Header("速度ベクトル")]
    public Vector2 _currentVelocity = default;

    [Header("デブリ衝突時の減衰率")]
    [SerializeField, Range(0, 1f)] private float _debrisDeceleration = 0.5f;

    private void FixedUpdate()
    {
        // 慣性を持たせるために減速を追加
        _currentVelocity = Vector2.Lerp(_currentVelocity, Vector2.zero, _deceleration * Time.fixedDeltaTime);

        transform.position += (Vector3)_currentVelocity * Time.fixedDeltaTime;
    }


    /// <summary>
    /// Debris vs Player の処理
    /// </summary>
    /// <param name="playerVelocity"></param>
    public void BouncedPlayer(Vector2 playerVelocity)
    {

        _currentVelocity += playerVelocity;
    }

    /// <summary>
    ///  Debris vs Debris の処理
    /// </summary>
    /// <param name="playerVelocity"></param>
    public void BouncedDebris(Vector2 debrisVelocity)
    {
        _currentVelocity += debrisVelocity;

    }


    /// <summary>
    /// デブリ（ぶつけた側）の減衰処理
    /// </summary>
    public void OnDebrisCollisionRecoil()
    {
        // 全体的に減衰させる
        Debug.Log($"{gameObject.name}");
        _currentVelocity *= _debrisDeceleration;  // 入力速度を減少
    }


}
