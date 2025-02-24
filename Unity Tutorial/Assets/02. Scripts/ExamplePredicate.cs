using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamplePredicate : MonoBehaviour
{
    public int level = 10;

    public Predicate<int> myPredicate;

    private void Start()
    {
        // 매개 변수가 1개이면 () 생략 가능
        myPredicate = (n) => n <= 10;
        
        // 아래 있는 것을 많이 줄였다.
        string msg = myPredicate(level) ? "초보자 사냥터 이용 가능" : "초보자 사냥터 이용 불가능";
        
        Debug.Log(msg);
        
        LevelCheck(level);
    }

    public void LevelCheck(int level)
    {
        if (level <= 10)
        {
            Debug.Log("초보자 사냥터 이용 가능");
        }
        else if (level > 10)
        {
            Debug.Log("초보자 사냥터 이용 불가능");
        }
    }
}
