using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CharController : MonoBehaviour
{
    public static readonly int Speed = Animator.StringToHash("Speed");
    InputAction Move_Input;
    InputAction Jump_Input;
    Animator animator;
    Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;

    public bool isGrounded = true;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 1f;
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
        Jump_Input = Input.actions["Jump"];
        
        cameraOffset = mainCamera.transform.position - transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = Move_Input.ReadValue<Vector2>();
        
        if(moveValue.x != 0)
            spriteRenderer.flipX = moveValue.x < 0;

        animator.SetFloat(Speed, Math.Abs(moveValue.x) );
        
        rigidbody.position += new Vector2(moveValue.x * speed * Time.deltaTime, 0);

        if (Jump_Input.IsPressed())
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, LayerMask.GetMask("Ground"));

            if (hit.distance <= 0)
            {
                animator.SetBool("Ground", true);
                rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }  
    }

    IEnumerator JumpEndChekc()
    {
        while (true)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, LayerMask.GetMask("Ground"));

            if (hit.distance <= 0)
            {
                animator.SetBool("Ground", false);
                break;
            }
            yield return null;
        }
        isGrounded = true;
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.GetContact(0).normal == Vector2.up)
        {
            animator.SetBool("Ground", false);
            animator.Play("Alchemist_Idle");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        animator.SetBool("Ground", true);
    }
}
