using System.Collections;
using System.Collections.Generic;
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
        public static Collider2D[] Around(Rigidbody2D rigidbody, float radius, string[] tags)
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
    }
}
