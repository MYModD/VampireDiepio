using System.Collections.Generic;
using UnityEngine;

public struct BulletComponents
{
    public BulletMove bulletMove;
}

public class BulletComponentManager : ComponentManagerBase<BulletComponentManager, BulletComponents>
{
    private static Dictionary<int, BulletComponents> _bulletComponents = new Dictionary<int, BulletComponents>();


    protected override BulletComponents CreateComponents(GameObject obj)
    {
        BulletComponents components = new BulletComponents();
        components.bulletMove = obj.GetComponent<BulletMove>();
        return components;
    }
}
