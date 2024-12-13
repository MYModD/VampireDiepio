using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawnManger : MonoBehaviour
{
    private EnemyObjectPoolManager _enemyObjectPoolManger;

    public bool _isSpawn = false;

    [Range(0,100f)]
    public float _spawnRange = 5f;

    [Range(0, 5f)]
    public float _spawnDulation = 1f;

    public Transform _playerTransform = default;



    private float valueTime = default;

    
    private void Awake()
    {
        _enemyObjectPoolManger = GetComponent<EnemyObjectPoolManager>();   
    }

    
    private void Update()
    {
        if(_isSpawn)
        {
            valueTime -= Time.deltaTime;
            if (valueTime <= 0f)
            {
                var hoge = _enemyObjectPoolManger.GetEnemyObject();
                float randomPostionX = Random.insideUnitCircle.x * 5f + _playerTransform.position.x;
                float randomPostionY = Random.insideUnitCircle.y * 5f + _playerTransform.position.y;
                Vector2 randomPostion = new Vector2(randomPostionX, randomPostionY);
                hoge.transform.position = randomPostion;
                hoge.GetComponent<EnemyMove>().ChengeTarget(_playerTransform);
                valueTime = _spawnDulation;
            }
        }


    }
}
