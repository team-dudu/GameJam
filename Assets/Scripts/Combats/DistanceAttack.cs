using System;
using UnityEngine;

public class DistanceAttack : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}