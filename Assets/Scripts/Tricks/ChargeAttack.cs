/// <summary>
/// 蓄力攻击
/// </summary>
public class ChargeAttack : PlainAttack
{
    readonly float chargeMagnification;

    public ChargeAttack(Entity from, float chargeMagnification, float manaCost = 0f, float cdMagnification = 1) : base(from, manaCost, cdMagnification)
    {
        this.chargeMagnification = chargeMagnification;
    }

    public new void Release()
    {
        if (CanRelease() && to != null)
        {
            base.Release();

            to.BeingHurt(from.atkBase * chargeMagnification);
            to.BeingKnockedOff(GetKnockbackVector(6));
        }
    }
}
