using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosSin : MonoBehaviour
{
    private float degree;
    public float radian;
    public float speed;

    private void OnDrawGizmos()
    {
        degree += Time.deltaTime * speed;

        Vector3 point1 = new Vector3(Mathf.Cos(degree * Mathf.Deg2Rad) * radian, 0, 0);
        Vector3 point2 = new Vector3(0, Mathf.Sin(degree * Mathf.Deg2Rad) * radian, 0);

        Gizmos.DrawLine(transform.position, point1);
        Gizmos.DrawLine(transform.position, point2);
        Gizmos.DrawLine(point1, point2);
    }
}
