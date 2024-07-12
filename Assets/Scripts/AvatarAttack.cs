using System;
using System.Collections;
using UnityEngine;

public class AvatarAttack : MonoBehaviour
{
    private Rigidbody2D avatar;
    public GameObject projectilePrefab;
    public Transform firePoint;

    void Start()
    {
        avatar = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("XboxX"))
        {
            DashAttack();
        }

        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("XboxY"))
        {
            RangedAttack();
        }

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetAxis("XboxRightTrigger") > 0.5f)
        {
            StartCoroutine(MagicAttack());
        }
    }

    void DashAttack()
    {
        Collider2D[] hitMonsters = Physics2D.OverlapCircleAll(transform.position, 1.0f); // Adjust radius as needed
        foreach (Collider2D monster in hitMonsters)
        {
            if (monster.CompareTag("Monster"))
            {
                monster.GetComponent<Monster>().TakeDamage(10);
            }
        }
    }

    void RangedAttack()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Rigidbody2D>().velocity = firePoint.right * 10f; // Adjust speed as needed
    }

    IEnumerator MagicAttack()
    {
        yield return new WaitForSeconds(1.0f); // 1 second delay before attack

        GameObject nearestMonster = FindNearestMonster();
        if (nearestMonster != null)
        {
            nearestMonster.GetComponent<Monster>().TakeDamage(20);
        }
    }

    GameObject FindNearestMonster()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        GameObject nearestMonster = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject monster in monsters)
        {
            float distance = Vector2.Distance(transform.position, monster.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestMonster = monster;
            }
        }

        return nearestMonster;
    }
}

internal class Monster
{
    internal void TakeDamage(int v)
    {
        throw new NotImplementedException();
    }
}