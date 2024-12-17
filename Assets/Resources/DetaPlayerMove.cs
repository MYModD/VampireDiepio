using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DetaPlayerMove", menuName = "Diepio/Player/DetaPlayerMove")]

public class DetaPlayerMove : ScriptableObject
{

    public float InputAcceleration;
    public float InputDeceleration;
    public float InputMaxSpeed;
    public float RecoilForceMultiplier;
    public float RecoilDeceleration;
    public float RecoilMaxSpeed;
    public float VelocityMaxSpeed;
    public float DebrisDeceleration;
    public float EnemyDeceleration;
}
