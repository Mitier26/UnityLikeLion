using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private int[] array = new[] { -1,2 };
    private List<int> list = new List<int>();
    private int max = int.MinValue;
    private void Start()
    {
        
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = i+1; j < array.Length; j++)
            {
                if ((array[i] * array[j]) > max)
                {
                    max = array[i] * array[j];
                }
            }
        }
        
        Debug.Log(max);
    }
}
