using UnityEngine;

namespace GameJam
{
    public class DistanceAttack : MonoBehaviour, IAttack
    {
        public Transform firePoint;
        private static GameObject bulletPrefab;

        private float _timeBtwAttack;
        public float startTimeBtwAttack=0.1f;

        void Start()
        {
            bulletPrefab=Resources.Load("Bullet") as GameObject;
        }

        void Update()
        {
            _timeBtwAttack -= Time.deltaTime;
        }

        public void Shoot(Vector3 direction, LayerMask? whatIsEnemies = null)
        {
            if(bulletPrefab == null)
            {
                bulletPrefab = Instantiate(Resources.Load("Bullet"), transform.position, transform.rotation) as GameObject;
            }

            if (_timeBtwAttack <= 0)
            {
                direction = direction.normalized;
                direction.y = 0;
                direction.z = 0;
                var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                bullet.GetComponent<Bullet>().direction = direction;
                if(whatIsEnemies!=null)
                {
                    bullet.GetComponent<Bullet>().whatIsTarget = (LayerMask) whatIsEnemies;
                }

                _timeBtwAttack = startTimeBtwAttack;
            }
        }
    }
}