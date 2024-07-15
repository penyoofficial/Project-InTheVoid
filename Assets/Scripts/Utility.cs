using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
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

    public class Game2D
    {
        public static float BossDetectDistance = 18f;

        public static float MonsterDetectDistance = 7f;

        public static Collider2D[] NearbyEntity(Rigidbody2D rigidbody, float radius, string[] tags)
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

        public static Collider2D[] NearbyEnemy(Rigidbody2D who)
        {
            return NearbyBoss(who).Concat(NearbyMonster(who)).ToArray();
        }

        public static bool HasNearbyEnemy(Rigidbody2D who)
        {
            return NearbyEnemy(who).Length != 0;
        }

        public static Collider2D[] NearbyBoss(Rigidbody2D who)
        {
            return NearbyEntity(who, BossDetectDistance, new string[] { "Boss" });
        }

        public static bool HasNearbyBoss(Rigidbody2D who)
        {
            return NearbyBoss(who).Length != 0;
        }

        public static Collider2D[] NearbyMonster(Rigidbody2D who)
        {
            return NearbyEntity(who, MonsterDetectDistance, new string[] { "Monster" });
        }

        public static bool HasNearbyMonster(Rigidbody2D who)
        {
            return NearbyMonster(who).Length != 0;
        }
    }
}
