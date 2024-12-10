
using System;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public string my_string = "hi12392";
    private void Start()
    {
        char[] charArray = my_string.ToCharArray();
        List<int> intList = new List<int>();
        
        for (int i = 0; i < charArray.Length; i++)
        {
            Debug.Log((int)(charArray[i]));
            // int로 변경했을 때 48 = 0; 57 = 9
            // 그냥 해도 가능
            if ((charArray[i]) >= '0' && charArray[i]<= '9')
            {
                intList.Add(charArray[i] - '0');
                // 문자를 숫자로 바꾸는 방법
            }
        }

        intList.Sort();
        
        int[] answer = intList.ToArray();

        for (int i = 0; i < answer.Length; i++)
        {
            Debug.Log(answer[i]);
        }
        
        // for 문을 이용해 만들었다.
        // 분명 더 쉬운 방법이 있을 것이다.
        // 리스트에서 숫자만 찾는 방법

    }
}
