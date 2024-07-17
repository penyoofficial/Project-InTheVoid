using UnityEngine;

/// <summary>
/// 附属敌怪：尖刺 的人工智能
/// </summary>
public class Thorn : Enemy
{
    Transform target;
    float baseSpeed = 5f;
    float increasedSpeed = 15f;
    int increasedDamage = 9999;
    float distanceThreshold = 30f;
    Vector2 spawnPosition;

    new void Start()
    {
        base.Start();
    }

    public void Initialize(Vector2 targetPosition, Vector2 spawnPoint)
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        spawnPosition = spawnPoint;
        UpdateSpeedAndDamage(targetPosition);
    }

    void UpdateSpeedAndDamage(Vector2 targetPosition)
    {
        if (Vector2.Distance(targetPosition, spawnPosition) > distanceThreshold)
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
        if (life > 0)
        {
            TrackTarget();
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

    void TrackTarget()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = direction * velocityBase;
        }
    }

    public override string GetDeathReason()
    {
        return "你死于剧毒";
    }
}
