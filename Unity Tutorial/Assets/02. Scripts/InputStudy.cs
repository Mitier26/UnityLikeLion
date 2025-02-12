using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputStudy : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) // W 키
        {
            Debug.Log("W");
        }

        if (Input.GetKeyDown(KeyCode.S)) // S 키
        {
            Debug.Log("S");
        }

        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스키
        {
            Debug.Log("Space");
        }

        if (Input.GetKeyDown(KeyCode.Return)) // 엔터키
        {
            Debug.Log("Return");
        }

        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼을 누르기 시작
        {
            Debug.Log("마우스 왼쪽 버튼을 누르기 시작");
        }

        if (Input.GetMouseButton(0))  // 마우스 왼쪽 버튼을 누르는 중
        {
            Debug.Log("마우스 왼쪽 버튼을 누르는 중");
        }
    }
}