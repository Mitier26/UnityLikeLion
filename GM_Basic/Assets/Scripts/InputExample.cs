using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputExample : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("플레이어가 아무 키를 눌렀습니다.");
        }

        if (Input.anyKey)
        {
            Debug.Log("아무키나 누르고 있습니다.");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("아이템을 구입했습니다");
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("왼쪽으로 이동 중");
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            Debug.Log("오른쪽 이동을 멈추었습니다.");
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("미사일 발사");
        }

        if (Input.GetMouseButton(0))
        {
            Debug.Log("미사일 모으는 중...");
        }

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("슈퍼 미사일 발사!!"); 
        }

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Input Manager를 이용한 입력!!");
        }

        if (Input.GetButton("Horizontal"))
        {
            Debug.Log("횡 이동 중...." + Input.GetAxis(("Horizontal")));
            // GetAxis : 가중치가 있다. 한번에 1이 되는 거이 아니야!
        }
        
    }
}
