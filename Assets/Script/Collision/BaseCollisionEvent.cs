using UnityEngine;

// 基底となる抽象クラス
public abstract class BaseCollisionEvent : MonoBehaviour,ICollisionEvents
{
    protected private SimpleShapeCollider2D _shapeCollider;

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

    protected virtual void OnEnable()
    {
        if(_shapeCollider == null)
        {
            // 再度警告
            _shapeCollider = GetComponent<SimpleShapeCollider2D>();
            Debug.LogError($"SimpleShapeCollider2Dが{gameObject.name}に見つかりません");

        }


    }

    protected virtual void OnDisable()
    {
        //UnregisterEvents();
    }


    private void RegisterEvents()
    {
        _shapeCollider.OnCollisionEnter2D += OnCustomCollisionEnter;
        _shapeCollider.OnCollisionStay2D += OnCustomCollisionStay;
        _shapeCollider.OnCollisionExit2D += OnCustomCollisionExit;
    }

    private void UnregisterEvents()
    {
        if (_shapeCollider != null)
        {
            _shapeCollider.OnCollisionEnter2D -= OnCustomCollisionEnter;
            _shapeCollider.OnCollisionStay2D -= OnCustomCollisionStay;
            _shapeCollider.OnCollisionExit2D -= OnCustomCollisionExit;
        }
    }

    // インターフェースで定義されたメソッドを abstract で宣言
    public abstract void OnCustomCollisionEnter(SimpleShapeCollider2D collision);
    public abstract void OnCustomCollisionStay(SimpleShapeCollider2D collision);
    public abstract void OnCustomCollisionExit(SimpleShapeCollider2D collision);

   
}

