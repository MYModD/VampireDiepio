using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolableDebris : MonoBehaviour, IPooledObject<PoolableDebris>
{
    public IPooledObject<PoolableDebris> ObjectPool { get; set; }


    /// <summary>
    /// ����������
    /// </summary>
    public void Initialize()
    {
        
    }


    /// <summary>
    /// �v�[���ɕԋp���鏈��
    /// </summary>
    public void ReturnToPool()
    {
        
    }
}
