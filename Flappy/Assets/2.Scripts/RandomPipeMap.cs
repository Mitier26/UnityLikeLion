using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPipeMap : MonoBehaviour
{
    public GameObject pipe;
    public float pipeSpeed = 5f;

    private void Update()
    {
        // 파이프의 위치를 오른쪽의 마이너스 ( 왼쪽 ) 방향으로 이동한다.
        pipe.transform.position -= Vector3.right * pipeSpeed * Time.deltaTime;
    }
}
