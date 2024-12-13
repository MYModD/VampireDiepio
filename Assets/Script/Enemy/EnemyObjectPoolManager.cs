using UnityEngine;


public class EnemyObjectPoolManager : BaseObjectPool<PoolableEnemy>
{
    protected override PoolableEnemy Create()
    {
        PoolableEnemy pooleableEnemy = base.Create();
        EnemyComponentsManager.Instance.RegisterComponents(pooleableEnemy.gameObject);
        return pooleableEnemy;

    }


    public GameObject GetEnemyObject()
    {
        return ObjectPool.Get().gameObject;
    }
}
