using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private int velocity;
    [SerializeField] public float direction; //-1 for left, 1 for right
    
    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(velocity * direction, 0);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        EnemyHealth enemyHealth = col.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth)
        {
            enemyHealth.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
