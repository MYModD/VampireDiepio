using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

public class EnemyJobController : MonoBehaviour
{
    public GameObject enemyPrefab; // 敵のプレハブ
    public Transform player; // プレイヤーの位置
    public int enemyCount = 1000; // 敵の数
    public float speed = 5f; // 移動速度

    private GameObject[] enemies;
    private NativeArray<Vector3> enemyPositions;
    private NativeArray<Vector3> directions;

    void Start()
    {
        // 敵オブジェクトの生成
        enemies = new GameObject[enemyCount];
        enemyPositions = new NativeArray<Vector3>(enemyCount, Allocator.Persistent);
        directions = new NativeArray<Vector3>(enemyCount, Allocator.Persistent);

        for (int i = 0; i < enemyCount; i++)
        {
            enemies[i] = Instantiate(enemyPrefab, Random.insideUnitSphere * 10f, Quaternion.identity);
        }
    }

    void Update()
    {
        // 敵の位置をNativeArrayにコピー
        for (int i = 0; i < enemyCount; i++)
        {
            enemyPositions[i] = enemies[i].transform.position;
            directions[i] = (player.position - enemyPositions[i]).normalized;
        }

        // Jobの作成とスケジューリング
        EnemyMovementJob movementJob = new EnemyMovementJob
        {
            positions = enemyPositions,
            directions = directions,
            speed = speed,
            deltaTime = Time.deltaTime
        };

        JobHandle jobHandle = movementJob.Schedule(enemyCount, 64);
        jobHandle.Complete(); // Jobの完了を待つ

        // 計算結果をオブジェクトに反映
        for (int i = 0; i < enemyCount; i++)
        {
            enemies[i].transform.position = enemyPositions[i];
        }
    }

    void OnDestroy()
    {
        // メモリの解放
        enemyPositions.Dispose();
        directions.Dispose();
    }

    // ジョブ構造体の定義
    public struct EnemyMovementJob : IJobParallelFor
    {
        public NativeArray<Vector3> positions;
        public NativeArray<Vector3> directions;
        public float speed;
        public float deltaTime;

        public void Execute(int index)
        {
            // 位置の更新
            positions[index] += directions[index] * speed * deltaTime;
        }
    }
}
