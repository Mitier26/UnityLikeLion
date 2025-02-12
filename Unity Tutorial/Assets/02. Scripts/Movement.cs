using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float h = 0f;
    private float v = 0f;
    public float rotSpeed = 30f;
    public float jumpPower = 10f; // 점프 파워
    public Transform target;
    public bool isGround = false;

    public Animator anim; // Animator 타입의 anim 변수 선언

    void Update()
    {
        Move(); // Move 함수 호출(실행)
        Jump(); // Jump 함수 호출(실행)
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true) // 스페이스바를 눌렀을 때
        {
            // 자기 자신의 Rigidbody라고하는 Component 접근 / AddForce로 힘을 가함(위쪽으로 jumpPower만큼 힘을 가한다.)
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    private void Move() // 캐릭터 이동 관련 기능
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0f, v).normalized;

        transform.position += dir * Time.deltaTime * moveSpeed;

        if (h == 0 && v == 0) // 아무런 키도 누르지 X
        {
            // 걷는 애니메이션 실행 X
            anim.SetBool("isWalk", false);
        }
        else // 이동키 중 어떤 키라도 눌렀을 경우
        {
            // 걷는 애니메이션 실행 O
            anim.SetBool("isWalk", true);
        }

        if (h != 0 || v != 0)
        {
            Vector3 targetPos = transform.position + dir;
            transform.LookAt(targetPos);
        }
    }
}