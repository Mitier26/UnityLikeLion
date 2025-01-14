using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JM_Singleton : MonoBehaviour
{
    private static JM_Singleton instance;

    public static JM_Singleton Instance
    {
        get
        {
            return instance;
        }
    }
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        
        instance = this;
    }
}
