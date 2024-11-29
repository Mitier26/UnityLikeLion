using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomPipeMap : MonoBehaviour
{
    public enum PIPE_TYPE { TOP, BOTTOM, ALL };
    public PIPE_TYPE pipeType;
    
    public GameObject[] pipes;
    public float pipeSpeed = 5f;

    private void Start()
    {
        RandomPipeType();
    }

    private void Update()
    {
        // 파이프의 위치를 오른쪽의 마이너스 ( 왼쪽 ) 방향으로 이동한다.
        // transform.position -= Vector3.right * pipeSpeed * Time.deltaTime;
        // pipes[0].transform.position -= Vector3.right * pipeSpeed * Time.deltaTime;
        // pipes[1].transform.position -= Vector3.right * pipeSpeed * Time.deltaTime;
        
        // 위에 것을 아래 것으로 만듬.
        
        // 반복문을 이용해 코드를 간결하게 만들었다.
        foreach (GameObject pipe in pipes)
        {
            pipe.transform.position -= Vector3.right * pipeSpeed * Time.deltaTime;

            if (pipe.transform.position.x < -10f)
            {
                pipe.transform.position = new Vector3(10f, pipe.transform.position.y, pipe.transform.position.z);
                RandomPipeType();
            }
        }
        
        // 아래에 있는 것도 위에 것으로 만듬

        // if (pipes[0].transform.position.x <= -10)
        // {
        //     pipes[0].transform.position = new Vector3(10, pipes[0].transform.position.y, pipes[0].transform.position.z);
        // }
        // if (pipes[1].transform.position.x <= -10)
        // {
        //     pipes[1].transform.position = new Vector3(10, pipes[1].transform.position.y, pipes[1].transform.position.z);
        // }
        
        // if (transform.position.x <= -20f)
        // {
        //     // -3 ~ 3 : 높이
        //     transform.position += Vector3.right * 25f;
        //     RandomPipeType();
        // }
    }

    private void RandomPipeType()
    {
        int ranInt = Random.Range(0, 3);
        pipeType = (PIPE_TYPE)ranInt;
        // 현재의 타입을 설정
        if (pipeType == PIPE_TYPE.TOP)
        {
            pipes[0].SetActive(true);
            pipes[1].SetActive(false);
        }
        else if (pipeType == PIPE_TYPE.BOTTOM)
        {
            pipes[0].SetActive(false);
            pipes[1].SetActive(true);
        }
        else if (pipeType == PIPE_TYPE.ALL)
        {
            pipes[0].SetActive(true);
            pipes[1].SetActive(true);
        }

        float ranFloat = Random.Range(-3f, 3f);
        transform.position = new Vector3(transform.position.x, ranFloat, transform.position.z);
    }
}
