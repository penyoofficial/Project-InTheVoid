using UnityEngine;

/// <summary>
/// 普通攻击
/// </summary>
public class PlainAttack : Trick<Entity>
{
    public PlainAttack(Entity from, float manaCost = 0f, float cdMagnification = 1) : base(from, manaCost, cdMagnification)
    {
    }

    public override void Release()
    {
        if (CanRelease() && to != null)
        {
            base.Release();

            to.BeingHurt(from.atkBase);
            to.BeingKnockedOff(GetKnockbackVector(4));
        }
    }

    protected Vector2 GetKnockbackVector(float force)
    {
        float x = ((to.transform.position.x > from.transform.position.x) ? Vector2.right : Vector2.left).x * force;
        float y = x * 0.5f;
        return new(x, y);
    }
}
