using UnityEngine;
using Utility;

/// <summary>
/// 普通敌怪：陆龟 的人工智能
/// 
/// <para>陆龟会以自身为中心，不断的搜索附近的实体。如果没找到玩家，则左右闲逛；
/// 如果找到玩家，那就以最近的一个为目标，自身向天空腾飞，向其位置在 x 轴上移动，
/// 坠落造成大量伤害。平常不产生伤害。</para>
/// 
/// <i>污染已经深入骨髓，殷红的血从感染处不断流出，散发着恶心的异味......</i>
/// </summary>
public class TortoiseAI : AbstractAI
{
    public override int Hurt()
    {
        return canTakeDamage ? base.Hurt() : 0;
    }

    private bool movingRight = true;

    private bool canTakeDamage = false;

    new void Update()
    {
        base.Update();

        Collider2D[] hits = Game2D.NearbyEntity(aiRB, Game2D.MonsterDetectDistance, new string[] { "Player" });
        if (hits.Length != 0)
        {
            if (aiRB.velocity.y == 0)
            {
                aiRB.velocity = new Vector2(aiRB.velocity.x, 10f);
            }
            else
            {
                Transform target = hits[0].transform;
                if (aiRB.velocity.y < 0)
                {
                    canTakeDamage = true;
                    aiRB.position = new Vector2(Mathf.MoveTowards(aiRB.position.x, target.position.x, 移动速度 * 10 * Time.deltaTime), aiRB.position.y);
                    aiRB.velocity = new Vector2(aiRB.velocity.x, -10f);
                }
                else
                {
                    canTakeDamage = false;
                }
            }
            movingRight = aiRB.velocity.x >= 0;
        }
        else
        {
            canTakeDamage = false;
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
