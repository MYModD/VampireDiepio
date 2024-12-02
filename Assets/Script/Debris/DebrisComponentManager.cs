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

    public static void RegisterDebris(GameObject debris)
    {
        var instanceId = debris.GetInstanceID();

        // ���łɓo�^����Ă���ꍇ�̓X�L�b�v
        if (_debrisComponents.ContainsKey(instanceId)) return;

        var components = new DebrisComponents
        {
            debrisMove = debris.GetComponent<DebrisMove>(),
            debrisHP = debris.GetComponent<DebrisHP>()
        };

        // �㏑������ꍇ������
        _debrisComponents[instanceId] = components;
    }



    public static DebrisComponents GetDebrisComponents(GameObject debris)
    {
        return _debrisComponents[debris.GetInstanceID()];
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _debrisComponents.Clear();
    }

}
