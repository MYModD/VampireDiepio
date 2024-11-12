// 衝突判定の種類を定義
using System;
using UnityEngine;

public enum CollisionType2D
{
    Circle,
    Triangle,
    Square
}

// カスタムコライダーコンポーネント
public class SimpleShapeCollider2D :  MonoBehaviour
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

    // 他のコライダーと衝突した時の内部処理
    internal void OnCollisionEnter2DInternal(SimpleShapeCollider2D other)
    {
        OnCollisionEnter2D?.Invoke(other);
    }

    internal void OnCollisionStay2DInternal(SimpleShapeCollider2D other)
    {
        OnCollisionStay2D?.Invoke(other);
    }

    internal void OnCollisionExit2DInternal(SimpleShapeCollider2D other)
    {
        OnCollisionExit2D?.Invoke(other);
    }

    // Gizmoの描画
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
            case CollisionType2D.Triangle:
                DrawTriangle();
                break;
        }
    }

    private void DrawRotatedRect()
    {
        Vector2[] corners = GetSquareCorners();
        for (int i = 0; i < 4; i++)
        {
            Gizmos.DrawLine(corners[i], corners[(i + 1) % 4]);
        }
    }

    private void DrawTriangle()
    {
        Vector2[] corners = GetTriangleCorners();
        for (int i = 0; i < 3; i++)
        {
            Gizmos.DrawLine(corners[i], corners[(i + 1) % 3]);
        }
    }

    // 各形状の頂点を取得
    public Vector2[] GetTriangleCorners()
    {
        Vector2[] corners = new Vector2[3];
        float rotationRad = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 pos = transform.position;

        for (int i = 0; i < 3; i++)
        {
            float angle = rotationRad + (i * 120f * Mathf.Deg2Rad);
            corners[i] = pos + new Vector2(
                Mathf.Cos(angle) * _size,
                Mathf.Sin(angle) * _size
            );
        }
        return corners;
    }

    public Vector2[] GetSquareCorners()
    {
        Vector2[] corners = new Vector2[4];
        float rotationRad = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 pos = transform.position;

        for (int i = 0; i < 4; i++)
        {
            float angle = rotationRad + (i * 90f * Mathf.Deg2Rad);
            corners[i] = pos + new Vector2(
                Mathf.Cos(angle) * _size,
                Mathf.Sin(angle) * _size
            );
        }
        return corners;
    }
}
