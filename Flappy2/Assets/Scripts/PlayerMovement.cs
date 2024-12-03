using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpPower = 10f;
    public float limitPower = 5f;
    public float rotationSpeed = 5f;
    private float angle;


    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);

            if (rb.velocity.y > limitPower)
            {
                rb.velocity = new Vector3(0, limitPower, 0);
            }
        }
        // var playerEulerAngle = transform.eulerAngles;
        // playerEulerAngle.z = rb.velocity.y * 5f;
        //
        // transform.eulerAngles = playerEulerAngle;
        
        // 새의 y 축 속도를 가지고 온다.
        // Clamp로 각도를 -90도 ~ 45도 사이로 제한한다. 
        float targetAngle = Mathf.Clamp(rb.velocity.y * 10f, -90f, 45f);
        // 현재 각도에서 타겟 각도로 부드럽게 이동하게 한다.
        angle = Mathf.Lerp(angle, targetAngle, rotationSpeed * Time.deltaTime);
        
        // 위에서 구한 각도를 실제로 대입한다.
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

}
