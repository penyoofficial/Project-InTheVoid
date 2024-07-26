using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

/// <summary>
/// 实体骨架
/// 
/// <para>所有智能体必须拥有的基本属性和必须遵守的基本规则。</para>
/// </summary>
public abstract class Entity : MonoBehaviour
{
    protected void Start()
    {
        life = lifeLimition;
        mana = manaLimition;
    }

    // ---------------
    // 生命、法力与防御
    // ---------------
    public float lifeLimition;
    protected float life;
    public float lifeAutoRecoveredPerSec;
    public float manaLimition;
    protected float mana;
    public float manaAutoRecoveryPerSec;
    public float defence;

    public virtual void BeingHurt(float damage)
    {
        float finalDamage = damage - defence;
        life -= finalDamage > 1 ? finalDamage : 1;
    }

    public bool CanCostMana(float cost)
    {
        return cost <= mana;
    }

    public void CostMana(float cost)
    {
        if (!CanCostMana(cost))
        {
            return;
        }

        mana -= cost;
    }

    public enum HealType
    {
        LIFE, MANA,
    }

    public void BeingHealed(float amount, HealType type = HealType.LIFE)
    {
        if (amount < 0)
        {
            return;
        }

        switch (type)
        {
            case HealType.LIFE:
                float finalLife = life + amount;
                life = finalLife <= lifeLimition ? finalLife : lifeLimition;
                break;
            case HealType.MANA:
                float finalMana = mana + amount;
                mana = finalMana <= manaLimition ? finalMana : manaLimition;
                break;
        }
    }

    protected IEnumerator AutoRecover(Func<GameObject, bool> condition, Action callbackFn = null)
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (condition(gameObject))
            {
                if (life + lifeAutoRecoveredPerSec <= lifeLimition)
                {
                    life += lifeAutoRecoveredPerSec;
                }
                if (mana + manaAutoRecoveryPerSec <= manaLimition)
                {
                    mana += manaAutoRecoveryPerSec;
                }
            }
            callbackFn?.Invoke();
        }
    }

    // ----
    // 运动
    // ----
    public float velocityBase;

    public void BeingKnockedOff(Vector2 force)
    {
        GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }

    // -------
    // 普通攻击
    // -------
    public float atkBase;

    // ----
    // 技能
    // ----
    public float cdBase;

    // ----
    // 效果
    // ----
    protected Dictionary<string, Effect> effects = new();

    public void BeingAffected(Effect effect, float duration)
    {
        if (effects.ContainsKey(effect.name))
        {
            effects[effect.name].Cancel();
            effects.Remove(effect.name);
        }

        effect.Apply(this, duration);
        StartCoroutine(Async.SetTimeout(effect.Cancel, duration));
        effects.Add(effect.name, effect);
    }
}
