using UnityEngine;

/// <summary>
/// 效果骨架
/// 
/// <para>所有增减益效果必须拥有的基本属性和必须遵守的基本规则。</para>
/// </summary>
public abstract class Effect
{
    public readonly string name;

    public readonly float appliedTime = Time.time;

    public float duration;

    public Effect()
    {
        name = "";
    }

    public virtual void Apply(Entity entity, float duration)
    {
        this.duration = duration;
    }

    private bool isCancelledInAdvance = false;

    public virtual void Cancel()
    {
        if (isCancelledInAdvance)
        {
            return;
        }
        isCancelledInAdvance = true;
    }
}
