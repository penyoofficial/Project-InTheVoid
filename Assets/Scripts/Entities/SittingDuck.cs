using UnityEngine;

/// <summary>
/// 普通敌怪：靶子 的人工智能
/// 
/// <para>靶子不会主动做出任何行为。</para>
/// 
/// <i>稻草人是打不还手、骂不还口的好朋友。</i>
/// </summary>
public class SittingDuckAI : Enemy
{
    protected new void OnCollisionStay2D(Collision2D entity)
    {
        return;
    }

    public override string GetDeathReason()
    {
        return "很难想象你是这样死的";
    }
}
