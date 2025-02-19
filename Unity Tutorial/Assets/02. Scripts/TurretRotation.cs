using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    public Transform target;
    
    public float rotationSpeed= 3f;
    private float theta;
    
    public float radius = 15f;
    public float angle = 120f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
        }
    }

    private void Update()
    {
        Rotation();
        SetTarget();
    }

    private void SetTarget()
    {
        if (target == null) return;
        
        // 터렛과 타겟과의 거리
        float distance = Vector3.Distance(this.transform.position, target.position);
        if (distance <= radius)
        {
            Vector3 targetDir = (target.position - this.transform.position).normalized;

            // 터렛의 덩면과 타겟의 각도
            float targetAngle = Vector3.Angle(this.transform.forward, targetDir);

            if (targetAngle <= angle / 2)
            {
                Debug.DrawRay(this.transform.position, targetDir * radius, Color.green);

                this.transform.LookAt(target);
            }
            else
            {
                Debug.DrawRay(this.transform.position, targetDir * radius, Color.red);
            }
        }
    }
    
    private void Rotation()
    {
        theta += Time.deltaTime * rotationSpeed;
        this.transform.localRotation = Quaternion.Euler(Vector3.up * 45f * Mathf.Sin((theta)));

        Vector3 vacA = new Vector3(0, 3, 5);
        Vector3 vacB = new Vector3(7, 2, 1);
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, radius);

        Vector3 leftBoundary = Quaternion.Euler(0, -angle / 2, 0) * this.transform.forward * radius;
        Vector3 rightBoundary = Quaternion.Euler(0, angle / 2, 0) * this.transform.forward * radius;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(this.transform.position, this.transform.position + leftBoundary);
        Gizmos.DrawLine(this.transform.position, this.transform.position + rightBoundary);
    }
}
