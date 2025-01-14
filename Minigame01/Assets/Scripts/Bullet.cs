using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    [NonSerialized] public float speed = 10f;
    public Vector2 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetBullet(Vector2 direction)
    {
        this.direction = direction.normalized;
        rb.velocity = this.direction * this.speed;
    }

}
