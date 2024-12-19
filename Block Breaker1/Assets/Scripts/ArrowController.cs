using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArrowController : MonoBehaviour
{
    public float rotationSpeed = 150f;
    
    public int direction = 1;
    private void OnEnable()
    {
        RandomDirection();
        RandomSpeed();
    }

    private void Start()
    {
        RandomDirection();
        RandomSpeed();
    }

    void RandomDirection()
    {
        direction = Random.Range(-1, 2);
        if(direction == 0) direction = 1;
    }

    void RandomSpeed()
    {
        rotationSpeed = Random.Range(50f, 150f);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime * direction);
    }

    public Vector3 GetDirection()
    {
        return transform.up;
    }
}
