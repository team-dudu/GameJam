using UnityEngine;

namespace GameJam
{
    public abstract class Character : MonoBehaviour, IDamageable
    {
        Animator animator;

        public int health = 5;
		public bool IsMoving = false;
		public bool IsAttacking = false;

		private Animator _animator;

		public Character()
		{
			_animator = GetComponent<Animator>();
		}

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void TakeDamage(int damage)
        {
            animator.SetTrigger("TriggerDamaged");
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