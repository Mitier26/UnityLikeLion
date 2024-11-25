using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed;
    private Vector3 movement;
    private void Start()
    {
        // transform.position += Vector3.forward;
    }

    private void Update()
    {
        // float horizontal = Input.GetAxis("Horizontal");
        // float vertical = Input.GetAxis("Vertical");

        // transform.position += new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime; 
        // transform.Translate(new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime);

        // transform.position += Vector3.forward * Time.deltaTime;

        // movement.x = Input.GetAxis("Horizontal");
        // movement.z = Input.GetAxis("Vertical");
        // movement.Normalize();
        // transform.position += movement * speed * Time.deltaTime;

        //if (Input.GetKey(KeyCode.W))
        //{
        //    transform.position += transform.forward * speed * Time.deltaTime;
        //}
        //else if (Input.GetKey(KeyCode.S))
        //{
        //    transform.position -= transform.forward * speed * Time.deltaTime;
        //}
        //else if (Input.GetKey(KeyCode.A))
        //{
        //    transform.position -= transform.right * speed * Time.deltaTime;
        //}
        //else if (Input.GetKey(KeyCode.D))
        //{
        //    transform.position += transform.right * speed * Time.deltaTime;
        //}

        // Input Manager
        
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v).normalized;

        transform.position += dir * speed * Time.deltaTime;
    }
}
