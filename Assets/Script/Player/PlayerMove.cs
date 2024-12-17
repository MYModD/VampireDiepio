using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMove : MonoBehaviour
{
    [Header("入力の加速度"), Range(0, 10f)]
    [SerializeField] private float _inputAcceleration = 5f;

    [Header("入力の減速度"), Range(0, 10f)]
    [SerializeField] private float _inputDeceleration = 2f;

    [Header("入力の最高速度"), Range(0, 20f)]
    [SerializeField] private float _inputMaxSpeed = 10f;

    [Header("反動の強さ"), Range(0, 10f)]
    [SerializeField] private float _recoilForceMultiplier = 5f;

    [Header("反動の減衰率"), Range(0, 10f)]
    [SerializeField] private float _recoilDeceleration = 2f;

    [Header("反動の最高速度"), Range(0, 20f)]
    [SerializeField] private float _recoilMaxSpeed = 10f;

    [Header("全体の最高速度"), Range(0, 20f)]
    [SerializeField] private float _velocityMaxSpeed = 10f;

    [Header("デブリ衝突時の減衰率")]
    [SerializeField, Range(0, 1f)] private float _debrisDeceleration = 0.5f;

    [Header("デブリ衝突時の減衰率")]
    [SerializeField, Range(0, 1f)] private float _enemyDeceleration = 0.5f;


    [SerializeField] private DetaPlayerMove _playerMoveData; // ScriptableObjectへの参照


    [SerializeField] private TextMeshProUGUI _debugText; // デバッグ用テキスト



    // 全体の速度ベクトル
    public Vector2 _currentVelocity { private set; get; }
    // 入力の速度ベクトル
    private Vector2 _currentInputVelocity;
    // 反動の速度ベクトル
    private Vector2 _currentRecoilVelocity;

    // 入力ベクトル
    private Vector2 _inputDirection;


    private PlayerFire _playerFire;

    private void Awake()
    {
        _playerFire = GetComponent<PlayerFire>();



    }


    private void FixedUpdate()
    {
        UpdateVelocityAndPosition();
    }


    private void UpdateVelocityAndPosition()
    {

        // 入力ベクトルに加速度を加える
        _currentInputVelocity += _inputDirection * _inputAcceleration * Time.fixedDeltaTime;

        // 入力ベクトルの制限
        _currentInputVelocity = Vector2.ClampMagnitude(_currentInputVelocity, _inputMaxSpeed);

        // 入力がない場合は減速
        if (_inputDirection == Vector2.zero)
        {
            _currentInputVelocity = Vector2.Lerp(_currentInputVelocity, Vector2.zero, _inputDeceleration * Time.fixedDeltaTime);
        }

        // 反動ベクトルの制限
        _currentRecoilVelocity = Vector2.ClampMagnitude(_currentRecoilVelocity, _recoilMaxSpeed);

        // 反動ベクトルの減速
        _currentRecoilVelocity = Vector2.Lerp(_currentRecoilVelocity, Vector2.zero, _recoilDeceleration * Time.fixedDeltaTime);

        // 全体の速度ベクトルを合成 、全体の速度ベクトルの制限
        _currentVelocity = _currentInputVelocity + _currentRecoilVelocity;
        _currentVelocity = Vector2.ClampMagnitude(_currentVelocity, _velocityMaxSpeed);

        // デバッグ用テキスト更新
        _debugText.text = $"Velocity: {_currentVelocity.sqrMagnitude}";

        // 位置を更新
        transform.position += (Vector3)_currentVelocity * Time.fixedDeltaTime;

    }


    /// <summary>
    /// デブリに当たった時の速度減少処理
    /// </summary>
    public void ReduceVelocityOnDebrisHit()
    {
        // 全体的に減衰させる

        _currentInputVelocity *= _debrisDeceleration;  // 入力速度を減少
        _currentRecoilVelocity *= _debrisDeceleration; // 反動速度を減少
    }

    /// <summary>
    /// 敵に当たった時の速度減少処理
    /// </summary>
    public void ReduceVelocityOnEnemyHit()
    {
        _currentInputVelocity *= _debrisDeceleration;  // 入力速度を減少
        _currentRecoilVelocity *= _debrisDeceleration; // 反動速度を減少
    }


    public void AddRecoilForce(Vector2 force)
    {
        // 正規化 反動をつけるため逆にする
        force = force.normalized * -1;

        // 向き＊長さ(力)
        _currentRecoilVelocity += force * _recoilForceMultiplier;
    }


    /// <summary>
    /// 入力イベント用メソッド
    /// </summary>
    /// <param name="context">入力コンテキスト</param>
    public void OnMove(InputAction.CallbackContext context)
    {
        // 入力の方向を更新
        _inputDirection = context.ReadValue<Vector2>();
    }



#if UNITY_EDITOR
    [Button]
    private void DetaSaveScriptableObject()
    {

        // ScriptableObjectに値を保存
        _playerMoveData.InputAcceleration = _inputAcceleration;
        _playerMoveData.InputDeceleration = _inputDeceleration;
        _playerMoveData.InputMaxSpeed = _inputMaxSpeed;
        _playerMoveData.RecoilForceMultiplier = _recoilForceMultiplier;
        _playerMoveData.RecoilDeceleration = _recoilDeceleration;
        _playerMoveData.RecoilMaxSpeed = _recoilMaxSpeed;
        _playerMoveData.VelocityMaxSpeed = _velocityMaxSpeed;
        _playerMoveData.DebrisDeceleration = _debrisDeceleration;
        _playerMoveData.EnemyDeceleration = _enemyDeceleration;

        // 変更を保存
        UnityEditor.EditorUtility.SetDirty(_playerMoveData);
        UnityEditor.AssetDatabase.SaveAssets();

        Debug.Log("設定を保存しました");
    }

    [SerializeField]    
    [Button]
    private void DetaLoadScriptableObject()
    {
        // ScriptableObjectから値を読み込む
        _inputAcceleration = _playerMoveData.InputAcceleration;
        _inputDeceleration = _playerMoveData.InputDeceleration;
        _inputMaxSpeed = _playerMoveData.InputMaxSpeed;
        _recoilForceMultiplier = _playerMoveData.RecoilForceMultiplier;
        _recoilDeceleration = _playerMoveData.RecoilDeceleration;
        _recoilMaxSpeed = _playerMoveData.RecoilMaxSpeed;
        _velocityMaxSpeed = _playerMoveData.VelocityMaxSpeed;
        _debrisDeceleration = _playerMoveData.DebrisDeceleration;
        _enemyDeceleration = _playerMoveData.EnemyDeceleration;

    }
#endif

    /// <summary>
    /// Debug用のGizmos描画
    /// </summary>
    private void OnDrawGizmos()
    {
        // Gizmosを使用してベクトルを可視化

        // x, y のそれぞれの長さを計算
        Vector3 xLength = new Vector3(_currentVelocity.x, 0, 0);
        Vector3 yLength = new Vector3(0, _currentVelocity.y, 0);

        // 全体の長さを計算
        Vector3 totalLength = new Vector3(_currentVelocity.x, _currentVelocity.y, 0);

        // x, y の Gizmos を描画
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + xLength);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + yLength);

        // 全体のベクトルを描画
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + totalLength);

    }
}
