using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsRotationTarget : MonoBehaviour
{
    public Transform target;

    public float selfSpeed, aroundSpeed;

    private void Update()
    {
        // 자기 자신이 회전하는 기능 -> Rotate
        transform.Rotate(Vector3.up, selfSpeed * Time.deltaTime);

        // 다른 대상을 기존을 회전하는 기능 -> RotateAround
        transform.RotateAround(target.position, Vector3.up, aroundSpeed * Time.deltaTime);
    }
}
