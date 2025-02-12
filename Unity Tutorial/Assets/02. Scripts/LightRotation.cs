using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotation : MonoBehaviour
{
    public float rotSpeed = 10f;

    void Update()
    {
        transform.Rotate(Vector3.right * rotSpeed * Time.deltaTime);
    }
}