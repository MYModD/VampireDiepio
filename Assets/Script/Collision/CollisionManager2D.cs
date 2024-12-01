using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionManager2D : Diepio.Singleton<CollisionManager2D>
{
    // 登録された全てのコライダーのリスト
    private List<SimpleShapeCollider2D> _colliders = new List<SimpleShapeCollider2D>();

    // 現在衝突中のコライダーペアを保持するセット
    private HashSet<(SimpleShapeCollider2D, SimpleShapeCollider2D)> _currentCollisions = new HashSet<(SimpleShapeCollider2D, SimpleShapeCollider2D)>();

    /// <summary>
    /// シングルトンの初期化
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
    }


    protected override void OnDestroy()
    {
        base.OnDestroy();

        // シーン遷移時にリストをクリア
        if (_colliders != null)
        {
            _colliders.Clear();
        }
        if (_currentCollisions != null)
        {
            _currentCollisions.Clear();
        }

    }


    protected override void OnApplicationQuit()
    {

        base.OnApplicationQuit();
        // アプリケーション終了時にクリーンアップを確実に行う
        if (_colliders != null)
        {
            _colliders.Clear();
        }
        if (_currentCollisions != null)
        {
            _currentCollisions.Clear();
        }
    }

    /// <summary>
    /// 毎フレーム実行される当たり判定チェック
    /// </summary>
    private void Update()
    {
        CheckCollisions();
    }

    /// <summary>
    /// 新しいコライダーを登録
    /// </summary>
    public void RegisterCollider(SimpleShapeCollider2D collider)
    {
        if (!_colliders.Contains(collider))
        {
            _colliders.Add(collider);
        }
    }


    /// <summary>
    /// コライダーの登録解除と、関連する衝突情報の削除
    /// </summary>
    public void UnregisterCollider(SimpleShapeCollider2D collider)
    {
        _colliders.Remove(collider);
        _currentCollisions.RemoveWhere(pair => pair.Item1 == collider || pair.Item2 == collider);
    }

    /// <summary>
    /// 全てのコライダー間の衝突判定を実行
    /// </summary>
    private void CheckCollisions()
    {
        HashSet<(SimpleShapeCollider2D, SimpleShapeCollider2D)> newCollisions = new HashSet<(SimpleShapeCollider2D, SimpleShapeCollider2D)>();

        for (int i = 0; i < _colliders.Count; i++)
        {
            for (int j = i + 1; j < _colliders.Count; j++)
            {
                SimpleShapeCollider2D a = _colliders[i];
                SimpleShapeCollider2D b = _colliders[j];

                bool isColliding = IsColliding(a, b);
                if (isColliding)
                {
                    var pair = (a, b);
                    newCollisions.Add(pair);

                    if (!_currentCollisions.Contains(pair))
                    {
                        // 衝突開始時の処理
                        a.OnCollisionEnter2DInternal(b);
                        b.OnCollisionEnter2DInternal(a);
                    }
                    else
                    {
                        // 衝突継続中の処理
                        a.OnCollisionStay2DInternal(b);
                        b.OnCollisionStay2DInternal(a);
                    }
                }
            }
        }

        // 衝突が終了したペアの処理
        foreach (var pair in _currentCollisions)
        {
            if (!newCollisions.Contains(pair))
            {
                pair.Item1.OnCollisionExit2DInternal(pair.Item2);
                pair.Item2.OnCollisionExit2DInternal(pair.Item1);
            }
        }

        _currentCollisions = newCollisions;
    }

    /// <summary>
    /// 2つのコライダー間の衝突判定
    /// </summary>
    private bool IsColliding(SimpleShapeCollider2D a, SimpleShapeCollider2D b)
    {
        // 円VS円
        if (a._collisionType == CollisionType2D.Circle && b._collisionType == CollisionType2D.Circle)
        {
            float distance = Vector2.Distance(a.transform.position, b.transform.position);
            return distance < a._size + b._size;
        }

        // 正方形VS正方形
        if (a._collisionType == CollisionType2D.Square && b._collisionType == CollisionType2D.Square)
        {
            return CheckPolygonCollision(a.GetSquareCorners(), b.GetSquareCorners());
        }

        // 円VS正方形
        if ((a._collisionType == CollisionType2D.Circle && b._collisionType == CollisionType2D.Square) ||
            (a._collisionType == CollisionType2D.Square && b._collisionType == CollisionType2D.Circle))
        {
            var circle = a._collisionType == CollisionType2D.Circle ? a : b;
            var square = a._collisionType == CollisionType2D.Square ? a : b;
            return CheckCirclePolygonCollision(circle, square.GetSquareCorners());
        }

        return false;
    }

    /// <summary>
    /// 多角形同士の衝突判定（分離軸判定法を使用）
    /// </summary>
    private bool CheckPolygonCollision(Vector2[] vertsA, Vector2[] vertsB)
    {
        // 多角形Aの各辺の法線ベクトルについて判定
        for (int i = 0; i < vertsA.Length; i++)
        {
            Vector2 edge = vertsA[(i + 1) % vertsA.Length] - vertsA[i];
            Vector2 normal = new Vector2(-edge.y, edge.x).normalized;

            if (HasSeparatingAxis(normal, vertsA, vertsB))
                return false;
        }

        // 多角形Bの各辺の法線ベクトルについて判定
        for (int i = 0; i < vertsB.Length; i++)
        {
            Vector2 edge = vertsB[(i + 1) % vertsB.Length] - vertsB[i];
            Vector2 normal = new Vector2(-edge.y, edge.x).normalized;

            if (HasSeparatingAxis(normal, vertsA, vertsB))
                return false;
        }

        return true;
    }

    /// <summary>
    /// 円と多角形の衝突判定
    /// </summary>
    private bool CheckCirclePolygonCollision(SimpleShapeCollider2D circle, Vector2[] polygonVerts)
    {
        Vector2 circlePos = circle.transform.position;
        float radiusSq = circle._size * circle._size;

        // 多角形の各辺と円の最近接点をチェック
        for (int i = 0; i < polygonVerts.Length; i++)
        {
            Vector2 start = polygonVerts[i];
            Vector2 end = polygonVerts[(i + 1) % polygonVerts.Length];

            float distSq = PointToLineSegmentDistanceSq(circlePos, start, end);
            if (distSq <= radiusSq)
                return true;
        }

        // 円の中心が多角形の内部にあるかチェック
        return IsPointInPolygon(circlePos, polygonVerts);
    }

    /// <summary>
    /// 分離軸判定法による衝突判定の補助メソッド
    /// </summary>
    private bool HasSeparatingAxis(Vector2 normal, Vector2[] vertsA, Vector2[] vertsB)
    {
        float minA = float.MaxValue, maxA = float.MinValue;
        float minB = float.MaxValue, maxB = float.MinValue;

        // 多角形Aの頂点を投影
        foreach (Vector2 v in vertsA)
        {
            float proj = Vector2.Dot(v, normal);
            minA = Mathf.Min(minA, proj);
            maxA = Mathf.Max(maxA, proj);
        }

        // 多角形Bの頂点を投影
        foreach (Vector2 v in vertsB)
        {
            float proj = Vector2.Dot(v, normal);
            minB = Mathf.Min(minB, proj);
            maxB = Mathf.Max(maxB, proj);
        }

        // 分離軸が存在するかチェック
        return maxA < minB || maxB < minA;
    }

    /// <summary>
    /// 点と線分間の最短距離の二乗を計算
    /// </summary>
    private float PointToLineSegmentDistanceSq(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
    {
        Vector2 line = lineEnd - lineStart;
        float lineLengthSq = line.sqrMagnitude;

        if (lineLengthSq == 0f)
            return (point - lineStart).sqrMagnitude;

        float t = Mathf.Clamp01(Vector2.Dot(point - lineStart, line) / lineLengthSq);
        Vector2 projection = lineStart + t * line;
        return (point - projection).sqrMagnitude;
    }

    /// <summary>
    /// 点が多角形の内部にあるかどうかを判定
    /// </summary>
    private bool IsPointInPolygon(Vector2 point, Vector2[] vertices)
    {
        bool inside = false;
        for (int i = 0, j = vertices.Length - 1; i < vertices.Length; j = i++)
        {
            if (((vertices[i].y > point.y) != (vertices[j].y > point.y)) &&
                (point.x < (vertices[j].x - vertices[i].x) * (point.y - vertices[i].y) /
                (vertices[j].y - vertices[i].y) + vertices[i].x))
            {
                inside = !inside;
            }
        }
        return inside;
    }
}