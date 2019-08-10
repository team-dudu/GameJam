using UnityEngine;

namespace GameJam
{
    public class Enemy : Character
    {
        public float speed = 1;
        public float castDistance = 2;

        private bool _movingRight = true;

        public Transform groundDetection;

        public LayerMask whatIsEnemies;
        public float attackRange;
        public IAttack attack;

        void Start()
        {
            attack = GetComponent<IAttack>();
        }

        void Update()
        {
            transform.Translate(Time.deltaTime * speed * Vector2.right);

            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, castDistance);
            if (!groundInfo.collider)
            {
                _movingRight = !_movingRight;
                transform.Rotate(0, 180, 0);
            }

            var enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, attackRange, whatIsEnemies);
            foreach (var t in enemiesToDamage)
            {
                var enemyPosition = t.GetComponent<PlayerController>().transform.position;
                attack.Shoot(enemyPosition - transform.position);
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}