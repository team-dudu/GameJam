using UnityEngine;

namespace GameJam
{
    public class Character : MonoBehaviour, IDamageable
    {
        public int health = 5;

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}