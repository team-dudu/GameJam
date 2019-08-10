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
		public MovingDirection CurrentDirection;
		private float colliderBoundsMargin = 0.5f;

		void Start()
        {
            Attack = GetComponent<IAttack>();
			_objectCollider = GetComponent<Collider2D>();
		}

        void Update()
        {
            transform.Translate(Time.deltaTime * Speed * Vector2.right);

			if (_platformCollider == null)
			{
				var groundInfo = Physics2D.Raycast(GroundDetection.position, Vector2.down, CastDistance);
				if (groundInfo.collider)
				{
					_platformCollider = groundInfo.collider;
					MoveEnemy();
				}
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
			var xDir = 0f;

			// Wait for the enemy to touch the ground before moving
			if (_platformCollider?.IsTouchingLayers() != true)
			{
				return;
			}

			var leftPlatformLimit = _platformCollider.bounds.min.x;
			var rightPlatformLimit = _platformCollider.bounds.max.x;
			var isSwitchingDirection = false;

			switch (CurrentDirection)
			{
				case MovingDirection.Left:
					var enemyLeftLimit = _objectCollider.bounds.min.x;
					if (enemyLeftLimit > leftPlatformLimit + colliderBoundsMargin)
					{
						// Avoid the game object to exit the collider
						xDir = enemyLeftLimit + 1f < leftPlatformLimit
								? leftPlatformLimit - enemyLeftLimit
								: 1f;

						IsMoving = true;
					}
					else
					{
						CurrentDirection = MovingDirection.Right;
						isSwitchingDirection = true;
						IsMoving = false;
					}
					break;
				case MovingDirection.Right:
					var enemyRightLimit = _objectCollider.bounds.max.x;
					if (enemyRightLimit < rightPlatformLimit - colliderBoundsMargin)
					{
						// Avoid the game object to exit the collider
						xDir = enemyRightLimit + 1f > rightPlatformLimit
								? rightPlatformLimit - enemyRightLimit
								: 1f;

						IsMoving = true;
					}
					else
					{
						CurrentDirection = MovingDirection.Left;
						isSwitchingDirection = true;
						IsMoving = false;
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