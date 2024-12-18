using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _attackPower = 10;


    public void AttackToPlayer(PlayerHP playerHP)
    {
        playerHP.DegreeHP(_attackPower);
    }

}
