using UnityEngine;

/// <summary>
/// 技能骨架
/// 
/// <para>所有技能必须拥有的基本属性和必须遵守的基本规则。</para>
/// </summary>
/// <typeparam name="E">释放技能的实体实现类</typeparam>
public abstract class Trick<E> where E : Entity
{
    protected readonly E from;
    readonly float manaCost;
    readonly float cdMagnification;
    float lastReleaseTime = -5.0f / 0;

    public Trick(E from, float manaCost, float cdMagnification = 1)
    {
        this.from = from;
        this.manaCost = manaCost;
        this.cdMagnification = cdMagnification;
    }

    public bool CanRelease()
    {
        float cd = from.cdBase * cdMagnification;
        return from.CanCostMana(manaCost) && lastReleaseTime + cd <= Time.time;
    }

    protected Entity to;

    public Trick<E> To(Entity to)
    {
        this.to = to;
        return this;
    }

    public virtual void Release()
    {
        if (CanRelease() && to != null)
        {
            from.CostMana(manaCost);
            lastReleaseTime = Time.time;
        }
    }
}
