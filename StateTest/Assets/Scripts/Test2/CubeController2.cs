using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum CubeState2
{
    Normal,
    Move,
    Scale,
}

public class CubeController2 : MonoBehaviour
{
    public TMP_Text text;
    private CubeState2 _cubeState2 = CubeState2.Normal;
    private Transform originalTransform;

    private void Start()
    {
        // 이거 참고 계속 변함
        // 각각의 속성 마다 변수를 만들어야 한다.
        originalTransform = transform;
        text.text = "";
    }

    // enum을 사용한 하드 코드 방식
    // State Pattern으로 변경하자.
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _cubeState2 = CubeState2.Normal;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _cubeState2 = CubeState2.Move;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _cubeState2 = CubeState2.Scale;
        }
        
        switch (_cubeState2)
        {
            case CubeState2.Normal:
                text.text = "Normal";
                transform.position = originalTransform.position;
                transform.rotation = originalTransform.rotation;
                transform.localScale = originalTransform.localScale;
                break;
            case CubeState2.Move:
                text.text = "Move";
                transform.position = new Vector3(Mathf.PingPong(Time.time , 1f), 0f, 0f);
                break;
            case CubeState2.Scale:
                text.text = "Scale";
                transform.localScale = Vector3.one * Mathf.PingPong(Time.time , 1f);
                break;
        }
    }
}
