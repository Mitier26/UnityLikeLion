using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputStudy : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("D");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space");
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Return");
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("마우스 왼쪽 클릭 시작");
        }
        if (Input.GetMouseButton(0))
        {
            Debug.Log("마우스 왼쪽 클릭 중");
        }
    }
}
