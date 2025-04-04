using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private static readonly int Move = Animator.StringToHash("Move");
    private static readonly int Jump = Animator.StringToHash("JumpUp");
    private static readonly int Back = Animator.StringToHash("Back");
    private static readonly int JumpDown = Animator.StringToHash("JumpDown");
    private static readonly int Speed = Animator.StringToHash("Speed");

    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private float jumpForce = 5;
    
    private CharacterController _characterController;
    private Animator _animator;
    
    private float _gravity = -9.81f;
    
    private Vector3 _velocity;
    private float _groundDistance;

    private bool isJump = false;
    
    // 이동 속도
    private float speed = 0f;
    [SerializeField] private float acceleration = 0.1f;
    [SerializeField] private float brakeSpeed = 0.1f;
    [SerializeField] private float speedMultiplier = 1f;

    private bool _isGrounded;
    private float _groundedMinDistance = 0.1f;

    private bool IsGrounded
    {
        get
        {
            var distance = GetDistanceToGround();
            return distance < _groundedMinDistance;
        }
    }

    private void Awake()
    {
        // 초기화 작업
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
        HandleMovement();
        // ApplyGravity();
        CheckRun();
    }

    // 사용자 입력 처리 함수
    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (vertical > 0)
        {
            _animator.SetBool(Move, true);
        }
        else
        {
            _animator.SetBool(Move, false);
        }
        
        _animator.SetFloat(Speed, speed);

        if (vertical < 0)
        {
            _animator.SetBool(Back, true);
        }
        else
        {
            _animator.SetBool(Back, false);
        }

        
        // 이동 속도 조절, 달리기
        // if (Input.GetKey(KeyCode.LeftShift))
        // {
        //     speed += acceleration * Time.deltaTime;
        // }
        // else
        // {
        //     speed -= brakeSpeed * Time.deltaTime;
        // }
        //
        // speed = Mathf.Clamp(speed, 0, 1);
        //
        // _animator.SetFloat("Speed", speed);

        Vector3 movement = transform.forward * vertical;
        transform.Rotate(0, horizontal * rotateSpeed * Time.deltaTime, 0);
        
        // _characterController.Move(movement * (speed + speedMultiplier) * Time.deltaTime);
        
        // _groundDistance = GetDistanceToGround();
        
        // if (Input.GetButtonDown("Jump"))
        // {
        //     _velocity.y = Mathf.Sqrt(jumpForce * -2f * _gravity);
        //     isJump = true;
        //     _animator.SetBool(Jump, true);
        // }

        // if (isJump)
        // {
        //     if (_velocity.y < 0)
        //     {
        //         _animator.SetBool(Jump, false);
        //         _animator.SetBool(JumpDown, true);
        //     }
        //
        //     if (_groundDistance <= 0.5f && _velocity.y < 0)
        //     {
        //         isJump = false;
        //         _animator.SetBool(JumpDown, false);
        //         _animator.SetTrigger("Land");
        //     }
        // }
        
    }

    // 중력 적용 함수
    // private void ApplyGravity()
    // {
    //     _velocity.y += _gravity * Time.deltaTime;
    //     _characterController.Move(_velocity * Time.deltaTime);
    // }
    
    
    // 달리기 처리
    private void CheckRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed += acceleration * Time.deltaTime;
        }
        else
        {
            speed -= brakeSpeed * Time.deltaTime;
        }

        speed = Mathf.Clamp01(speed);
        
        // _animator.SetFloat("Speed", speed);
    }
    
    // 바닥과 거리를 계산하는 함수
    private float GetDistanceToGround()
    {
        float maxDistance = 10f;
        if (Physics.Raycast(transform.position, 
                Vector3.down, out RaycastHit hit, maxDistance))
        {
            return hit.distance;
        }
        else
        {
            return maxDistance;
        }
    }

    #region Animator Methods

    private void OnAnimationMove()
    {
        Vector3 movePosition;

        movePosition = _animator.deltaPosition;     // 애니메이션에서 이동한 거리
        
        // 증력 적용
        _velocity.y += _gravity * Time.deltaTime;
        movePosition.y = _velocity.y;

        _characterController.Move(movePosition);
    }
    

    #endregion


    #region Debug

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _groundDistance);
    }

    #endregion

   
}