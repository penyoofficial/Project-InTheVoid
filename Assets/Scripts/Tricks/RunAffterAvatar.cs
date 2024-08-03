/// <summary>
/// 追逐玩家
/// </summary>
public class RunAffterAvatar : Trick<Enemy>
{
    public RunAffterAvatar(Enemy from, float manaCost, float cdMagnification = 1) : base(from, manaCost, cdMagnification)
    {
    }
}