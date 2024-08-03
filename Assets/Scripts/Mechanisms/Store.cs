using UnityEngine;

/// <summary>
/// 商店
/// </summary>
public class Store : MonoBehaviour
{
    protected void Start()
    {
        This.Set(Context.STORE, gameObject);
        gameObject.SetActive(false);
    }
}
