using UnityEngine;

// 基底となる抽象クラス
public abstract class CollisionEventBase : MonoBehaviour,ICollisionEvents
{
    protected SimpleShapeCollider2D _shapeCollider;

    protected virtual void Awake()
    {
        _shapeCollider = GetComponent<SimpleShapeCollider2D>();
        if (_shapeCollider == null)
        {
            Debug.LogError($"SimpleShapeCollider2Dが{gameObject.name}に見つかりません");
            return;
        }

        RegisterEvents();
    }

    protected virtual void OnDestroy()
    {
        UnregisterEvents();
    }

    private void RegisterEvents()
    {
        _shapeCollider.OnCollisionEnter2D += OnCollisionEnter;
        _shapeCollider.OnCollisionStay2D += OnCollisionStay;
        _shapeCollider.OnCollisionExit2D += OnCollisionExit;
    }

    private void UnregisterEvents()
    {
        if (_shapeCollider != null)
        {
            _shapeCollider.OnCollisionEnter2D -= OnCollisionEnter;
            _shapeCollider.OnCollisionStay2D -= OnCollisionStay;
            _shapeCollider.OnCollisionExit2D -= OnCollisionExit;
        }
    }

    // インターフェースで定義されたメソッドを abstract で宣言
    public abstract void OnCollisionEnter(SimpleShapeCollider2D collision);
    public abstract void OnCollisionStay(SimpleShapeCollider2D collision);
    public abstract void OnCollisionExit(SimpleShapeCollider2D collision);

    protected bool IsCollidingWithTag(SimpleShapeCollider2D collision, string tag)
    {
        return collision.gameObject.CompareTag(tag);
    }
}

// 使用例：プレイヤーの衝突処理
