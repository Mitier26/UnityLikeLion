using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IfStudy : MonoBehaviour
{
    private int number1;
    private float number2;

    public int currentLevel = 1;

    private void Start()
    {
        if (currentLevel < 10)
        {
            Debug.Log("초보자 사녕터 사용 가능");
        }
        else if (currentLevel > 10)
        {
            Debug.Log("초보자 사냥터 X");
        }
        else
        {
            Debug.Log("현재 Level = 10");
        }
    }

    IfStudy(int number1, float number2)
    {
        this.number1 = number1;
        this.number2 = number2;
    }
}
