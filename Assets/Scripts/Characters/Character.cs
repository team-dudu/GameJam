using UnityEditor.Animations;
using UnityEngine;

namespace GameJam
{
	public abstract class Character : MonoBehaviour, IDamageable
	{
        Animator animator;

		public int health = 5;
		public bool IsMoving = false;
		public bool IsAttacking = false;

		public bool ShouldDisappear = true;

		protected Animator _animator;
		protected BoxCollider2D _collider;

		protected void Start()
		{
			_animator = GetComponent<Animator>();
			_collider = GetComponent<BoxCollider2D>();
		}

		public void Update()
		{
			//Die();
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
			if (health <= 0)
			{
				return;
			}

			health -= damage;
			if (health <= 0)
			{
				Die();
			}
			else
			{
				_animator.SetTrigger("TriggerDamaged");
			}
		}

		public void Die()
		{
			_animator.SetAnimation(AnimationParameter.Death);
		}

		public void OnDeathEnds()
		{
			if (ShouldDisappear)
			{
				gameObject.SetActive(false);
				Destroy(this);
			}
			else
			{
				_animator.SetAnimation(AnimationParameter.DeathIdle);
			}
		}
	}
}