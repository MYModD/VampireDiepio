using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float _timer = 5f;

    private float _timerValue = default;
    // ���̂̎���
    private float _mass;

    // ���݂̑��x
    private Vector3 _velocity;

    // �ʒu
    private Vector3 _position;

    //

    // ���݂̈ʒu���擾
    public Vector3 Position => _position;

    // ���݂̑��x���擾�E�ݒ�
    public Vector3 Velocity
    {
        get => _velocity;
        set => _velocity = value;
    }

    // �͂������郁�\�b�h
    public void AddForce(Vector3 force)
    {
        // �����x���v�Z (F = ma)
        Vector3 acceleration = force / _mass;

        // ���݂̑��x�ɉ����x��������
        _velocity += acceleration * Time.deltaTime; // ��t�Ɋ�Â��đ��x���X�V

        // �ʒu���X�V
        _position += _velocity * Time.deltaTime; // �V�����ʒu���v�Z
    }

    // ���̂��X�V���郁�\�b�h
    public void Update()
    {
        // �����ł͕����I�ȍX�V���s���i�ʒu�𑬓x�Ɋ�Â��čX�V����Ȃǁj
        _position += _velocity * Time.deltaTime;
    }



    private void OnEnable()
    {
        _timerValue = _timer;
    }

    
}
