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
    // �V���O���g����Monobehavior�p�����Ă���̂ŏd���\��������


    private static Dictionary<int, DebrisComponents> _debrisComponents = new Dictionary<int, DebrisComponents>();

    public void RegisterDebris(GameObject debris)
    {
        int instanceId = debris.GetInstanceID();

        // ���łɓo�^����Ă���ꍇ�̓X�L�b�v
        if (_debrisComponents.ContainsKey(instanceId))
        {
            Debug.LogError("���łɓo�^����Ă��܂�");
            return;
        }

        DebrisComponents components = new DebrisComponents
        {
            debrisMove = debris.GetComponent<DebrisMove>(),
            debrisHP = debris.GetComponent<DebrisHP>()
        };

        // �㏑������ꍇ������
        _debrisComponents[instanceId] = components;



    }


    /// <summary>
    /// Debug�p
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
