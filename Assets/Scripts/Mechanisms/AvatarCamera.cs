using System.Collections;
using UnityEngine;

/// <summary>
/// 本地角色摄像机
/// </summary>
public class AvatarCamera : MonoBehaviour
{
    GameObject target;

    protected void Start()
    {
        This.Set(Context.AVATAR_CAMERA, gameObject);
    }

    protected void Update()
    {
        if (target == null)
        {
            target = This.Get(Context.AVATAR);
        }
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, formatSize, formatSize > 10 ? Time.deltaTime * 0.5f : Time.deltaTime * 2);
    }

    float formatSize = 5f;

    public void SetFormatSize(float size)
    {
        formatSize = size;
    }

    Vector2 velocity = Vector2.zero;

    protected void LateUpdate()
    {
        if (target != null)
        {
            Vector2 newPosition = Vector2.SmoothDamp(transform.position, target.transform.position, ref velocity, 0.3f);
            transform.position = new(newPosition.x, newPosition.y, transform.position.z);
        }
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        float duration = 0.3f;
        float magnitude = 0.1f;
        Vector3 originalPosition = transform.position;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPosition;
    }
}
