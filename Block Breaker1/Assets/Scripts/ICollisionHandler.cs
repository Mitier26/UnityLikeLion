
using UnityEngine;

public interface ICollisionHandler
{
    void HandleCollision(Ball ball, Collision2D collision);
}