using System;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private float _timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;

    void Update()
    {
        if (_timeBtwAttack <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                var enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                foreach (var t in enemiesToDamage)
                {
                    t.GetComponent<Enemy>().TakeDamage(damage);
                }
            }

            _timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            _timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}