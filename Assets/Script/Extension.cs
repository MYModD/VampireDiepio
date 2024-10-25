using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    public static void LookAt2D(this Transform transform, Vector3 target)
    {
        Vector3 direction = target - transform.position;
        

    }

   

}
