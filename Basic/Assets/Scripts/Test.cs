
using System;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        int r = (int)Math.Floor((double)10 / 3);
        int c = (int)Math.Floor((double)8 / 3);
        int h = (int)Math.Floor((double)6 / 3);
        
        Debug.Log(r);
        Debug.Log(c);
        Debug.Log(h);
        int answer = (r * c) * h;
        
        Debug.Log(answer);
        
    }
}
