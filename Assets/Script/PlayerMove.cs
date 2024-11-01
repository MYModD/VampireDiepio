using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMove : MonoBehaviour
{
    
    [Header("�����x")]
    [SerializeField] private float _acceleration = 5f;  
    [Header("�����x")]
    [SerializeField] private float _deceleration = 2f;  
    [Header("�ō����x")]
    [SerializeField] private float _maxSpeed = 10f;

    public TextMeshProUGUI _text;
    public PlayerFire _playerFire;
    [HideInInspector]
    public Vector2 _velocity;
    private Vector2 _inputDirection;

    void FixedUpdate()
    {
        _velocity += _inputDirection * _acceleration * Time.fixedDeltaTime;
        
        // ��������Ă��Ȃ��ꍇ
        // �������������邽�߂Ɍ�����ǉ�
        if (_inputDirection == Vector2.zero)
        {
            _velocity = Vector2.Lerp(_velocity, Vector2.zero, _deceleration * Time.fixedDeltaTime);
          }

        // ���x�̐���
        _velocity = Vector2.ClampMagnitude(_velocity, _maxSpeed);

        _text.text = $"velocity : {_velocity.sqrMagnitude}";

        // vector2��3�ɂȂ����āA������
        transform.position += (Vector3)_velocity * Time.fixedDeltaTime;
    }

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
