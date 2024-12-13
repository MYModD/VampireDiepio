using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _targetTransform;



    private void Update()
    {
        Vector3 vectol = _targetTransform.position - this.transform.position;
        vectol.Normalize();
        this.transform.position += vectol * _speed * Time.deltaTime;
    }

    public void ChengeTarget(Transform target)
    {
        _targetTransform = target;
    }
}
