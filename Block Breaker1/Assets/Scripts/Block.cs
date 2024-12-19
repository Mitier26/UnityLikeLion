using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour, ICollisionHandler
{
    private void Start()
    {
        // GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
        Material material = GetComponent<SpriteRenderer>().material;
        Color color = new Color(Random.value, Random.value, Random.value);
        material.SetColor("_EmissionColor", color);
    }

    public void HandleCollision(Ball ball, Collision2D collision)
    {
        ball.direction = Vector3.Reflect(ball.direction, collision.contacts[0].normal);
        AudionManager.instance.BlockSound();
        GameManager.instance.OnBlockDestroyed();
        Destroy(gameObject);
    }
}
