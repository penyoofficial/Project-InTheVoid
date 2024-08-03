using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 世界
/// </summary>
public class World : MonoBehaviour
{
    protected void Start()
    {
        This.Set(Context.WORLD, gameObject);
    }

    public Tilemap groundTilemap;

    public void RequestSpawn(string type, Vector2 position, float cd = 0, Action<GameObject> initializor = null)
    {
        SetTimeout(() =>
        {
            GameObject entity = Instantiate(Resources.Load<GameObject>(type));
            entity.transform.position = position;
            initializor?.Invoke(entity);
        }, cd);
    }

    public static float BossDetectDistance = 18f;

    public static float MonsterDetectDistance = 8f;

    public Entity[] NearbyEntities(GameObject who, float radius, string[] tags = null)
    {
        List<Entity> meets = new();

        foreach (Collider2D hit in Physics2D.OverlapCircleAll(who.transform.position, radius))
        {
            bool hasWantedTag = false;
            if (tags == null || tags.Length == 0)
            {
                hasWantedTag = true;
            }
            else
            {
                foreach (string t in tags)
                {
                    if (hit.CompareTag(t))
                    {
                        hasWantedTag = true;
                        break;
                    }
                }
            }

            if (hasWantedTag && hit.TryGetComponent(out Entity e))
            {
                meets.Add(e);
            }
        }

        return meets.ToArray();
    }

    public Enemy[] NearbyEnemies(GameObject who)
    {
        return NearbyBoss(who).Concat(NearbyMonsters(who)).ToArray();
    }

    public bool HasNearbyEnemies(GameObject who)
    {
        return NearbyEnemies(who).Length != 0;
    }

    public Enemy[] NearbyBoss(GameObject who, float radius = 18)
    {
        return NearbyEntities(who, radius, new string[] { "Boss" }).Select((e) => e.GetComponent<Enemy>()).ToArray();
    }

    public bool HasNearbyBoss(GameObject who)
    {
        return NearbyBoss(who).Length != 0;
    }

    public Enemy[] NearbyMonsters(GameObject who, float radius = 8)
    {
        return NearbyEntities(who, radius, new string[] { "Monster" }).Select((e) => e.GetComponent<Enemy>()).ToArray();
    }

    public bool HasNearbyMonsters(GameObject who)
    {
        return NearbyMonsters(who).Length != 0;
    }

    public void SetTimeout(Action action, float secs = 0)
    {
        StartCoroutine(SetTimeoutCoroutine(action, secs));
    }

    IEnumerator SetTimeoutCoroutine(Action action, float secs)
    {
        yield return new WaitForSeconds(secs);
        action.Invoke();
    }

    public void PushNotice(string msg)
    {
        This.Get<GlobalNotice>(Context.GLOBAL_NOTICE).Push(msg);
    }
}

public class ElementType
{
    public static string HYENA = "鬣狗";
    public static string TORTOISE = "陆龟";
    public static string CORRUPTED_SUNFLOWER = "腐败的向阳花";

    public static string COIN_BAG = "钱袋";
}
