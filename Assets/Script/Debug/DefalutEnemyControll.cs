using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefalutEnemyControll : MonoBehaviour
{
    public Transform player; // �v���C���[�̈ʒu
    public float speed = 5f; // �G�̈ړ����x

    void Update()
    {
        // ���t���[���A�v���C���[�Ɍ������Ĉړ�
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}
