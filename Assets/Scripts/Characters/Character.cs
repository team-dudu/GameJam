using UnityEngine;

namespace GameJam
{
    public abstract class Character : MonoBehaviour, IDamageable
    {
        public int health = 5;
		public bool IsMoving = false;
		public bool IsAttacking = false;

		private Animator _animator;

		public Character()
		{
			_animator = GetComponent<Animator>();
		}

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
			_animator.SetTrigger("TriggerDeath");
        }
    }
}