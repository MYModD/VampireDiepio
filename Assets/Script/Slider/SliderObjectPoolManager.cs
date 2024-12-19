using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;


public class SliderObjectPoolManager : BaseObjectPool<PoolableSlider>
{
    [Header("�e�I�u�W�F�N�g�I��")]
    [SerializeField] private Transform _pearentObjct;

    // HP�o�[��o�^����Hashset,������key�̓C���X�^���XID
    private HashSet<int> activeInstanceIds = new();


    private static SliderObjectPoolManager instance ;

    public static SliderObjectPoolManager Instance
    {
        get
        {           
            return instance;
        }
    }


    public void HPBarRental(GameObject gameObject)
    {
        PoolableSlider poolableSlider = null;

        if (IsRegistered(gameObject.GetInstanceID())) {

            // �A������trygetValue�Ŏ擾

        }
        else
        {
            poolableSlider = ObjectPool.Get();
            Register(gameObject);
        }
    }




    protected override PoolableSlider Create()
    {
        PoolableSlider slider = base.Create();
        slider.transform.SetParent(_pearentObjct);
        return slider;
    }




    /// <summary>
    /// HP�o�[���o�^����Ă��邩
    /// </summary>
    /// <param name="id">�C���X�^���XID</param>
    /// <returns>�o�^���Ă��邩</returns>
    public bool IsRegistered(int id)
    {
        return activeInstanceIds.Contains(id);
    }

    /// <summary>
    /// �����^�����ɓo�^
    /// </summary>
    /// <param name="gameObject"></param>
    public void Register(GameObject gameObject)
    {
        activeInstanceIds.Add(gameObject.GetInstanceID());
    }

    public void Unregister(GameObject gameObject)
    {
        activeInstanceIds.Remove(gameObject.GetInstanceID());
    }


    /// <summary>
    /// �S�Ă�HP�o�[���폜
    /// </summary>

    public void ClearALL()
    {
        activeInstanceIds.Clear();
    }

    private void OnDestroy()
    {
        ClearALL();
    }


}
