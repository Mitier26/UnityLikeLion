using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollider : MonoBehaviour
{
    public Vector3 direction;
    public float speed;

    private void Update()
    {
        transform.position += direction * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 point = other.ClosestPoint(transform.position);
        
        // 외적
        Vector3 normal1 = Vector3.Cross(other.transform.forward, transform.right);
        float dot = Vector3.Dot(normal1, -direction);
        Vector3 p = normal1 * dot;
        direction = direction + p * 2;
        
        // p 1 번은 평면을 따라 슬라이딩 함
        // p 2 번은 반대로 반사됨
        Debug.Log("OnTriggerEnter");
    }
}
