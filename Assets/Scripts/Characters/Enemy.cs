using System;
using UnityEngine;

namespace GameJam
{
    public class Enemy : Character
    {
        public float Speed = 1f;
        public LayerMask WhatIsEnemies;
        public float AttackRange;
        public GameObject DistanceWeaponPrefab;
        public GameObject CaCWeaponPrefab;

        protected int _roundTripCount = 0;

        protected bool _cacActivated = false;
        protected bool _jumpActivated = false;

        private Collider2D _objectCollider;
        private Collider2D _platformCollider;

        [SerializeField]
        protected Transform m_WallCheck; // A position marking where to check if the player is grounded.

        private GameObject _distanceWeapon;
        private GameObject _cacWeapon;

        public new void Start()
        {
            base.Start();
            _objectCollider = GetComponent<Collider2D>();

            _distanceWeapon = Instantiate(DistanceWeaponPrefab, transform);
            _cacWeapon = Instantiate(CaCWeaponPrefab, transform);
        }

        public new void Update()
        {
            if (_isDead) return;
            base.Update();

            var groundInfo = Physics2D.Raycast(m_GroundCheck.position, Vector2.down, k_GroundedRadius);
            if (groundInfo.collider)
            {
                _platformCollider = groundInfo.collider;
            }

            groundInfo = Physics2D.Raycast(m_WallCheck.position, Vector2.right, k_GroundedRadius);
            if (groundInfo.collider)
            {
                Flip();
                _roundTripCount++;
            }

            //todo refacto
            if(this.gameObject.name!= "Spoiler")
            {
                MoveEnemy();
            }
            

            if (_cacActivated)
            {
                _cacWeapon.GetComponent<IAttack>().Shoot(transform.right,WhatIsEnemies);
            }

            var enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, AttackRange, WhatIsEnemies);
            foreach (var t in enemiesToDamage)
            {
                var enemyPosition = t.GetComponent<PlayerController>().transform.position;
                _distanceWeapon.GetComponent<IAttack>().Shoot(enemyPosition - transform.position,WhatIsEnemies);
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, AttackRange);
        }

        public void MoveEnemy()
        {
            Move(m_FacingRight ? Speed : -Speed, false, _jumpActivated);
            const int roundedDecimal = 2;

            // Wait for the enemy to touch the ground before moving
            if (_platformCollider?.IsTouchingLayers() != true)
            {
                return;
            }

            var leftPlatformLimit = (float) Math.Round(_platformCollider.bounds.min.x, roundedDecimal);
            var rightPlatformLimit = (float) Math.Round(_platformCollider.bounds.max.x, roundedDecimal);

            if (m_FacingRight)
            {
                var enemyRightLimit = (float) Math.Round(_objectCollider.bounds.max.x, roundedDecimal);
                if (enemyRightLimit >= rightPlatformLimit)
                {
                    Flip();
                    _roundTripCount++;
                }
            }
            else
            {
                var enemyLeftLimit = (float) Math.Round(_objectCollider.bounds.min.x, roundedDecimal);
                if (enemyLeftLimit <= leftPlatformLimit)
                {
                    Flip();
                    _roundTripCount++;
                }
            }
        }
    }
}