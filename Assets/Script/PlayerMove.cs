using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMove : MonoBehaviour
{
    
    [Header("加速度")]
    [SerializeField] private float _acceleration = 5f;  
    [Header("減速度")]
    [SerializeField] private float _deceleration = 2f;  
    [Header("最高速度")]
    [SerializeField] private float _maxSpeed = 10f;

    public TextMeshProUGUI _text;
    private Vector2 _velocity;
    private Vector2 _inputDirection;

    void FixedUpdate()
    {

       
        _velocity += _inputDirection * _acceleration * Time.fixedDeltaTime;
        
        // 操作をしていない場合
        // 慣性を持たせるために減速を追加
        if (_inputDirection == Vector2.zero)
        {
            _velocity = Vector2.Lerp(_velocity, Vector2.zero, _deceleration * Time.fixedDeltaTime);
           
        }


        // 小数点細かくなるのでここ要調整
        //if(_velocity.sqrMagnitude < 0.01f)
        //{
        //    _velocity = Vector2.zero;
        //}

        
        // 速度の制限
        _velocity = Vector2.ClampMagnitude(_velocity, _maxSpeed);

        _text.text = $"velocity : {_velocity.sqrMagnitude}";

        // vector2を3になおして、かける
        transform.position += (Vector3)_velocity * Time.fixedDeltaTime;
    }

    public void Onmove(InputAction.CallbackContext context)
    {
        Debug.Log("WASD押してます！");

        // Input
        _inputDirection = context.ReadValue<Vector2>();
       
    }
}
