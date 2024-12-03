using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PipeMovement : MonoBehaviour
{
    public float pipeSpeed = 4f;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, Random.Range(-2f, 2f), 0);
    }

    private void Update()
    {
        transform.Translate(Vector2.left * pipeSpeed * Time.deltaTime);

        if (transform.position.x < -10)
        {
            transform.position = new Vector3(10f, Random.Range(-2f, 2f), 0);
        }
    }
}
