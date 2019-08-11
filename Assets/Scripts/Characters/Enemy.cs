using System;
using UnityEngine;

namespace GameJam
{
    public class Enemy : Character
    {
        private bool _movingRight = true;
		private Collider2D _platformCollider;
		private Collider2D _objectCollider;

		public float Speed = 1;
        public float CastDistance = 2;
        public Transform GroundDetection;
		public LayerMask WhatIsEnemies;
        public float AttackRange;
        public IAttack Attack;
		public MovingDirection CurrentDirection = MovingDirection.Left;
		private float colliderBoundsMargin = 0.05f;

		new void Start()
        {
			base.Start();

            Attack = GetComponent<IAttack>();
			_objectCollider = GetComponent<Collider2D>();
		}

		new void Update()
        {
			base.Update();
			if (_platformCollider == null)
			{
				var groundInfo = Physics2D.Raycast(GroundDetection.position, Vector2.down, CastDistance);
				if (groundInfo.collider)
				{
					_platformCollider = groundInfo.collider;
					MoveEnemy();
				}

				return;
			}
			else
			{
				MoveEnemy();
			}

			var enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, AttackRange, WhatIsEnemies);
            foreach (var t in enemiesToDamage)
            {
                var enemyPosition = t.GetComponent<PlayerController>().transform.position;
                Attack.Shoot(enemyPosition - transform.position);
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, AttackRange);
        }

		public void MoveEnemy()
		{
			const int roundedDecimal = 2;
			var xDir = 0f;

			// Wait for the enemy to touch the ground before moving
			if (_platformCollider?.IsTouchingLayers() != true)
			{
				return;
			}

			var leftPlatformLimit = (float)Math.Round(_platformCollider.bounds.min.x, roundedDecimal);
			var rightPlatformLimit = (float)Math.Round(_platformCollider.bounds.max.x, roundedDecimal);
			var isSwitchingDirection = false;

			switch (CurrentDirection)
			{
				case MovingDirection.Left:
					var enemyLeftLimit = (float)Math.Round(_objectCollider.bounds.min.x, roundedDecimal);
					if (enemyLeftLimit > leftPlatformLimit)
					{
						xDir = -1f;
						_animator.SetAnimation(AnimationParameter.IsMoving, true);
					}
					else
					{
						CurrentDirection = MovingDirection.Right;
						isSwitchingDirection = true;
						_animator.SetAnimation(AnimationParameter.IsMoving, false);
					}
					break;
				case MovingDirection.Right:
					var enemyRightLimit = (float)Math.Round(_objectCollider.bounds.max.x, roundedDecimal);
					if (enemyRightLimit < rightPlatformLimit)
					{
						xDir = -1f;
						_animator.SetAnimation(AnimationParameter.IsMoving, true);
					}
					else
					{
						CurrentDirection = MovingDirection.Left;
						isSwitchingDirection = true;
						_animator.SetAnimation(AnimationParameter.IsMoving, false);
					}
					break;
			}

			if (isSwitchingDirection)
			{
				transform.Rotate(0f, 180f, 0f);
			}

			transform.Translate(Time.deltaTime * Speed * new Vector3(
				x: xDir,
				y: 0,
				z: 0
			));
		}
	}
}