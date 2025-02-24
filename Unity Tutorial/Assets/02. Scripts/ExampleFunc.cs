using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleFunc : MonoBehaviour
{
    public Func<int, int, int> myuFunc;
    // 앞에 2개는 매개변수, 마지막만 반환값
    
    public List<Func<int, int,int>> calculations = new List<Func<int, int, int>>();

    private void Start()
    {
        myuFunc = AddFunction;
        
        int result = myuFunc(10, 20);
        
        Debug.Log(result);
        
        calculations.Add(AddFunction);
        calculations.Add(MinusFunction);

        foreach (var func in calculations)
        {
            int result2 = func(10, 20);
            Debug.Log(result2);
        }
        
        // 람다식
        calculations.Add((num1, num2) => num1 * num2);
        calculations.Add((num1, num2) => num1 / num2);
    }

    public int AddFunction(int num1, int num2)
    {
        return num1 + num2;
    }
    
    public int MinusFunction(int num1, int num2)
    {
        return num1 - num2;
    }
}
