using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1;
    public float castDistance = 2;
    public int health = 5;

    private bool _movingRight = true;

    public Transform groundDetection;

    void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector2.right);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, castDistance);
        if (!groundInfo.collider)
        {
            _movingRight = !_movingRight;
            transform.Rotate(0, 180, 0);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Damage taken !");
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}