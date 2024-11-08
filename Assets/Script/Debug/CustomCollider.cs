using UnityEngine;
using System;

public abstract class CustomCollider : MonoBehaviour
{
    // �V���A���C�Y�\�Ȑ}�`�̃f�[�^
    [SerializeField] protected Vector2 _position;
    [SerializeField] protected float _radius;     // �~�`�p
    [SerializeField] protected Vector2 _size;     // �l�p�`�p
    [SerializeField] protected Vector2[] _vertices; // �O�p�`�̒��_

    // �Փ˃C�x���g
    public event Action<CustomCollider> OnCollision;

    private void Update()
    {
        _position = transform.position;
    }

    // �Փ˔��胁�\�b�h�i�}�`���ƂɃI�[�o�[���C�h����j
    public abstract bool CheckCollision(CustomCollider other);

    // �Փ˂����������Ƃ��ɌĂяo����郁�\�b�h
    protected void TriggerCollision(CustomCollider other)
    {
        OnCollision?.Invoke(other);
    }
}
