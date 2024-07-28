using UnityEngine;

public class SummonThorn : Trick<CorruptedSunflower>
{
    public SummonThorn(CorruptedSunflower from, float manaCost, float cdMagnification = 0.1f) : base(from, manaCost, cdMagnification)
    {
    }

    public override void Release()
    {
        if (CanRelease() && to != null)
        {
            base.Release();

            Vector2 playerPosition = to.transform.position;
            SingletonRegistry.Get(SR.WORLD).GetComponent<World>().RequestSpawn("尖刺", new Vector2(playerPosition.x, playerPosition.y + 20), 0, (e) =>
            {
                Thorn t = e.GetComponent<Thorn>();
                t.Setup(to.GetComponent<Avatar>(), from.spawnPoint);
            });
        }
    }
}
