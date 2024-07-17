using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 抽象人工智能。为最终实现的类提供骨架。
/// </summary>
public abstract class AbstractAI : MonoBehaviour
{
    protected Rigidbody2D aiRB;

    protected SpriteRenderer aiSR;

    /// <summary>
    /// 难度因子
    /// 
    /// <para>全局所有的敌怪共享这个参数，该参数值越大，则生成出来的敌怪越强大。
    /// 每次生成新的敌怪都会令因子不断增大。因此在前期停留过长时间似乎没有任何益处；
    /// 同样的，失败过多也会令玩家背上因果的惩罚。</para>
    /// </summary>
    private static float difficutyFactor = 0;

    public void Start()
    {
        aiRB = GetComponent<Rigidbody2D>();
        aiSR = GetComponent<SpriteRenderer>();

        life = (int)(初始生命 * (1 + difficutyFactor));
        攻击力 = (int)(攻击力 * (1 + difficutyFactor));

        血条控件.gameObject.SetActive(false);

        difficutyFactor += 0.02f;
    }

    protected int life;

    [SerializeField] protected int 初始生命 = 100;

    [SerializeField] private Slider 血条控件;

    [SerializeField] protected int 攻击力 = 10;

    [SerializeField] protected float 攻击冷却 = 2f;

    protected float lastAtkTime = 0;

    [SerializeField] protected float 移动速度 = 1f;

    [SerializeField] protected int 防御力 = 0;

    [SerializeField] protected int 判定重量 = 1;

    public void BeingHurt(int damage)
    {
        血条控件.gameObject.SetActive(true);
        int finalDamage = damage - 防御力;
        life -= finalDamage > 1 ? finalDamage : 1;
        血条控件.value = 1.0f * life / 初始生命;
    }

    public void BeingKnockedOff(float vx)
    {
        vx /= 判定重量;
        aiRB.velocity = new Vector2(vx, Math.Abs(vx) * 0.33f);
    }

    /// <summary>
    /// 伤害值
    /// 
    /// <para>当玩家接触到 AI 时，将调用它的 Hurt() 方法。该方法会实时计算出本次接触所产生的伤害，
    /// 有的时候可能是 0，有的时候可能要经过复杂计算得出（如技能期间或特殊效果）。</para>
    /// </summary>
    /// <returns>伤害值</returns>
    public virtual int Hurt()
    {
        if (Time.time >= lastAtkTime + 攻击冷却)
        {
            lastAtkTime = Time.time;
            return 攻击力;
        }
        return 0;
    }

    public void Update()
    {
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
