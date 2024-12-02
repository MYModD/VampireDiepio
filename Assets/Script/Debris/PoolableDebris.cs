using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolableDebris : MonoBehaviour, IPooledObject<PoolableDebris>
{
    public IPooledObject<PoolableDebris> ObjectPool { get; set; }


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
