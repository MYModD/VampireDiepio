using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct EnemyComponents
{
    public EnemyMove enemyMove;
    public EnemyHP enemyHealth;
}

public class EnemyComponentsManager : BaseComponentsManager<EnemyComponentsManager, EnemyComponents>
{
    protected override EnemyComponents CreateComponents(GameObject obj)
    {
        EnemyComponents components = new EnemyComponents();
        components.enemyMove = obj.GetComponent<EnemyMove>();
        components.enemyHealth = obj.GetComponent<EnemyHP>();
        return components;
    }
}
