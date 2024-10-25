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
    private Vector2 _velocity;
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


        // �����_�ׂ����Ȃ�̂ł����v����
        //if(_velocity.sqrMagnitude < 0.01f)
        //{
        //    _velocity = Vector2.zero;
        //}

        
        // ���x�̐���
        _velocity = Vector2.ClampMagnitude(_velocity, _maxSpeed);

        _text.text = $"velocity : {_velocity.sqrMagnitude}";

        // vector2��3�ɂȂ����āA������
        transform.position += (Vector3)_velocity * Time.fixedDeltaTime;
    }

    public void Onmove(InputAction.CallbackContext context)
    {
        Debug.Log("WASD�����Ă܂��I");

        // Input
        _inputDirection = context.ReadValue<Vector2>();
       
    }
}
