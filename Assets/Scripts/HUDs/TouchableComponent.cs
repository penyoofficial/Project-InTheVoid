using UnityEngine;

public class TouchableComponent : MonoBehaviour
{
    protected void Start()
    {
#if UNITY_STANDALONE
        gameObject.SetActive(false);
#endif
    }
}