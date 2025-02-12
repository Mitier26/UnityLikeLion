using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTarget : MonoBehaviour
{
    public Transform target;

    //public float rSpeed, raSpeed = 0f;
    public float rotSpeed = 0f;
    public float rotAroundSpeed = 0f;

    void Update()
    {
        // 자전
        // 자기 자신이 회전하는 기능 -> Rotate
        transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);

        // 공전
        // 다른 대상을 기준으로 회전하는 기능 -> RotateAround
        transform.RotateAround(target.position, Vector3.up, rotAroundSpeed * Time.deltaTime);
    }
}