using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private InputSystem_Actions action;
    private InputAction movementAction;
    private InputAction rotateAction;

    [SerializeField] private float speed;
    
    private Camera mainCamera;
    private Rigidbody2D rb;

    private void Awake()
    {
        action = new InputSystem_Actions();
        movementAction = action.Player.Move;
        rotateAction = action.Player.Look;
        
        mainCamera = Camera.main;
        
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        movementAction.Enable();
        rotateAction.Enable();
    }

    private void OnDisable()
    {
        movementAction.Disable();
        rotateAction.Disable();
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isplaying) return;
        
        // 이동 처리
        Vector2 moveVector2 = movementAction.ReadValue<Vector2>();
        rb.velocity = new Vector2(moveVector2.x * speed,moveVector2.y * speed);
        
        Vector2 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -49, 49);
        pos.y = Mathf.Clamp(pos.y, -49, 49);
        transform.position = pos;
    }




}
