using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomPipeMap : MonoBehaviour
{
    public enum PIPE_TYPE { TOP, BOTTOM, ALL }
    public PIPE_TYPE pipeType;

    // public GameObject pipe;
    public GameObject[] pipes; // GameObject 배열
    public float pipeSpeed = 10f;

    void Start()
    {
        RandomPipeType(); // 처음 시작할 때 파이프 타입을 설정!!
    }

    void Update()
    {
        foreach (GameObject pipe in pipes)
        {
            pipe.transform.position -= Vector3.right * pipeSpeed * Time.deltaTime;

            if (pipe.transform.position.x <= -10f)
            {
                RandomPipeType();
                pipe.transform.position = new Vector3(10f, pipe.transform.position.y, pipe.transform.position.z);
            }
        }
    }

    private void RandomPipeType() // 파이프 재설정 기능
    {
        int ranInt = Random.Range(0, 3); // 0, 1, 2
        pipeType = (PIPE_TYPE)ranInt; // 형변환(캐스팅)

        if (pipeType == PIPE_TYPE.TOP) // 파이프 타입이 TOP일 경우
        {
            pipes[0].SetActive(true);
            pipes[1].SetActive(false);
        }
        else if (pipeType == PIPE_TYPE.BOTTOM) // 파이프 타입이 BOTTOM일 경우
        {
            pipes[0].SetActive(false);
            pipes[1].SetActive(true);
        }
        else if (pipeType == PIPE_TYPE.ALL) // 파이프 타입이 ALL일 경우
        {
            pipes[0].SetActive(true);
            pipes[1].SetActive(true);
        }

        // 높이를 랜덤하게 바꾸는 기능
        float ranFloat = Random.Range(-3f, 2f); // float 타입의 랜덤 값

        transform.position = new Vector3(transform.position.x, ranFloat, transform.position.z);
    }
}