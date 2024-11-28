using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ICollisionEvents
{
    void OnCustomCollisionEnter(SimpleShapeCollider2D collision);
    void OnCustomCollisionStay(SimpleShapeCollider2D collision);
    void OnCustomCollisionExit(SimpleShapeCollider2D collision);
}
