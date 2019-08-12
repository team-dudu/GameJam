using UnityEngine;

namespace GameJam
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 20f;
        public int damage = 1;
        public Rigidbody2D rb;
        public LayerMask whatIsTarget;
        public float distanceLimit = 10f;

        private Vector2 startPosition;

        public Vector2 direction = Vector2.right;

        void Start()
        {
            rb.velocity = direction * speed;
            startPosition = transform.position;
        }

        private void Update()
        {
            float bulletDistance = Vector2.Distance(startPosition, transform.position);

            if (bulletDistance > distanceLimit)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var layerValue = 1 << other.gameObject.layer;
            var maskValue = whatIsTarget.value;

            if ((layerValue & maskValue) == layerValue)
            {
                var o = other.GetComponent<IDamageable>();
                o?.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}