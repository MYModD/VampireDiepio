using Diepio;
using System.Collections.Generic;
using UnityEngine;


public struct DebrisComponents
{
    public DebrisHP debrisHP;
    public DebrisMove debrisMove;
}

public class DebrisComponentManager : BaseComponentManager<DebrisComponentManager, DebrisComponents>
{
   

    protected override DebrisComponents CreateComponents(GameObject obj)
    {
        DebrisComponents components = new DebrisComponents
        {
            debrisHP = obj.GetComponent<DebrisHP>(),
            debrisMove = obj.GetComponent<DebrisMove>()
        };
        return components;
    }
}
