using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [NonSerialized] public float speed = 15f;
    public float damage = 10f;
    public Vector2 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetBullet(Vector2 direction)
    {
        this.direction = direction.normalized;
        rb.velocity = this.direction * this.speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            var enemy = other.GetComponent<EnemyStateMachine>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage, player );
            }
            Destroy(gameObject);
        }
    }
}
