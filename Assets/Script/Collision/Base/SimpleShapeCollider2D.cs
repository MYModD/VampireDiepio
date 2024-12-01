using System;
using UnityEngine;

/// <summary>
/// �Փ˔���̎�ނ��`
/// </summary>
public enum CollisionType2D
{
    Circle,
    Square
}

/// <summary>
/// �J�X�^���R���C�_�[�R���|�[�l���g
/// </summary>
public class SimpleShapeCollider2D : MonoBehaviour
{
    public CollisionType2D _collisionType;
    public float _size = 1f;

    // �R���C�_�[�̃C�x���g���`
    public event Action<SimpleShapeCollider2D> OnCollisionEnter2D;
    public event Action<SimpleShapeCollider2D> OnCollisionStay2D;
    public event Action<SimpleShapeCollider2D> OnCollisionExit2D;

    private void OnEnable()
    {
        CollisionManager2D.Instance.RegisterCollider(this);
    }

    private void OnDisable()
    {
        if (CollisionManager2D.Instance != null)  // enabled�`�F�b�N��ǉ�
        {
            CollisionManager2D.Instance.UnregisterCollider(this);
        }
    }

    /// <summary>
    /// ���̃R���C�_�[�Ƃ̏ՓˊJ�n���̓�������
    /// </summary>
    internal void OnCollisionEnter2DInternal(SimpleShapeCollider2D other)
    {
        OnCollisionEnter2D?.Invoke(other);
    }

    /// <summary>
    /// ���̃R���C�_�[�Ƃ̏Փˌp�����̓�������
    /// </summary>
    internal void OnCollisionStay2DInternal(SimpleShapeCollider2D other)
    {
        OnCollisionStay2D?.Invoke(other);
    }

    /// <summary>
    /// ���̃R���C�_�[�Ƃ̏ՓˏI�����̓�������
    /// </summary>
    internal void OnCollisionExit2DInternal(SimpleShapeCollider2D other)
    {
        OnCollisionExit2D?.Invoke(other);
    }

    /// <summary>
    /// �V�[���r���[�ł̃R���C�_�[�̉���
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

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
    /// ��]�����l�p�`��Gizmos�ł̕`��
    /// </summary>
    private void DrawRotatedRect()
    {
        Vector2[] corners = GetSquareCorners();
        for (int i = 0; i < 4; i++)
        {

            //���ꂷ��
            Gizmos.DrawLine(corners[i], corners[(i + 1) % 4]);
        }
    }

    /// <summary>
    /// �����`�̒��_���W���擾
    /// �I�u�W�F�N�g�̉�]���l�����Čv�Z
    /// </summary>
    public Vector2[] GetSquareCorners()
    {
        Vector2[] corners = new Vector2[4];
        Vector2 pos = transform.position;
        float halfSize = _size;

        // ��]���l�������A�����Ȏl�p�`�̒��_���v�Z
        corners[0] = pos + new Vector2(-halfSize, halfSize);   // ����
        corners[1] = pos + new Vector2(halfSize, halfSize);    // �E��
        corners[2] = pos + new Vector2(halfSize, -halfSize);   // �E��
        corners[3] = pos + new Vector2(-halfSize, -halfSize);  // ����

        return corners;
    }
}