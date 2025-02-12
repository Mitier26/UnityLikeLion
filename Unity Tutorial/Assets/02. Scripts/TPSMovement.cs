using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSMovement : MonoBehaviour
{
    float h;
    float v;

    public float moveSpeed = 10f;
    public float rotSpeed = 30f;

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Move();
        Turn();
    }

    private void Move() // W, S 이동
    {
        transform.Translate(Vector3.forward * v * moveSpeed * Time.deltaTime);
    }

    private void Turn() // A, D 회전
    {
        transform.Rotate(Vector3.up, h * rotSpeed * Time.deltaTime);
    }

}
