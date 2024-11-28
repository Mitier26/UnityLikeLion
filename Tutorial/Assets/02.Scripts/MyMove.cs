using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMove : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Animator animator;
    public float speed = 2;
    public float jumpPower = 4f;
    public bool isGround;
    
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
       
        Jump();
    }
    
    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        Vector3 dir = new Vector3(h, 0, v).normalized;
            
        transform.position += dir * speed * Time.deltaTime;

        if (h == 0 && v == 0)
        {
            animator.SetBool("isWalk", false);
        }
        else
        {
            animator.SetBool("isWalk", true);
        }
        
        if (h != 0 || v != 0)
        {
            Vector3 targetPos = transform.position + dir;
            transform.LookAt(targetPos);
            
        }
    }

    private void LateUpdate()
    {
        if (rigidbody.velocity.y > 0.05f)
        {
            animator.SetBool("isUp", true);
        }
        else if (rigidbody.velocity.y < -0.05f)
        {
            animator.SetBool("isDown", true);
        }
        else if (isGround)
        {
            animator.SetBool("isUp", false);
            animator.SetBool("isDown", false);
            
        }
    }

    private void Jump()
    {
        // 스페이스바를 눌렀을 때
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            animator.SetTrigger("Jump");
        }
    }
}
