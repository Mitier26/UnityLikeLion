using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public SoundManager soundManager;
    private Rigidbody2D myRigid;
    public float flyPower = 10f;
    private float angle;
    public float rotationSpeed = 5f;
    public float limitPower = 5f;
    private void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            soundManager.OnJumpSound();
            // myRigid.velocity = Vector2.up * 5f;
            myRigid.AddForce(Vector2.up * flyPower, ForceMode2D.Impulse);

            if (myRigid.velocity.y >= limitPower)
            {
                // myRigid.velocity = new Vector2(myRigid.velocity.x, limitPower);
                myRigid.velocity = new Vector2(0f, limitPower);
            }
        }
        
        // 새의 y 축 속도를 가지고 온다.
        // Clamp로 각도를 -90도 ~ 45도 사이로 제한한다. 
        float targetAngle = Mathf.Clamp(myRigid.velocity.y * 10f, -90f, 45f);
        // 현재 각도에서 타겟 각도로 부드럽게 이동하게 한다.
        angle = Mathf.Lerp(angle, targetAngle, rotationSpeed * Time.deltaTime);
        
        // 위에서 구한 각도를 실제로 대입한다.
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
