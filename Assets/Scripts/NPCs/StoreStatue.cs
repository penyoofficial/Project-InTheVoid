using UnityEngine;

/// <summary>
/// 女神像
/// </summary>
public class StoreStatue : MonoBehaviour
{
    protected void Start()
    {
        This.Set(Context.STORE_STATUE, gameObject);
    }
}
