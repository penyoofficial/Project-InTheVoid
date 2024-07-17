using UnityEngine;
using Utility;

/// <summary>
/// 古希腊掌握玩家进攻机制的神
/// </summary>
public class AvatarAttack : MonoBehaviour
{
    private Rigidbody2D avatar;

    void Start()
    {
        avatar = GetComponent<Rigidbody2D>();
    }

    [SerializeField] private AudioSource 背景音乐组件;

    [SerializeField] private AudioClip 常态背景音乐;

    [SerializeField] private AudioClip 战斗背景音乐;

    [SerializeField] private AudioClip 重要战斗背景音乐;

    private bool isBossAlive = false;

    void UpdateBGM()
    {
        if (Game2D.HasNearbyBoss(avatar) || isBossAlive)
        {
            背景音乐组件.clip = 重要战斗背景音乐;
            isBossAlive = true;
        }
        else if (Game2D.HasNearbyMonster(avatar))
        {
            背景音乐组件.clip = 战斗背景音乐;
        }
        else
        {
            背景音乐组件.clip = 常态背景音乐;
        }

        if (!背景音乐组件.isPlaying)
        {
            背景音乐组件.Play();
        }
    }

    public void KilledBoss() {
        isBossAlive = false;
    }

    [SerializeField] private int 攻击力 = 15;

    [SerializeField] private float 攻击范围 = 3f;

    [SerializeField] private float 攻击冷却 = 0.7f;

    private float lastAtkTime;

    [SerializeField] private int 蓄力攻击倍率 = 3;

    [SerializeField] private float 蓄力时间 = 2f;

    private float chargeStartTime;

    private bool isCharging;

    void Update()
    {
        UpdateBGM();

        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.JoystickButton2)) && Time.time >= lastAtkTime + 攻击冷却)
        {
            lastAtkTime = Time.time;

            foreach (var hit in Game2D.NearbyEntity(avatar, 攻击范围, new string[] { "Boss", "Monster" }))
            {
                if (hit.TryGetComponent<AbstractAI>(out var enemyAI))
                {
                    enemyAI.BeingHurt(攻击力);
                    enemyAI.BeingKnockedOff(GetKnockbackVectorX(hit, 4f));
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            isCharging = true;
            chargeStartTime = Time.time;
        }

        if (isCharging && Time.time >= chargeStartTime + 蓄力时间)
        {
            foreach (var hit in Game2D.NearbyEntity(avatar, 攻击范围, new string[] { "Boss", "Monster" }))
            {
                if (hit.TryGetComponent<AbstractAI>(out var enemyAI))
                {
                    enemyAI.BeingHurt(攻击力 * 蓄力攻击倍率);
                    enemyAI.BeingKnockedOff(GetKnockbackVectorX(hit, 8f));
                }
            }
            isCharging = false;
        }

        if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.JoystickButton3))
        {
            isCharging = false;
        }
    }

    private float GetKnockbackVectorX(Collider2D enemy, float force)
    {
        return ((enemy.transform.position.x > avatar.transform.position.x) ? Vector2.right : Vector2.left).x * force;
    }
}
