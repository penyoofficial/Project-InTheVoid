/// <summary>
/// 重要敌怪：教徒 的人工智能
/// 
/// <para>教徒总是悬浮在空中，并从远古战场召唤出死去的魂灵协助战斗。
/// 当场上不足 3 个魂灵时，教徒就会尝试召唤一个新的魂灵；每个魂灵为教徒免伤
/// 33.33%，并不断向玩家冲刺造成击退和弱化效果（攻击力和移速减去 50%）。
/// 魂灵在场上如果 30 秒内没有被杀死，就会向玩家返还自身所受到的全部伤害，
/// 并回复全部生命值、重置计时。如果 300 秒内玩家未能击杀教徒（自激怒其后），
/// 教徒将见证上帝的光辉，对除自己以外全场所有友敌单位造成 9999 伤害，
/// 随后消失。</para>
/// 
/// <i>“你必须用你的死来见证我们的救赎。”</i>
/// </summary>
public class TheFollowerAI : AbstractAI
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
