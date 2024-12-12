using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{
    public GameObject cannonBall;
    public GameObject firePoint;

    public Slider slider;
    public float maxPower;
    public float currentPower;
    public float fillSpeed;
    private int fillDirection = 1;
    
    public float rotateSpeed = 5f;
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if(currentPower >= maxPower) fillDirection = -1;
            else if(currentPower <= 0) fillDirection = 1;
            
            currentPower += fillSpeed* fillDirection * Time.deltaTime;
            currentPower = Mathf.Clamp(currentPower, 0, maxPower);
            slider.value = currentPower / maxPower;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(rotateSpeed * Time.deltaTime,0 , 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(-rotateSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0, Space.World);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.World);
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject ballInstance = Instantiate(cannonBall, firePoint.transform.position, transform.rotation);
            ballInstance.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * currentPower, ForceMode.Impulse);
            currentPower = 0;
            slider.value = currentPower / maxPower;
            
            // Destroy(ballInstance, 3);
        }
    }
}
