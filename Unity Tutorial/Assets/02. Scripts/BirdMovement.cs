using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public SoundManager soundManager; // SoundManager 타입의 변수

    private Rigidbody2D myRigid;
    public float flyPower = 10f; // 날아오르는 힘

    public float limitPower = 5f; // 제한 속도

    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>(); // 자기 자신의 Rigidbody2D를 할당
    }

    // 키보드 스페이스바를 눌러서
    // 새가 날아오르는 기능
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바 눌렀을 때 조건문
        {
            soundManager.OnJumpSound();

            myRigid.AddForce(Vector2.up * flyPower, ForceMode2D.Impulse); // 위쪽으로 힘을 가함

            // 속도 체크 및 제한
            if (myRigid.velocity.y > limitPower) // 새의 y축 속도가 제한 속도보다 커졌는지 확인하는 조건문
            {
                // 속도를 재조정
                myRigid.velocity = new Vector2(myRigid.velocity.x, limitPower);
            }
        }

        var playerEulerAngle = transform.eulerAngles;
        playerEulerAngle.z = myRigid.velocity.y * 5f;

        transform.eulerAngles = playerEulerAngle;
    }
}