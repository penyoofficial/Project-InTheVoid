using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例注册表
/// 
/// <para>IPC 的中央枢纽。</para>
/// </summary>
public class SingletonRegistry
{
    static readonly Dictionary<SR, GameObject> data = new();

    public static GameObject Get(SR item)
    {
        return data[item];
    }

    public static void Set(SR item, GameObject data)
    {
        SingletonRegistry.data[item] = data;
    }
}

public enum SR
{
    /// <summary>
    /// 本地玩家
    /// </summary>
    AVATAR,

    /// <summary>
    /// 本地玩家摄像机
    /// </summary>
    AVATAR_CAMERA,

    /// <summary>
    /// 世界（主要掌管生物生成和判定二段跳）
    /// </summary>
    WORLD,

    /// <summary>
    /// 背包
    /// </summary>
    BACKPACK,

    /// <summary>
    /// 商店
    /// </summary>
    STORE,

    /// <summary>
    /// 商店塑像
    /// </summary>
    STORE_STATUE,
}
