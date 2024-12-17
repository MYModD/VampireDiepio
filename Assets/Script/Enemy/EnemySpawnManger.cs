using UnityEngine;


public class EnemySpawnManger : MonoBehaviour
{
    private EnemyObjectPoolManager _enemyObjectPoolManger;

    public bool _isSpawn = false;

    [Range(0, 30f)]
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
        if (_isSpawn)
        {
            valueTime -= Time.deltaTime;
            if (valueTime <= 0f)
            {
                GameObject enemyObjct = _enemyObjectPoolManger.GetEnemyObject();

                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                float randomDistance = Random.Range(_spawnRange, _spawnRange * 2);
                Vector2 spawnPosition = (Vector2)_playerTransform.position + randomDirection * randomDistance;
                enemyObjct.transform.position = spawnPosition;
                enemyObjct.GetComponent<EnemyMove>().ChengeTarget(_playerTransform);
                valueTime = _spawnDulation;
            }
        }
    }
}
