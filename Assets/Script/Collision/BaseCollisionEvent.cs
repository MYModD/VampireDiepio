using UnityEngine;

// ���ƂȂ钊�ۃN���X
public abstract class BaseCollisionEvent : MonoBehaviour,ICollisionEvents
{
    protected private SimpleShapeCollider2D _shapeCollider;

    protected virtual void Awake()
    {
        _shapeCollider = GetComponent<SimpleShapeCollider2D>();
        if (_shapeCollider == null)
        {
            Debug.LogError($"SimpleShapeCollider2D��{gameObject.name}�Ɍ�����܂���");
            return;
        }
        RegisterEvents();
    }

    protected virtual void OnEnable()
    {
        if(_shapeCollider == null)
        {
            // �ēx�x��
            _shapeCollider = GetComponent<SimpleShapeCollider2D>();
            Debug.LogError($"SimpleShapeCollider2D��{gameObject.name}�Ɍ�����܂���");

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

    // �C���^�[�t�F�[�X�Œ�`���ꂽ���\�b�h�� abstract �Ő錾
    public abstract void OnCustomCollisionEnter(SimpleShapeCollider2D collision);
    public abstract void OnCustomCollisionStay(SimpleShapeCollider2D collision);
    public abstract void OnCustomCollisionExit(SimpleShapeCollider2D collision);

   
}

