using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コンポーネント管理の基底クラス
/// </summary>
/// <typeparam name="T">継承先がコンポーネント</typeparam>
/// <typeparam name="TComponents">継承先で使う方が構造体</typeparam>
public abstract class BaseComponentsManager<T, TComponents> : Diepio.Singleton<T>
    where T : Component
    where TComponents : struct 
{
    // GameObject の InstanceID をキーとしたコンポーネント情報の辞書
    protected Dictionary<int, TComponents> _components = new Dictionary<int, TComponents>();

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// オブジェクトとそのコンポーネント情報を登録
    /// </summary>
    public virtual void RegisterComponents(GameObject obj)
    {
        int instanceId = obj.GetInstanceID();

        // 既に登録済みの場合はスキップ
        if (_components.ContainsKey(instanceId))
        {
            Debug.LogError($"ID: {instanceId} のオブジェクトは既に登録されているよ!!");
            return;
        }

        // 継承先で作らなければならないリターンが構造体
        TComponents components = CreateComponents(obj);
        _components[instanceId] = components;
    }

    /// <summary>
    /// 指定したオブジェクトのコンポーネント情報を取得
    /// </summary>
    public virtual TComponents GetComponents(GameObject obj)
    {
        int instanceId = obj.GetInstanceID();

        // TryGetValueで捜査を一回にすることで効率化、なかった場合エラーを出す
        if (!_components.TryGetValue(instanceId,out TComponents components))
        {
            Debug.LogError($"ID: {instanceId} のオブジェクトのコンポーネントがみつからないよ!!");            
        }

        return components;
    }

   

    /// <summary>
    /// Dictionaryから削除 すべてオブジェクトプールのため使わない
    /// </summary>
    public virtual void Unregister(GameObject obj)
    {
        int instanceId = obj.GetInstanceID();
        _components.Remove(instanceId);
    }

    /// <summary>
    /// コンポーネント情報を作成するメソッド
    /// 継承先でoverrideしないとコンパイルエラーを起こす
    /// </summary>
    protected abstract TComponents CreateComponents(GameObject obj);

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _components.Clear();
    }
}