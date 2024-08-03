using System.Collections;
using UnityEngine;

/// <summary>
/// 本地角色摄像机（Beta）
/// </summary>
public class AvatarCameraX : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    public Vector3 offset;
    public float edgeDetectionThreshold = 1.0f;
    public float fixedCameraDelay = 0.5f;
    public Transform[] roomBounds;

    Vector3 velocity = Vector3.zero;
    Vector3 lastTargetPosition;
    bool isFixedCamera = false;
    Vector3 fixedCameraPosition;

    void Start()
    {
        if (target != null)
        {
            lastTargetPosition = target.position;
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            float distance = Vector3.Distance(lastTargetPosition, targetPosition);

            if (IsAtEdge(targetPosition))
            {
                targetPosition = HandleEdgeDetection(targetPosition);
            }

            if (IsInRoomBounds(targetPosition))
            {
                if (!isFixedCamera)
                {
                    StartCoroutine(SetFixedCamera(targetPosition));
                }
            }
            else
            {
                isFixedCamera = false;
            }

            if (!isFixedCamera)
            {
                Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
                transform.position = newPosition;
            }
            else
            {
                transform.position = fixedCameraPosition;
            }

            lastTargetPosition = target.position;
        }
    }

    bool IsAtEdge(Vector3 targetPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(targetPosition, Vector2.down, edgeDetectionThreshold);
        return hit.collider != null && Mathf.Abs(hit.point.y - targetPosition.y) > edgeDetectionThreshold;
    }

    Vector3 HandleEdgeDetection(Vector3 targetPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(targetPosition, Vector2.down, edgeDetectionThreshold);
        if (hit.collider != null)
        {
            targetPosition.y = hit.point.y + offset.y;
        }
        return targetPosition;
    }

    bool IsInRoomBounds(Vector3 targetPosition)
    {
        foreach (Transform bound in roomBounds)
        {
            if (targetPosition.x > bound.position.x - bound.localScale.x / 2 &&
                targetPosition.x < bound.position.x + bound.localScale.x / 2 &&
                targetPosition.y > bound.position.y - bound.localScale.y / 2 &&
                targetPosition.y < bound.position.y + bound.localScale.y / 2)
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator SetFixedCamera(Vector3 targetPosition)
    {
        isFixedCamera = true;
        fixedCameraPosition = targetPosition;
        yield return new WaitForSeconds(fixedCameraDelay);
    }
}
