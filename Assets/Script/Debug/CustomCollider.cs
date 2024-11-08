using UnityEngine;
using System;

public abstract class CustomCollider : MonoBehaviour
{
    // シリアライズ可能な図形のデータ
    [SerializeField] protected Vector2 _position;
    [SerializeField] protected float _radius;     // 円形用
    [SerializeField] protected Vector2 _size;     // 四角形用
    [SerializeField] protected Vector2[] _vertices; // 三角形の頂点

    // 衝突イベント
    public event Action<CustomCollider> OnCollision;

    private void Update()
    {
        _position = transform.position;
    }

    // 衝突判定メソッド（図形ごとにオーバーライドする）
    public abstract bool CheckCollision(CustomCollider other);

    // 衝突が発生したときに呼び出されるメソッド
    protected void TriggerCollision(CustomCollider other)
    {
        OnCollision?.Invoke(other);
    }
}
