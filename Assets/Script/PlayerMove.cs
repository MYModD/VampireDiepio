using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMove : SerializedMonoBehaviour
{

    [Header("�����x")]
    [SerializeField] private float _acceleration = 5f;
    [Header("�����x")]
    [SerializeField] private float _deceleration = 2f;
    [Header("���͂̍ō����x")]
    [SerializeField] private float _inputMaxSpeed = 10f;
    [Header("��������ꂽ�ō����x")]
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
        // �L�����Z���g�[�N����邼�I�I�I

        // �����x
        _inputVelocity += _inputDirection * _acceleration * Time.fixedDeltaTime;


        // ���͑��x�̐���
        _inputVelocity = Vector2.ClampMagnitude(_velocity, _recoilMaxSpeed);

        // ��������Ă��Ȃ��ꍇ
        // �������������邽�߂Ɍ�����ǉ�
        if (_inputDirection == Vector2.zero)
        {
            _inputVelocity = Vector2.Lerp(_velocity, Vector2.zero, _deceleration * Time.fixedDeltaTime);
        }

        // UI debug�p
        _text.text = $"velocity : {_velocity.sqrMagnitude}";

        // vector2��3�ɂȂ����āA������
        transform.position += (Vector3)_velocity * Time.fixedDeltaTime;
    }
    public void AddForce(Vector2 force)
    {
        _recoilVelocity += force;
        // ���������킹�����x�̐���
        _recoilVelocity = Vector2.ClampMagnitude(_recoilVelocity, _recoilMaxSpeed);
        
    }

    async UniTask hoge(CancellationToken token)
    {

        await UniTask.Delay(1);
    }

    /// <summary>
    /// ���̓C�x���g�p
    /// </summary>
    /// <param name="context"></param>
    public void Onmove(InputAction.CallbackContext context)
    {
        // Input
        _inputDirection = context.ReadValue<Vector2>();

    }

    private void OnDrawGizmos()
    {

        // x y ���ꂼ��̒���
        Vector3 xLength = new Vector3(_velocity.x, 0, 0);
        Vector3 yLength = new Vector3(0, _velocity.y, 0);

        // �S�̂̒���
        Vector3 Length = new Vector3(_velocity.x, _velocity.y, 0);

        // x y ��DrawGizmo
        Gizmos.color = Color.green;
        Gizmos.DrawLine(this.transform.position, transform.position + xLength);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(this.transform.position, transform.position + yLength);


        // �S�̂�DrawGizmo
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, transform.position + Length);
    }

}
