using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody rigidbody;
    public float speed = 2;
    public float jumpPower = 4f;
    public bool isGround;
    private Vector3 movement;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        // OneStepMove();
    }

    private void Update()
    {
       Move();
       
       Jump();
    }

    private void OneStepMove()
    {
        transform.position += Vector3.forward;
    }
    private void AxisMove()
    {
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.position += new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime; 
        transform.Translate(new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime);

        transform.position += Vector3.forward * Time.deltaTime;

        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
        movement.Normalize();
        transform.position += movement * speed * Time.deltaTime;
    }

    private void InputMove()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }
    
    private void Move()
    {
        // Input Manager
        // 입력 받기
        // 유니티 내부에 있는 Input Manager를 이용한 방법
        
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        // Horizonatal과 Vertical은 유니티 세팅에서 정한 것
        // GetAxis는 ( -1 ~ 1 ) 사이의 값을 뱉어낸다.
        // GetAxisRaw는 ( -1, 0, 1 ) 값을 뱉어낸다.

        Vector3 dir = new Vector3(h, 0, v).normalized;
        // normalized : 정규화
        // 위와 오른쪽을 동시에 누르면 삼각함수로 인해 이동 값이 '루트2'가 된다.
        // 대각선 이동이 더 빨라지는 버그가 발생
        // 이것을 막고 방향벡터로 만드는 방법 = normalized

        // transform.position : 월드 좌표 기준으로 이동하는 방법
        // 방향 * 속도 * 프레임 보정
        transform.position += dir * speed * Time.deltaTime;

        if (h != 0 || v != 0)
        {
            Vector3 targetPos = transform.position + dir;
            transform.LookAt(targetPos);
        }
    }

    private void Jump()
    {
        // 스페이스바를 눌렀을 때
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            // 힘을 가한다 ( 방향 * 속도, 힘의 모드);
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            // 자기 자신의 Rigidbody에 접근에 힘을 가한다. 위로 점프 파워 만큰 순간적 힘을 가함
            // ForceMode : 지속적인 힘이냐, 순간적인 힘이냐, 질량을 사용하냐 무시하냐 차이
            // Impulse를 대부분 사용한다.
            
        }
    }
}
