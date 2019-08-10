using System;
using GameJam;
using UnityEngine;

namespace GameJam
{
    public class MeleeAttack : MonoBehaviour, IAttack
    {
        private float _timeBtwAttack;
        public float startTimeBtwAttack;

        public Transform attackPos;
        public LayerMask whatIsEnemies;
        public float attackRange;
        public int damage;

        void Update()
        {
            _timeBtwAttack -= Time.deltaTime;
        }

        public void Shoot()
        {
            if (_timeBtwAttack <= 0)
            {
                var enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                foreach (var t in enemiesToDamage)
                {
                    t.GetComponent<Enemy>().TakeDamage(damage);
                }

                _timeBtwAttack = startTimeBtwAttack;
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPos.position, attackRange);
        }
    }
}