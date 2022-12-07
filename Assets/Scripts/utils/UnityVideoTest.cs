using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// 单例模式
/// instance 指向目前存在的实例
/// </summary>
/// <typeparam name="T">需要单例的类名</typeparam>
public class SingletonMonoBehavior<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// 实际使用的 instance 
    /// </summary>
    private static T instance;

    /// <summary>
    /// 如果有 instance 就返回 instance 如果没有就选择第一个实例
    /// </summary>
    public static T Instance {
        get => instance ? instance : FindObjectOfType<T>();
        set => instance = value; 
    }

    /// <summary>
    /// 创建实例 
    /// 如果实例不为空 则等待实例为空再继续
    /// </summary>
    protected virtual async void Awake(){
        if (instance != null) {
            Debug.LogWarning("instance is not null , start waiting");
            await UniTask.WaitUntil(() => instance == null);
            Debug.Log("waiting over");
            if (this == null) return;
        }
        instance = this as T;
    }

    /// <summary>
    /// 销毁实例 释放instance
    /// </summary>
    protected virtual void OnDestroy() {
        instance = null;
    }

}
