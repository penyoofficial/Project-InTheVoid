using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 世界
/// </summary>
public class World : MonoBehaviour
{
    protected void Start()
    {
        SingletonRegistry.Set(SR.WORLD, gameObject);
    }

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
}

public class ElementType
{
    public static string HYENA = "鬣狗";
    public static string TORTOISE = "陆龟";
    public static string CORRUPTED_SUNFLOWER = "腐败的向阳花";

    public static string COIN_BAG = "钱袋";
}
