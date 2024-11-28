using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotation : MonoBehaviour
{
    public Transform directionalLight;
    public float rotationSpeed = 10f;

    private void Update()
    {
        directionalLight.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}
