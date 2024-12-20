using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosSin : MonoBehaviour
{
    private float degree;
    public float radius;
    public float speed;

    private void OnDrawGizmos()
    {
        degree += Time.deltaTime * speed;

        Vector3 point1 = new Vector3(Mathf.Cos(degree * Mathf.Deg2Rad) * radius, 0, 0);
        Vector3 point2 = new Vector3(0, Mathf.Sin(degree * Mathf.Deg2Rad) * radius, 0);

        Gizmos.DrawLine(transform.position, point1);
        Gizmos.DrawLine(transform.position, point2);
        Gizmos.DrawLine(point1, point2);
        
        Vector3 point3 = new Vector3(
            Mathf.Cos(degree * Mathf.Deg2Rad) * radius, Mathf.Sin(degree * Mathf.Deg2Rad) * radius , 0);
        Vector3 point4 = new Vector3(0,
            Mathf.Sin(degree * Mathf.Deg2Rad) * radius, 0);
        Gizmos.DrawLine(transform.position, point3);
        Gizmos.DrawLine(transform.position, point4);
        Gizmos.DrawLine(point3, point4);
    }
    
}
