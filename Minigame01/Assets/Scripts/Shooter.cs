using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{   
    public GameObject bulletPrefab;

    private InputSystem_Actions action;
    private InputAction shoot;

    private void Awake()
    {
        action = new InputSystem_Actions();
        shoot = action.Player.Attack;
    }

    private void OnEnable()
    {
        shoot.Enable();
    }

    private void OnDisable()
    {
        shoot.Disable();
    }

    private void Update()
    {
        
        if (shoot.triggered)
        {
            Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.SetBullet(transform.up);
        }
    }
}
