using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

public class EnemyJobController : MonoBehaviour
{
    public GameObject enemyPrefab; // �G�̃v���n�u
    public Transform player; // �v���C���[�̈ʒu
    public int enemyCount = 1000; // �G�̐�
    public float speed = 5f; // �ړ����x

    private GameObject[] enemies;
    private NativeArray<Vector3> enemyPositions;
    private NativeArray<Vector3> directions;

    void Start()
    {
        // �G�I�u�W�F�N�g�̐���
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
        // �G�̈ʒu��NativeArray�ɃR�s�[
        for (int i = 0; i < enemyCount; i++)
        {
            enemyPositions[i] = enemies[i].transform.position;
            directions[i] = (player.position - enemyPositions[i]).normalized;
        }

        // Job�̍쐬�ƃX�P�W���[�����O
        EnemyMovementJob movementJob = new EnemyMovementJob
        {
            positions = enemyPositions,
            directions = directions,
            speed = speed,
            deltaTime = Time.deltaTime
        };

        JobHandle jobHandle = movementJob.Schedule(enemyCount, 64);
        jobHandle.Complete(); // Job�̊�����҂�

        // �v�Z���ʂ��I�u�W�F�N�g�ɔ��f
        for (int i = 0; i < enemyCount; i++)
        {
            enemies[i].transform.position = enemyPositions[i];
        }
    }

    void OnDestroy()
    {
        // �������̉��
        enemyPositions.Dispose();
        directions.Dispose();
    }

    // �W���u�\���̂̒�`
    public struct EnemyMovementJob : IJobParallelFor
    {
        public NativeArray<Vector3> positions;
        public NativeArray<Vector3> directions;
        public float speed;
        public float deltaTime;

        public void Execute(int index)
        {
            // �ʒu�̍X�V
            positions[index] += directions[index] * speed * deltaTime;
        }
    }
}
