using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ICollisionEvents
{
    void OnCollisionEnter(SimpleShapeCollider2D collision);
    void OnCollisionStay(SimpleShapeCollider2D collision);
    void OnCollisionExit(SimpleShapeCollider2D collision);
}
