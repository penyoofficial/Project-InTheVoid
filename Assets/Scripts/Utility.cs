using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
    /// <summary>
    /// 异步行为
    /// </summary>
    public class Async
    {
        public static IEnumerator SetTimeout(Action action, float secs)
        {
            yield return new WaitForSeconds(secs);
            action();
        }
    }

    /// <summary>
    /// 游戏控件
    /// </summary>
    public class HUD
    {
        public static IEnumerator FlashText(Text t)
        {
            for (int i = 0; i < 5; i++)
            {
                t.gameObject.SetActive(!t.gameObject.activeSelf);
                yield return new WaitForSeconds(0.2f);
            }
            t.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 游戏空间逻辑
    /// </summary>
    public class Game2D : MonoBehaviour
    {
        public static float BossDetectDistance = 18f;

        public static float MonsterDetectDistance = 8f;

        public static Collider2D[] NearbyEntities(Rigidbody2D rigidbody, float radius, string[] tags)
        {
            List<Collider2D> meets = new();

            Collider2D[] hits = Physics2D.OverlapCircleAll(rigidbody.position, radius);
            foreach (var hit in hits)
            {
                if (tags.Length == 0)
                {
                    meets.Add(hit);
                }
                foreach (var t in tags)
                {
                    if (hit.CompareTag(t))
                    {
                        meets.Add(hit);
                        break;
                    }
                }
            }

            return meets.ToArray();
        }

        public static Collider2D[] NearbyEnemies(Rigidbody2D who)
        {
            return NearbyBoss(who).Concat(NearbyMonsters(who)).ToArray();
        }

        public static bool HasNearbyEnemies(Rigidbody2D who)
        {
            return NearbyEnemies(who).Length != 0;
        }

        public static Collider2D[] NearbyBoss(Rigidbody2D who, float radius = 18)
        {
            return NearbyEntities(who, radius, new string[] { "Boss" });
        }

        public static bool HasNearbyBoss(Rigidbody2D who)
        {
            return NearbyBoss(who).Length != 0;
        }

        public static Collider2D[] NearbyMonsters(Rigidbody2D who, float radius = 8)
        {
            return NearbyEntities(who, radius, new string[] { "Monster" });
        }

        public static bool HasNearbyMonsters(Rigidbody2D who)
        {
            return NearbyMonsters(who).Length != 0;
        }
    }
}
