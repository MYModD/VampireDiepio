using UnityEngine;

// ���ƂȂ钊�ۃN���X
public abstract class CollisionEventBase : MonoBehaviour,ICollisionEvents
{
    protected SimpleShapeCollider2D _shapeCollider;

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

    // �C���^�[�t�F�[�X�Œ�`���ꂽ���\�b�h�� abstract �Ő錾
    public abstract void OnCollisionEnter(SimpleShapeCollider2D collision);
    public abstract void OnCollisionStay(SimpleShapeCollider2D collision);
    public abstract void OnCollisionExit(SimpleShapeCollider2D collision);

    protected bool IsCollidingWithTag(SimpleShapeCollider2D collision, string tag)
    {
        return collision.gameObject.CompareTag(tag);
    }
}

// �g�p��F�v���C���[�̏Փˏ���
