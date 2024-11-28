using System;
using UnityEngine;

/// <summary>
/// 衝突判定の種類を定義
/// </summary>
public enum CollisionType2D
{
    Circle,
    Square
}

/// <summary>
/// カスタムコライダーコンポーネント
/// </summary>
public class SimpleShapeCollider2D : MonoBehaviour
{
    public CollisionType2D _collisionType;
    public float _size = 1f;

    // コライダーのイベントを定義
    public event Action<SimpleShapeCollider2D> OnCollisionEnter2D;
    public event Action<SimpleShapeCollider2D> OnCollisionStay2D;
    public event Action<SimpleShapeCollider2D> OnCollisionExit2D;

    private void OnEnable()
    {
        CollisionManager2D.Instance.RegisterCollider(this);
    }

    private void OnDisable()
    {
        CollisionManager2D.Instance.UnregisterCollider(this);
    }

    /// <summary>
    /// 他のコライダーとの衝突開始時の内部処理
    /// </summary>
    internal void OnCollisionEnter2DInternal(SimpleShapeCollider2D other)
    {
        OnCollisionEnter2D?.Invoke(other);
    }

    /// <summary>
    /// 他のコライダーとの衝突継続中の内部処理
    /// </summary>
    internal void OnCollisionStay2DInternal(SimpleShapeCollider2D other)
    {
        OnCollisionStay2D?.Invoke(other);
    }

    /// <summary>
    /// 他のコライダーとの衝突終了時の内部処理
    /// </summary>
    internal void OnCollisionExit2DInternal(SimpleShapeCollider2D other)
    {
        OnCollisionExit2D?.Invoke(other);
    }

    /// <summary>
    /// シーンビューでのコライダーの可視化
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        switch (_collisionType)
        {
            case CollisionType2D.Circle:
                Gizmos.DrawWireSphere(transform.position, _size);
                break;
            case CollisionType2D.Square:
                DrawRotatedRect();
                break;
        }
    }

    /// <summary>
    /// 回転した四角形のGizmosでの描画
    /// </summary>
    private void DrawRotatedRect()
    {
        Vector2[] corners = GetSquareCorners();
        for (int i = 0; i < 4; i++)
        {
            Gizmos.DrawLine(corners[i], corners[(i + 1) % 4]);
        }
    }

    /// <summary>
    /// 正方形の頂点座標を取得
    /// オブジェクトの回転を考慮して計算
    /// </summary>
    public Vector2[] GetSquareCorners()
    {
        Vector2[] corners = new Vector2[4];
        // オブジェクトのZ軸回転角度をラジアンに変換
        float rotationRad = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 pos = transform.position;

        for (int i = 0; i < 4; i++)
        {
            // 各頂点の角度を計算（オブジェクトの回転角度 + 90度ごとの角度）
            float angle = rotationRad + (i * 90f * Mathf.Deg2Rad);

            // 回転行列を使用して頂点の位置を計算
            corners[i] = pos + new Vector2(
                Mathf.Cos(angle) * _size,  // X座標
                Mathf.Sin(angle) * _size   // Y座標
            );
        }

        return corners;
    }
}