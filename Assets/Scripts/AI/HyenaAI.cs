using UnityEngine;
using Utility;

/// <summary>
/// 普通敌怪：鬣狗 的人工智能
/// 
/// <para>鬣狗会以自身为中心，不断的搜索附近的实体。如果没找到玩家，则左右闲逛；
/// 如果找到玩家，那就以最近的一个为目标，自身移速变为原先的 2 倍，向其位置在 x
/// 轴上移动，如果遇到墙壁且自身未腾空，则尝试跳跃。</para>
/// 
/// <i>狂热的阮病毒，贪婪地吸食着宿主的养分，驱动它直至死亡。</i>
/// </summary>
public class HyenaAI : AbstractAI
{
    public override int Hurt()
    {
        return base.Hurt();
    }

    private bool movingRight = true;

    new void Update()
    {
        base.Update();

        Collider2D[] hits = Game2D.NearbyEntity(aiRB, Game2D.MonsterDetectDistance, new string[] { "Player" });
        if (hits.Length != 0)
        {
            移动速度 = 2f;
            Transform target = hits[0].transform;
            float step = 移动速度 * Time.deltaTime;
            aiRB.position = Vector2.MoveTowards(aiRB.position, new Vector2(target.position.x, aiRB.position.y), step);
            movingRight = aiRB.velocity.x >= 0;

            if (aiRB.velocity.Equals(new Vector2(0, 0)))
            {
                aiRB.velocity = new Vector2(aiRB.velocity.x, 5f);
            }
        }
        else
        {
            移动速度 = 1f;
            if (aiRB.velocity.x == 0)
            {
                movingRight = !movingRight;
            }
            if (movingRight)
            {
                aiRB.velocity = new Vector2(移动速度, aiRB.velocity.y);
            }
            else
            {
                aiRB.velocity = new Vector2(-移动速度, aiRB.velocity.y);
            }
        }

        if (movingRight && aiSR.flipX || !movingRight && !aiSR.flipX)
        {
            aiSR.flipX = !aiSR.flipX;
        }
    }
}
