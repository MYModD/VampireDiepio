using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class PoolableDebris : MonoBehaviour, IPooledObject<PoolableDebris>
{
    public IObjectPool<PoolableDebris> ObjectPool { get; set; }


    /// <summary>
    /// ‰Šú‰»ˆ—
    /// </summary>
    public void Initialize()
    {
        
    }


    /// <summary>
    /// ƒv[ƒ‹‚É•Ô‹p‚·‚éˆ—
    /// </summary>
    public void ReturnToPool()
    {
        
    }
}
