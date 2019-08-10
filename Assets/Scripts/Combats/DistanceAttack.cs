using System;
using UnityEngine;

namespace GameJam
{
    public class DistanceAttack : MonoBehaviour, IAttack
    {
        public Transform firePoint;
        public GameObject bulletPrefab;

        public void Shoot()
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}