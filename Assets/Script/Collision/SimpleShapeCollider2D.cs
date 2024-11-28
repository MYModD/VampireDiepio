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
        CollisionManager2D.Instance.UnregisterCollider(this);
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
    /// ��]�����l�p�`��Gizmos�ł̕`��
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
    /// �����`�̒��_���W���擾
    /// �I�u�W�F�N�g�̉�]���l�����Čv�Z
    /// </summary>
    public Vector2[] GetSquareCorners()
    {
        Vector2[] corners = new Vector2[4];
        // �I�u�W�F�N�g��Z����]�p�x�����W�A���ɕϊ�
        float rotationRad = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 pos = transform.position;

        for (int i = 0; i < 4; i++)
        {
            // �e���_�̊p�x���v�Z�i�I�u�W�F�N�g�̉�]�p�x + 90�x���Ƃ̊p�x�j
            float angle = rotationRad + (i * 90f * Mathf.Deg2Rad);

            // ��]�s����g�p���Ē��_�̈ʒu���v�Z
            corners[i] = pos + new Vector2(
                Mathf.Cos(angle) * _size,  // X���W
                Mathf.Sin(angle) * _size   // Y���W
            );
        }

        return corners;
    }
}