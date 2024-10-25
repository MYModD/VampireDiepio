using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefalutEnemyControll : MonoBehaviour
{
    public Transform player; // プレイヤーの位置
    public float speed = 5f; // 敵の移動速度

    void Update()
    {
        // 毎フレーム、プレイヤーに向かって移動
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}
