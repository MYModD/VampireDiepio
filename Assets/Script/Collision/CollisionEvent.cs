using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionEvent : MonoBehaviour
{
    private SimpleShapeCollider2D _simpleCollider;
    void Awake()
    {
        _simpleCollider = GetComponent<CustomCollider2D>();
        _simpleCollider.OnCollisionEnter2D += OnCollisionEnter2DCustom;
        _simpleCollider.OnCollisionStay2D += OnCollisionStay2DCustom;
        _simpleCollider.OnCollisionExit2D += OnCollisionExit2DCustom;
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter2DCustom(CustomCollider2D collision)
    {
        
        //Debug.Log($"{collision.gameObject.name}����������");
    }
    private void OnCollisionStay2DCustom(CustomCollider2D collision)
    {
        //Debug.Log($"{collision.gameObject.name}���}����");

    }
    private void OnCollisionExit2DCustom(CustomCollider2D collision)
    {
        //Debug.Log($"{collision.gameObject.name}���o����");

    }
}
