using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float _timer = 5f;

    private float _timerValue = default;
    // 物体の質量
    private float _mass;

    // 現在の速度
    private Vector3 _velocity;

    // 位置
    private Vector3 _position;

    //

    // 現在の位置を取得
    public Vector3 Position => _position;

    // 現在の速度を取得・設定
    public Vector3 Velocity
    {
        get => _velocity;
        set => _velocity = value;
    }

    // 力を加えるメソッド
    public void AddForce(Vector3 force)
    {
        // 加速度を計算 (F = ma)
        Vector3 acceleration = force / _mass;

        // 現在の速度に加速度を加える
        _velocity += acceleration * Time.deltaTime; // Δtに基づいて速度を更新

        // 位置を更新
        _position += _velocity * Time.deltaTime; // 新しい位置を計算
    }

    // 物体を更新するメソッド
    public void Update()
    {
        // ここでは物理的な更新を行う（位置を速度に基づいて更新するなど）
        _position += _velocity * Time.deltaTime;
    }



    private void OnEnable()
    {
        _timerValue = _timer;
    }

    
}
