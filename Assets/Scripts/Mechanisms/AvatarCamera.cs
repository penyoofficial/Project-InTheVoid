using UnityEngine;

/// <summary>
/// 本地玩家摄像机
/// </summary>
public class AvatarCamera : MonoBehaviour
{
    protected void Start()
    {
        SingletonRegistry.Set(SR.AVATAR_CAMERA, gameObject);
    }

    protected void Update()
    {
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, formatSize, formatSize > 10 ? Time.deltaTime * 0.5f : Time.deltaTime * 2);
    }

    float formatSize = 5f;

    public void SetFormatSize(float size)
    {
        formatSize = size;
    }
}
