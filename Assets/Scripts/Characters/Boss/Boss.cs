using UnityEngine;
using UnityEngine.UI;

namespace GameJam
{
    public class Boss : Character
    {
        int health = 50;
        public int damage;
        private float timeBtwDamage = 1.5f;

        public Slider healthBar;

        private void Update()
        {
            // give the player some time to recover before taking more damage !
            if (timeBtwDamage > 0)
            {
                timeBtwDamage -= Time.deltaTime;
            }

            healthBar.value = health;
        }

        private void OnTriggerEnter2D(Collider2D other)
        { 
            if (!other.CompareTag("Player")) return;
            // deal the player damage !
            if (timeBtwDamage <= 0)
            {
                other.GetComponent<IDamageable>().TakeDamage(damage);
            }
        }
    }
}