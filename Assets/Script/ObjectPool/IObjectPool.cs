using UnityEngine.Pool;

public interface IPooledObject<T> where T : class
{
    public IPooledObject<T> ObjectPool { set; }
    public void Initialize();
    public void ReturnToPool();
}

