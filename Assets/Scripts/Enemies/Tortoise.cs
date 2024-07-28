using UnityEngine;
using Utility;

/// <summary>
/// 普通敌怪：陆龟 的人工智能
/// 
/// <para>陆龟会以自身为中心，不断的搜索附近的实体。如果没找到玩家，则左右闲逛；
/// 如果找到玩家，那就以最近的一个为目标，自身向天空腾飞，向其位置在 x 轴上移动，
/// 坠落造成大量伤害。</para>
/// 
/// <i>污染已经深入骨髓，殷红的血从感染处不断流出，散发着恶心的异味......</i>
/// </summary>
public class Tortoise : Enemy
{
    bool movingRight = true;

    protected new void Update()
    {
        if (life <= 0)
        {
            SingletonRegistry.Get(SR.WORLD).GetComponent<World>().RequestSpawn(ElementType.TORTOISE, spawnPoint, 30);
        }

        base.Update();

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        Collider2D[] hits = Game2D.NearbyEntities(rb, Game2D.MonsterDetectDistance, new string[] { "Player" });
        if (hits.Length != 0)
        {
            if (rb.velocity.y == 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 10);
            }
            else
            {
                if (rb.velocity.y < 0)
                {
                    rb.position = new Vector2(Mathf.MoveTowards(rb.position.x, hits[0].transform.position.x, velocityBase * 10 * Time.deltaTime), rb.position.y);
                    rb.velocity = new Vector2(rb.velocity.x, -10);
                }
            }
        }
        else
        {
            if (rb.velocity.x == 0)
            {
                movingRight = !movingRight;
            }
            if (movingRight)
            {
                rb.velocity = new Vector2(velocityBase, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-velocityBase, rb.velocity.y);
            }
        }

        movingRight = rb.velocity.x >= 0;

        if (movingRight && sr.flipX || !movingRight && !sr.flipX)
        {
            sr.flipX = !sr.flipX;
        }
    }

    public override string GetDeathReason()
    {
        return "你以为你是龟兔赛跑里的兔子，但乌龟不再是那只乌龟";
    }
}
