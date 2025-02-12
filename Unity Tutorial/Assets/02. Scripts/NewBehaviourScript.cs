using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("NewBehaviourScript - Awake");
    }

    void OnEnable()
    {
        Debug.Log("NewBehaviourScript - OnEnable");
    }

    void Start()
    {
        Debug.Log("NewBehaviourScript - Start");
    }

}