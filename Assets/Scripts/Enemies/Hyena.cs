using UnityEngine;
using Utility;

/// <summary>
/// 普通敌怪：鬣狗 的人工智能
/// 
/// <para>鬣狗会以自身为中心，不断的搜索附近的实体。如果没找到玩家，则左右闲逛；
/// 如果找到玩家，那就以最近的一个为目标，自身移速变为原先的 2 倍，向其位置在 x
/// 轴上移动，如果遇到墙壁且自身未腾空，则尝试跳跃。</para>
/// 
/// <i>狂热的神经病毒，贪婪地吸食着宿主的养分，驱动它直至死亡。</i>
/// </summary>
public class Hyena : Enemy
{
    bool movingRight = true;

    protected new void Update()
    {
        if (life <= 0)
        {
            SingletonRegistry.Get(SR.WORLD).GetComponent<World>().RequestSpawn(ElementType.HYENA, spawnPoint, 30);
        }

        base.Update();

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        Collider2D[] hits = Game2D.NearbyEntities(rb, Game2D.MonsterDetectDistance, new string[] { "Player" });
        if (hits.Length != 0)
        {
            velocityBase = 2f;
            rb.position = Vector2.MoveTowards(rb.position, new Vector2(hits[0].transform.position.x, rb.position.y), velocityBase * Time.deltaTime);

            if (rb.velocity.Equals(new Vector2(0, 0)))
            {
                rb.velocity = new Vector2(rb.velocity.x, 5);
            }
        }
        else
        {
            velocityBase = 1f;
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

        movingRight = rb.velocity.x > 0;

        if (movingRight && sr.flipX || !movingRight && !sr.flipX)
        {
            sr.flipX = !sr.flipX;
        }
    }

    public override string GetDeathReason()
    {
        return "你与狼共舞，但失败了";
    }
}