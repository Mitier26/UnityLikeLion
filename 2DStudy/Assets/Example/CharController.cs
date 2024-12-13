using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CharController : MonoBehaviour
{
    InputAction Move_Input;
    Animator animator;
    Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float cameraSlowSpeed = 8f;
    [SerializeField] private float cameraFastSpeed = 15f;
    [SerializeField] private float maxDistance = 4f;
    
    Vector3 cameraOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        UnityEngine.InputSystem.PlayerInput Input = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        Move_Input = Input.actions["Move"];
        
        cameraOffset = mainCamera.transform.position - transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = Move_Input.ReadValue<Vector2>();
        
        if(moveValue.x != 0)
            spriteRenderer.flipX = moveValue.x < 0;
        
        animator.SetFloat("Speed", Math.Abs(moveValue.x) );
        
        rigidbody.position += new Vector2(moveValue.x * speed * Time.deltaTime, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        var charactorPosition = transform.position + cameraOffset;
        float distance = Vector3.Distance(mainCamera.transform.position, charactorPosition);

        cameraSpeed = distance > maxDistance ? cameraFastSpeed : cameraSlowSpeed;
            
        Vector3 newPosition = Vector3.Lerp(
            mainCamera.transform.position, 
            charactorPosition, 
            cameraSpeed * Time.deltaTime
        );
        mainCamera.transform.position = newPosition;

    }
}
