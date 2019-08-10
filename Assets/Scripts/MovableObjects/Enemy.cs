using UnityEngine;

namespace GameJam
{
	public class Enemy : MonoBehaviour
	{
		//public int playerDamage;
		//public int range;
		public MovingDirection CurrentDirection;
		public float Speed = 1;
		public float CastDistance = 2;
		public Transform groundDetection;

		//[Header("Scanning settings")]
		//[Tooltip("The angle of the forward of the view cone. 0 is forward of the sprite, 90 is up, 180 behind etc.")]
		//[Range(0.0f, 360.0f)]
		//public float viewDirection = 0.0f;
		//[Range(0.0f, 360.0f)]
		//public float viewFov;
		//[Tooltip("Time in seconds without the target in the view cone before the target is considered lost from sight")]
		//public float timeBeforeTargetLost = 3.0f;

		//[Header("Misc")]
		//[Tooltip("Time in seconds during which the enemy flicker after being hit")]
		//public float flickeringDuration;

		private Collider2D _objectCollider;
		private Collider2D _platformCollider;
		private float colliderBoundsMargin = 0.5f;

		// Start is called before the first frame update
		void Start()
		{
			_objectCollider = GetComponent<Collider2D>();
		}

		// Update is called once per frame
		void Update()
		{
			if (_platformCollider == null)
			{
				var groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, CastDistance);
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
					}
					else
					{
						CurrentDirection = MovingDirection.Right;
						isSwitchingDirection = true;
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
					}
					else
					{
						CurrentDirection = MovingDirection.Left;
						isSwitchingDirection = true;
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

		private void OnTriggerEnter2D(Collider2D collision)
		{
			_platformCollider = collision;
		}

		private void OnTriggerStay2D(Collider2D collision)
		{
			MoveEnemy();
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			_platformCollider = null;
			throw new System.Exception("Should not happen");
		}
	}
}

