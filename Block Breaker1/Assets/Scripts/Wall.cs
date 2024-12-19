using UnityEngine;

public class Wall : MonoBehaviour, ICollisionHandler
{
    public void HandleCollision(Ball ball, Collision2D collision)
    {
        ball.direction = Vector3.Reflect(ball.direction, collision.contacts[0].normal);
        AudionManager.instance.WallSound();
    }
}