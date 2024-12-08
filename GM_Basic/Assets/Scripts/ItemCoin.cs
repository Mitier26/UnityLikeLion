using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemCoin : MonoBehaviour
{
    public float rotateSpeed = 5f;

    private void Start()
    {
        rotateSpeed = Random.Range(100f, 250f);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }

}
