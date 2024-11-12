using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionEvent : MonoBehaviour
{
    private CustomCollider2D _customCollider2D;
    void Awake()
    {
        _customCollider2D = GetComponent<CustomCollider2D>();
        _customCollider2D.OnCollisionEnter2D += Enter;
        _customCollider2D.OnCollisionStay2D += Stay;
        _customCollider2D.OnCollisionExit2D += Exit;
    }

    
    void Update()
    {
        
    }

    private void Enter(CustomCollider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && this.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
        //Debug.Log($"{collision.gameObject.name}Ç™ì¸Ç¡ÇΩÇÊ");
    }
    private void Stay(CustomCollider2D collision)
    {
        //Debug.Log($"{collision.gameObject.name}Ç™ë}ì¸íÜ");

    }
    private void Exit(CustomCollider2D collision)
    {
        //Debug.Log($"{collision.gameObject.name}Ç™èoÇΩÇÊ");

    }
}
