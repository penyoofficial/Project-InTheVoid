using UnityEngine;

/// <summary>
/// 冲撞攻击
/// </summary>
public class RushAttack : Trick<CorruptedSunflower>
{
    public RushAttack(CorruptedSunflower from, float manaCost, float cdMagnification = 1f) : base(from, manaCost, cdMagnification)
    {
    }

    public override void Release()
    {
        if (CanRelease() && to != null)
        {
            base.Release();

            Vector2 playerPosition = to.transform.position;
            Vector2 rushTarget = new(2 * playerPosition.x - from.transform.position.x, 2 * playerPosition.y - from.transform.position.y);
            from.GetComponent<Rigidbody2D>().velocity = rushTarget - (Vector2)from.transform.position;
        }
    }
}
