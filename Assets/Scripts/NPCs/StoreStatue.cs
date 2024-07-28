using UnityEngine;

public class StoreStatue : MonoBehaviour
{
    protected void Start()
    {
        SingletonRegistry.Set(SR.STORE_STATUE, gameObject);
    }
}
