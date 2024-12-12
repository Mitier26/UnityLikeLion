using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float frontMoveSpeed = 5f; // 전진 속도
    public float sideMoveSpeed = 5f;  // 좌우 이동 속도
    public float backMoveSpeed = 3f;  // 후진 속도
    public GameObject bulletPrefab;  // 총알 프리팹
    public Transform firePoint;      // 총알 발사 위치
    public float bulletSpeed = 20f;  // 총알 속도
    public float rotationSpeed = 5f; // 회전 속도

    private Rigidbody rb;
    private Vector3 lastMousePosition; // 이전 마우스 위치
    
    public Slider slider;
    public float maxPower;
    public float currentPower;
    public float fillSpeed;
    private int fillDirection = 1;

    Animator animator;
    bool isJumping = false;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        lastMousePosition = Input.mousePosition; // 마우스 초기 위치 저장

        // 마우스를 화면에 고정
        Cursor.lockState = CursorLockMode.Locked; // 화면 중앙에 고정
        Cursor.visible = false;                  // 마우스 커서 숨기기
    }

    private void Update()
    {
        // 이동 처리
        Vector3 velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            velocity += transform.TransformDirection(Vector3.forward) * frontMoveSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            velocity += transform.TransformDirection(Vector3.back) * backMoveSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            velocity += transform.TransformDirection(Vector3.left) * sideMoveSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            velocity += transform.TransformDirection(Vector3.right) * sideMoveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(Vector3.up * 30, ForceMode.Impulse);
            isJumping = true;
            animator.SetBool("Jump",isJumping);
        }

        Vector3 currentVelocity = rb.velocity;
        velocity.y = currentVelocity.y; // Y 축 속도를 유지
        rb.velocity = velocity;
        animator.SetFloat("Speed", rb.velocity.magnitude);

        // 마우스 좌우 이동 감지
        float deltaX = Input.GetAxis("Mouse X"); // 마우스의 좌우 이동량 (축)

        if (Mathf.Abs(deltaX) > 0.01f) // 좌우로 일정 이상 이동한 경우
        {
            RotateToMouseHorizontal(deltaX);
        }

        if (Input.GetMouseButton(0))
        {
            if(currentPower >= maxPower) fillDirection = -1;
            else if(currentPower <= 0) fillDirection = 1;
            
            currentPower += fillSpeed* fillDirection * Time.deltaTime;
            currentPower = Mathf.Clamp(currentPower, 0, maxPower);
            slider.value = currentPower / maxPower;
        }
        
        // 총알 발사
        if (Input.GetMouseButtonUp(0)) // 왼쪽 마우스 클릭
        {
            Fire();
        }
    }

    private void RotateToMouseHorizontal(float deltaX)
    {
        // 마우스 이동량을 회전에 반영
        float rotationAmount = deltaX * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotationAmount);
    }

    private void Fire()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.velocity = firePoint.forward * currentPower; // 총알을 발사 방향으로 이동
            }
            animator.SetTrigger("Fire");
            currentPower = 0;
            slider.value = currentPower / maxPower;
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        // 화면 포커스가 벗어나면 다시 마우스 잠금 설정
        if (hasFocus)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetBool("Jump",isJumping);
        }
    }

}
