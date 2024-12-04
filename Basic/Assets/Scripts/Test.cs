using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private List<int> list = new List<int>();
    private void Start()
    {
        for (int i = 1; i <= 29; i++)
        {
            if (29 % i == 0)
            {
                list.Add(i);
            }
        }

        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log(list[i]);
        }
    }
}
