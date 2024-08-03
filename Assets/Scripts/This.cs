using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例注册表
/// 
/// <para>实体通信的交换机。</para>
/// </summary>
public static class This
{
    static readonly Dictionary<Context, GameObject> data = new();

    public static GameObject Get(Context item)
    {
        return data[item];
    }

    public static T Get<T>(Context item) where T : MonoBehaviour
    {
        return data[item].GetComponent<T>();
    }

    public static void Set(Context item, GameObject data)
    {
        This.data[item] = data;
    }
}

public enum Context
{
    /// <summary>
    /// 本地角色
    /// </summary>
    AVATAR,

    /// <summary>
    /// 本地角色摄像机
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

    /// <summary>
    /// 全局通知组件
    /// </summary>
    GLOBAL_NOTICE,

    /// <summary>
    /// 二级全局通知组件
    /// </summary>
    GLOBAL_NOTICE_2,

    /// <summary>
    /// 渐变器
    /// </summary>
    FADER,
}
