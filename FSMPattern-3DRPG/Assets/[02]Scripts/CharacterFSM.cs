using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CharacterFSMState
{
    Idle,
    Walk,
    Jump
}

public class CharacterFSM : MonoBehaviour
{
    private static readonly int Speed_Hash = Animator.StringToHash("Speed");
    private CharacterFSMState currentState = CharacterFSMState.Idle;
    private CharacterFSMState prevState = CharacterFSMState.Idle;

    private Rigidbody rb;
    
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float jumpForce = 10.0f;

    private bool isGrounded;
    private Vector2 moveInput;

    private InputAction moveAction;
    private InputAction jumpAction;
    
    private Animator animator;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        currentState = CharacterFSMState.Idle;

        moveAction = GetComponent<PlayerInput>().actions["Move"];
        jumpAction = GetComponent<PlayerInput>().actions["Jump"];
    }

    // Update is called once per frame
    void Update()
    {
        // 움직임 값 확인
        moveInput =  moveAction.ReadValue<Vector2>();
        bool bPressedJump = jumpAction.triggered;

        GroundCheck();
        StateChange(bPressedJump);
        
        EnterState();
        UpdateState();
        ExitState();
    }

    private void GroundCheck()
    {
        // 점프 상태 확인
        isGrounded = rb.velocity.y == 0.0f;
    }

    private void StateChange(bool bPressedJump)
    {
        prevState = currentState;
        
        switch (currentState)
        {
            case CharacterFSMState.Idle:
            {
                if (moveInput.sqrMagnitude > 0.0f)
                {
                    currentState = CharacterFSMState.Walk;
                }

                if (bPressedJump && isGrounded)
                {
                    currentState = CharacterFSMState.Jump;
                }
            }
                break;
            case CharacterFSMState.Walk:
            {
                if (moveInput.sqrMagnitude <= 0.0f)
                {
                    currentState = CharacterFSMState.Idle;
                }
                
                if (bPressedJump && isGrounded)
                {
                    currentState = CharacterFSMState.Jump;
                }
            }
                break;
            case CharacterFSMState.Jump:
            {
                if (isGrounded)
                {
                    currentState = CharacterFSMState.Idle;
                }
            }
                break;
        }
    }

    private void EnterState()
    {
        if (prevState != currentState)
        {
            switch (currentState)
            {
                case CharacterFSMState.Idle:
                {
                    animator.SetFloat(Speed_Hash, 0.0f);
                }
                    break;
                case CharacterFSMState.Walk:
                {
                    animator.SetFloat(Speed_Hash, 1.0f);
                }
                    break;
                case CharacterFSMState.Jump:
                {
                    animator.CrossFade("Jump", 0.1f);
                    rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                }
                    break;
            }
            
        }
    }
    
    private void UpdateState()
    {
        switch (currentState)
        {
            case CharacterFSMState.Walk:
            {
                rb.velocity = new Vector3(moveInput.x * moveSpeed, rb.velocity.y, moveInput.y * moveSpeed);
            }
                break;
            case CharacterFSMState.Jump:
            {
            }
                break;
        }
    }
    
    private void ExitState()
    {
        if (prevState != currentState)
        {
            switch (prevState)
            {
                case CharacterFSMState.Jump:
                    {
                        animator.CrossFade("Idles", 0.1f);
                    }
                    break;
            }
        }
    }
}
