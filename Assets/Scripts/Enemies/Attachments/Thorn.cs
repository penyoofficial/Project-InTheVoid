using UnityEngine;

/// <summary>
/// 附属敌怪：尖刺 的人工智能
/// </summary>
public class Thorn : Enemy
{
    Avatar target;
    readonly float baseSpeed = 5;
    readonly float increasedSpeed = 15;
    readonly int increasedDamage = 9999;
    readonly float distanceThreshold = 30;

    public void Setup(Avatar target, Vector2 hostSpawnPoint)
    {
        this.target = target;

        if (Vector2.Distance(target.transform.position, hostSpawnPoint) > distanceThreshold)
        {
            velocityBase = increasedSpeed;
            atkBase = increasedDamage;
        }
        else
        {
            velocityBase = baseSpeed;
            atkBase = 1;
        }
    }

    protected new void Update()
    {
        if (target != null)
        {
            GetComponent<Rigidbody2D>().linearVelocity = (target.transform.position - transform.position).normalized * velocityBase;
        }
    }

    protected new void OnCollisionStay2D(Collision2D entity)
    {
        base.OnCollisionStay2D(entity);

        if (entity.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    public override string GetDeathReason()
    {
        return "你死于剧毒";
    }
}
