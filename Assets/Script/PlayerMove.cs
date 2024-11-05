using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMove : SerializedMonoBehaviour
{

    [Header("加速度")]
    [SerializeField] private float _acceleration = 5f;
    [Header("減速度")]
    [SerializeField] private float _deceleration = 2f;
    [Header("入力の最高速度")]
    [SerializeField] private float _inputMaxSpeed = 10f;
    [Header("反動を入れた最高速度")]
    [SerializeField] private float _recoilMaxSpeed = 10f;


    public Vector2 _velocity;
    public Vector2 _recoilVelocity;
    public Vector2 _inputVelocity;
    public TextMeshProUGUI _text;
    public PlayerFire _playerFire;
    private Vector2 _inputDirection;

    private void Awake()
    {
        var cts = new CancellationToken();
    }
    void FixedUpdate()
    {
        // キャンセルトークン作るぞ！！！

        // 加速度
        _inputVelocity += _inputDirection * _acceleration * Time.fixedDeltaTime;


        // 入力速度の制限
        _inputVelocity = Vector2.ClampMagnitude(_velocity, _recoilMaxSpeed);

        // 操作をしていない場合
        // 慣性を持たせるために減速を追加
        if (_inputDirection == Vector2.zero)
        {
            _inputVelocity = Vector2.Lerp(_velocity, Vector2.zero, _deceleration * Time.fixedDeltaTime);
        }

        // UI debug用
        _text.text = $"velocity : {_velocity.sqrMagnitude}";

        // vector2を3になおして、かける
        transform.position += (Vector3)_velocity * Time.fixedDeltaTime;
    }
    public void AddForce(Vector2 force)
    {
        _recoilVelocity += force;
        // 反動をあわせた速度の制限
        _recoilVelocity = Vector2.ClampMagnitude(_recoilVelocity, _recoilMaxSpeed);
        
    }

    async UniTask hoge(CancellationToken token)
    {

        await UniTask.Delay(1);
    }

    /// <summary>
    /// 入力イベント用
    /// </summary>
    /// <param name="context"></param>
    public void Onmove(InputAction.CallbackContext context)
    {
        // Input
        _inputDirection = context.ReadValue<Vector2>();

    }

    private void OnDrawGizmos()
    {

        // x y それぞれの長さ
        Vector3 xLength = new Vector3(_velocity.x, 0, 0);
        Vector3 yLength = new Vector3(0, _velocity.y, 0);

        // 全体の長さ
        Vector3 Length = new Vector3(_velocity.x, _velocity.y, 0);

        // x y のDrawGizmo
        Gizmos.color = Color.green;
        Gizmos.DrawLine(this.transform.position, transform.position + xLength);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(this.transform.position, transform.position + yLength);


        // 全体のDrawGizmo
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, transform.position + Length);
    }

}
