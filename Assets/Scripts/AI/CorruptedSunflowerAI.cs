using System.Collections;
using UnityEngine;
using Utility;

/// <summary>
/// 重要敌怪：腐败的向阳花 的人工智能
/// 
/// <para>腐败的向阳花（下称腐花）无视全部碰撞体，可以自由在空气和地块中穿梭。
/// 由于惧怕阳光的缘故，腐花总是会躲在地块里，远远地望着玩家。但每隔一段时间，
/// 腐花仍会快速向玩家冲来，并造成巨额伤害和击退，然后再次钻进地块中。
/// 当腐花生命值不足 50% 后，将从四面八方召唤植物毒刺飞弹向玩家射去，
/// 并大幅减短每两次冲刺间的等待时间。如果玩家在这一阶段试图逃跑（远离腐花出生点
/// 30f 距离），则每颗飞弹攻击力提升至 9999、移速提高 300%。</para>
/// 
/// <b>技能</b>
/// <list type="bullet">
///     <item>
///         <term>绝望的冲刺</term>
///         <description>瞄准玩家当前位置 O，找到自身以点 O 为基准的对称点
///         B，并向点 B 高速移动，对路径上的玩家造成 25 伤害。若玩家位置变化，
///         自己的移动路径不变。该技能拥有 10 秒冷却。</description>
///     </item>
///     <item>
///         <term>有形的怒火</term>
///         <description>当自身血量低于 50% 时，可在屏幕外部召唤一根植物毒刺，
///         它全图追踪离自己最近的玩家，且不会受到伤害，击中玩家造成 1 伤害和小幅击退。
///         如果玩家离自身出生点超过 30f 距离，毒刺攻击力提升至 9999、移速提高 300%。
///         只要有毒刺在场，自身全部技能冷却减少 70%。该技能拥有 1 秒冷却。
///         </description>
///     </item>
/// </list>
/// 
/// <i>大自然的怒火，似乎也快要烤焦祂自己的枝叶。</i>
/// </summary>
public class CorruptedSunflowerAI : AbstractAI
{
    private float rushCooldown = 10f;
    private float furyCooldown = 1f;
    private bool isFurious = false;

    private float lastRushTime = 0;

    private GameObject player;
    private Transform spawnPoint;

    new void Start()
    {
        base.Start();

        spawnPoint = transform;
    }

    public override int Hurt()
    {
        return base.Hurt();
    }

    new void Update()
    {
        base.Update();

        Collider2D[] players = Game2D.NearbyEntity(aiRB, Game2D.BossDetectDistance, new string[] { "Player" });
        if ((players.Length != 0 || player != null) && life > 0)
        {
            if (player == null)
            {
                player = players[0].gameObject;
            }

            if (life <= 初始生命 * 0.5 && !isFurious)
            {
                isFurious = true;
                StartCoroutine(SummonThorns());
            }

            if (Time.time >= lastRushTime + rushCooldown)
            {
                RushAttack();
            }
        }
    }

    private void RushAttack()
    {
        lastRushTime = Time.time;
        Vector2 playerPosition = player.transform.position;
        Vector2 rushTarget = new(2 * playerPosition.x - transform.position.x, 2 * playerPosition.y - transform.position.y);
        aiRB.velocity = rushTarget - (Vector2)transform.position;
    }

    private IEnumerator SummonThorns()
    {
        while (life > 0)
        {
            yield return new WaitForSeconds(furyCooldown);
            Collider2D[] players = Game2D.NearbyEntity(aiRB, 999f, new string[] { "Player" });
            if (players.Length != 0)
            {
                Vector2 playerPosition = players[0].transform.position;
                GameObject thorn = Instantiate(Resources.Load<GameObject>("Thorn"));
                thorn.transform.position = new Vector2(playerPosition.x, playerPosition.y + 20f);

                if (thorn.TryGetComponent<ThornAI>(out var thornAI))
                {
                    thornAI.Initialize(playerPosition, spawnPoint.position);
                }
            }
        }
    }

    void OnDestroy()
    {
        player.GetComponent<AvatarAttack>().KilledBoss();
    }
}
