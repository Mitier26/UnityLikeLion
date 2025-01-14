using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBody : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPosition = (mousePosition-transform.position).normalized;
        
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, targetPosition);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
    }
}
