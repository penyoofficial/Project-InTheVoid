using UnityEngine;

/// <summary>
/// 背包
/// </summary>
public class Backpack : MonoBehaviour
{
    protected void Start()
    {
        This.Set(Context.BACKPACK, gameObject);
        gameObject.SetActive(false);
    }
}
