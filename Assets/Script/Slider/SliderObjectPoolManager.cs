using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;


public class SliderObjectPoolManager : BaseObjectPool<PoolableSlider>
{
    [Header("親オブジェクト選択")]
    [SerializeField] private Transform _pearentObjct;

    // HPバーを登録するHashset,実質なkeyはインスタンスID
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

            // 帰ったらtrygetValueで取得

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
    /// HPバーが登録されているか
    /// </summary>
    /// <param name="id">インスタンスID</param>
    /// <returns>登録しているか</returns>
    public bool IsRegistered(int id)
    {
        return activeInstanceIds.Contains(id);
    }

    /// <summary>
    /// レンタル時に登録
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
    /// 全てのHPバーを削除
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
