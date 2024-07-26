using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 唯心世界
/// </summary>
public class World : MonoBehaviour
{
    public Tilemap groundTilemap;

    IEnumerator Spawn(string type, Vector2 position, float cd, Action<GameObject> initializor = null)
    {
        yield return new WaitForSeconds(cd);
        GameObject entity = Instantiate(Resources.Load<GameObject>(type));
        entity.transform.position = position;
        initializor?.Invoke(entity);
    }

    public void RequestSpawn(string type, Vector2 position, float cd = 0, Action<GameObject> initializor = null)
    {
        StartCoroutine(Spawn(type, position, cd, initializor));
    }

    protected void Update()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, formatSize, formatSize > 10 ? Time.deltaTime * 0.5f : Time.deltaTime * 2);
    }

    public Camera cam;

    float formatSize = 5f;

    public void SetFormatSize(float size)
    {
        formatSize = size;
    }
}

public class EntityType
{
    public static string HYENA = "鬣狗";
    public static string TORTOISE = "陆龟";
    public static string CORRUPTED_SUNFLOWER = "腐败的向阳花";

    public static string COIN_BAG = "钱袋";
}
