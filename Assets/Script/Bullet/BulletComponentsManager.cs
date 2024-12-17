using System.Collections.Generic;
using UnityEngine;

public struct BulletComponents
{
    public BulletMove bulletMove;
    public BulletCollisionEvent bulletCollisionEvent;
}

public class BulletComponentsManager : BaseComponentsManager<BulletComponentsManager, BulletComponents>
{
    protected override BulletComponents CreateComponents(GameObject obj)
    {
        BulletComponents components = new BulletComponents();
        components.bulletMove = obj.GetComponent<BulletMove>();
        components.bulletCollisionEvent = obj.GetComponent<BulletCollisionEvent>();
        return components;
    }
}
