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
/// <i>大自然的怒火，似乎也快要烤焦祂自己的枝叶。</i>
/// </summary>
public class CorruptedSunflowerAI : AbstractAI
{
    public override int Hurt()
    {
        throw new System.NotImplementedException();
    }

    new void Update()
    {
        base.Update();

        
    }
}
