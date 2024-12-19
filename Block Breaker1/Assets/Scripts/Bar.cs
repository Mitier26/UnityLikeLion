using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Bar : MonoBehaviour, ICollisionHandler
{
    public float speed = 5f;
    public float maxPos = 5;
    public void HandleCollision(Ball ball, Collision2D collision)
    {
        float barWidth = transform.localScale.x;
        float hitPoint = collision.GetContact(0).point.x;
        float relativeHitPoint = (hitPoint - transform.position.x) / (barWidth / 2);

        float maxBounceAngle = 75f;
        float bounceAngle = relativeHitPoint * maxBounceAngle;

        float angleInRadians = bounceAngle * Mathf.Deg2Rad;
        ball.direction = new Vector3(Mathf.Sin(angleInRadians), Mathf.Cos(angleInRadians), 0).normalized;
        AudionManager.instance.BarSound();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) && transform.position.x - transform.localScale.x / 2 > -maxPos)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) && transform.position.x + transform.localScale.x / 2 < maxPos)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
}