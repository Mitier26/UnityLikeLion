using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyCharacter : MonoBehaviour
{
    public Transform child;
    public Transform target;

    public float checkDegree;
    public float checkDistance;

    private void OnDrawGizmos()
    {

        Vector3 direction = child.position - transform.position;
        direction.Normalize();

        Vector3 directionToTarget = target.transform.position - transform.position;
        float distance = directionToTarget.magnitude;
        directionToTarget.Normalize();

        float dot = Vector3.Dot(direction, directionToTarget);

        float angle = Mathf.Cos(checkDegree * 0.5f * Mathf.Deg2Rad);

        if (distance > checkDistance)
        {
            Gizmos.color = Color.red;
        }
        else if (dot > angle)
        {
            Gizmos.color = Color.blue;
        }
        else
        {
            Gizmos.color = Color.red;
        }

        // Debug.Log($"angle: {angle}, dot: {dot}");

        for (float i = -checkDegree / 2; i < checkDegree / 2; i++)
        {
            Vector3 dir = Quaternion.Euler(0, i, 0) * direction * checkDistance;
            Gizmos.DrawLine(transform.position, transform.position + dir);
        }
        
    }
}
