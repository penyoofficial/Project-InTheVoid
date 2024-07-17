using UnityEngine;

/// <summary>
/// 附属敌怪：毒刺 的人工智能
/// </summary>
public class ThornAI : AbstractAI
{
    private Transform target;
    private float baseSpeed = 5f;
    private float increasedSpeed = 15f;
    private int increasedDamage = 9999;
    private float distanceThreshold = 30f;
    private Vector2 spawnPosition;

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

    private void UpdateSpeedAndDamage(Vector2 targetPosition)
    {
        if (Vector2.Distance(targetPosition, spawnPosition) > distanceThreshold)
        {
            移动速度 = increasedSpeed;
            攻击力 = increasedDamage;
        }
        else
        {
            移动速度 = baseSpeed;
            攻击力 = 1;
        }
    }

    new void Update()
    {
        base.Update();
        if (life > 0)
        {
            TrackTarget();
        }
    }

    private void TrackTarget()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            aiRB.velocity = direction * 移动速度;
        }
    }

    public override int Hurt()
    {
        return 攻击力;
    }
}
