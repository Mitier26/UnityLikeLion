using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private void Start()
    {
        transform.Translate(Vector3.right * 3);
    }

    private void Update()
    {
        // transform.Translate(Vector3.left * 3 * Time.deltaTime);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dis = new Vector3(h, 0, v);
        dis.Normalize();
        
        transform.Translate(dis * 5f * Time.deltaTime);


    }
}
