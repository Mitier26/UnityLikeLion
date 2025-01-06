using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTag : MonoBehaviour
{
    public string tag;

    private void Start()
    {
        if(!string.IsNullOrEmpty(tag))
            PlayerController.Instance.AddObserver(tag, gameObject);
    }
}
