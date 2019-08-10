using UnityEngine;

namespace GameJam
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 20f;
        public int damage = 1;
        public Rigidbody2D rb;
        public LayerMask whatIsTarget;

        public Vector3 direction = Vector3.right;

        void Start()
        {
            rb.velocity = direction * speed;
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