using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfStudy : MonoBehaviour
{
    public int currentLevel = 1; // 현재 레벨 변수 = 1

    void Start()
    {
        if (currentLevel > 0 && currentLevel < 10) // 현재 레벨이 10보다 작으면, 초보자 사냥 사용 가능
        {
            Debug.Log("초보자 사냥터 O");
        }
        else if (currentLevel > 10) // 현재 레벨이 10보다 크면, 초보자 사냥 사용 불가능
        {
            Debug.Log("초보자 사냥터 X");
        }
        else // currentLevel == 10일 경우
        {
            Debug.Log("레벨이 10이므로, 초보자 사냥터 O");
        }
    }

    void Update()
    {
        
    }
}
