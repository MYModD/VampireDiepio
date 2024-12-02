using Diepio;
using System.Collections.Generic;
using UnityEngine;


public struct DebrisComponents
{
    public DebrisHP debrisHP;
    public DebrisMove debrisMove;
}

public class DebrisComponentManager : Singleton<DebrisComponentManager>
{
    // シングルトンがMonobehavior継承しているので重い可能性がある


    private static Dictionary<int, DebrisComponents> _debrisComponents = new Dictionary<int, DebrisComponents>();

    public void RegisterDebris(GameObject debris)
    {
        int instanceId = debris.GetInstanceID();

        // すでに登録されている場合はスキップ
        if (_debrisComponents.ContainsKey(instanceId))
        {
            Debug.LogError("すでに登録されています");
            return;
        }

        DebrisComponents components = new DebrisComponents
        {
            debrisMove = debris.GetComponent<DebrisMove>(),
            debrisHP = debris.GetComponent<DebrisHP>()
        };

        // 上書きする場合もある
        _debrisComponents[instanceId] = components;



    }


    /// <summary>
    /// Debug用
    /// </summary>
    public void DebugDictionary()
    {
        foreach (var item in _debrisComponents)
        {
            Debug.Log($"{item.Key} : {item.Value} :{item.Value.debrisHP} : {item.Value.debrisMove}".Warning());
        }
    }


    public DebrisComponents GetDebrisComponents(GameObject debris)
    {
        return _debrisComponents[debris.GetInstanceID()];
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _debrisComponents.Clear();
    }

}
