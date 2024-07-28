using UnityEngine;

/// <summary>
/// 商店
/// </summary>
public class Store : MonoBehaviour
{
    protected void Start()
    {
        SingletonRegistry.Set(SR.STORE, gameObject);
        gameObject.SetActive(false);
    }
}
