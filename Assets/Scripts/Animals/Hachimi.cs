using UnityEngine;

/// <summary>
/// 猫
/// </summary>
public class Hachimi : Entity
{
    float lastRunningTime = 0;

    protected new void Update()
    {
        base.Update();

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        GetComponent<Animator>().SetFloat("velocityX", Mathf.Abs(rb.velocityX));

        if (!rb.velocity.Equals(Vector2.zero))
        {
            lastRunningTime = Time.time;
        }
        GetComponent<Animator>().SetFloat("lastRunningTime", Time.time - lastRunningTime);
    }

    protected void OnDestroy()
    {
        World w = This.Get<World>(Context.WORLD);
        for (int i = 0; i < 10; i++)
        {
            w.RequestSpawn(ElementType.COIN_BAG, transform.position);
        }
    }
}
