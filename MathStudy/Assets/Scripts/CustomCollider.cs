using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollider : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public Rigidbody rigidbody;

    private void Start()
    {
        rigidbody.velocity = direction;
    }

    private void Update()
    {
        transform.position += direction * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Physics.Raycast(transform.position, rigidbody.velocity, out RaycastHit hit,
                1 << LayerMask.NameToLayer("Blockable")))
        {
            Vector3 hitVector = (hit.point - transform.position).normalized;
            
            Vector3 vector3 = rigidbody.velocity; 
            vector3.y = -rigidbody.velocity.y;
            rigidbody.velocity = hitVector * 2;

            // rigidbody.velocity = -rigidbody.velocity + hit.normal * 2;
            
            // 이런거 쓰지 말고 OnCollisionEnter를 사용하자!!
            // 직접 구현하는 것은 좋지만 더 좋은 것이 있다.
        }
    }

    void custom(Collider other)
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
