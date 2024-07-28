using UnityEngine;

/// <summary>
/// 背包
/// </summary>
public class Backpack : MonoBehaviour
{
    protected void Start()
    {
        SingletonRegistry.Set(SR.BACKPACK, gameObject);
        gameObject.SetActive(false);
    }
}
