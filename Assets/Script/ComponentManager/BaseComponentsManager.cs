using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �R���|�[�l���g�Ǘ��̊��N���X
/// </summary>
/// <typeparam name="T">�p���悪�R���|�[�l���g</typeparam>
/// <typeparam name="TComponents">�p����Ŏg�������\����</typeparam>
public abstract class BaseComponentsManager<T, TComponents> : Diepio.Singleton<T>
    where T : Component
    where TComponents : struct 
{
    // GameObject �� InstanceID ���L�[�Ƃ����R���|�[�l���g���̎���
    protected Dictionary<int, TComponents> _components = new Dictionary<int, TComponents>();

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// �I�u�W�F�N�g�Ƃ��̃R���|�[�l���g����o�^
    /// </summary>
    public virtual void RegisterComponents(GameObject obj)
    {
        int instanceId = obj.GetInstanceID();

        // ���ɓo�^�ς݂̏ꍇ�̓X�L�b�v
        if (_components.ContainsKey(instanceId))
        {
            Debug.LogError($"ID: {instanceId} �̃I�u�W�F�N�g�͊��ɓo�^����Ă����!!");
            return;
        }

        // �p����ō��Ȃ���΂Ȃ�Ȃ����^�[�����\����
        TComponents components = CreateComponents(obj);
        _components[instanceId] = components;
    }

    /// <summary>
    /// �w�肵���I�u�W�F�N�g�̃R���|�[�l���g�����擾
    /// </summary>
    public virtual TComponents GetComponents(GameObject obj)
    {
        int instanceId = obj.GetInstanceID();

        // TryGetValue�ő{�������ɂ��邱�ƂŌ������A�Ȃ������ꍇ�G���[���o��
        if (!_components.TryGetValue(instanceId,out TComponents components))
        {
            Debug.LogError($"ID: {instanceId} �̃I�u�W�F�N�g�̃R���|�[�l���g���݂���Ȃ���!!");            
        }

        return components;
    }

   

    /// <summary>
    /// Dictionary����폜 ���ׂăI�u�W�F�N�g�v�[���̂��ߎg��Ȃ�
    /// </summary>
    public virtual void Unregister(GameObject obj)
    {
        int instanceId = obj.GetInstanceID();
        _components.Remove(instanceId);
    }

    /// <summary>
    /// �R���|�[�l���g�����쐬���郁�\�b�h
    /// �p�����override���Ȃ��ƃR���p�C���G���[���N����
    /// </summary>
    protected abstract TComponents CreateComponents(GameObject obj);

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _components.Clear();
    }
}