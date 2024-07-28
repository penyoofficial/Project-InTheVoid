using UnityEngine;
using UnityEngine.UI;
using Utility;

/// <summary>
/// 敌怪骨架
/// 
/// <para>所有敌怪必须拥有的基本属性和必须遵守的基本规则。</para>
/// </summary>
public abstract class Enemy : Entity
{
    /// <summary>
    /// 难度因子
    /// 
    /// <para>全局所有的敌怪共享这个参数，该参数值越大，则生成出来的敌怪越强大。
    /// 每次生成新的敌怪都会令因子不断增大。因此在前期停留过长时间似乎没有任何益处；
    /// 同样的，失败过多也会令玩家背上因果的惩罚。</para>
    /// </summary>
    static float difficutyFactor = 0;

    public Vector2 spawnPoint;

    protected new void Start()
    {
        spawnPoint = transform.position;

        life = (int)(lifeLimition * (1 + difficutyFactor));
        mana = (int)(manaLimition * (1 + difficutyFactor));
        atkBase = (int)(atkBase * (1 + difficutyFactor));

        _PlainAttack = new(this);

        difficutyFactor += 0.02f;
    }

    protected void Update()
    {
        if (life <= 0)
        {
            SingletonRegistry.Get(SR.WORLD).GetComponent<World>().RequestSpawn(ElementType.COIN_BAG, transform.position);
            Destroy(gameObject);
        }
        else if (life < lifeLimition)
        {
            lifeDisplayComponent.gameObject.SetActive(true);
        }
        else
        {
            lifeDisplayComponent.gameObject.SetActive(false);
        }
    }

    protected void OnCollisionStay2D(Collision2D entity)
    {
        if (entity.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetString("DEATH_REASON", GetDeathReason());
            _PlainAttack.To(entity.gameObject.GetComponent<Avatar>()).Release();
        }
    }

    protected void OnTriggerEnter2D(Collider2D item)
    {
        if (item.CompareTag("Hellfire"))
        {
            BeingHurt(life + defence);
        }
    }

    public abstract string GetDeathReason();

    public Slider lifeDisplayComponent;

    protected PlainAttack _PlainAttack;

    public override void BeingHurt(float damage)
    {
        base.BeingHurt(damage);

        lifeDisplayComponent.value = life / lifeLimition;
    }

    protected GameObject GetNearestAvatar()
    {
        return GameObject.FindGameObjectWithTag("Player");
    }
}
