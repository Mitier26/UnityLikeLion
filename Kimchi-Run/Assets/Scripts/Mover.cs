using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Setting")] public float moveSpeed = 2f;

    private void Update()
    {
        transform.position += Vector3.left * GameManager.Instance.CalculateGameSpeed() * Time.deltaTime;
    }
}
