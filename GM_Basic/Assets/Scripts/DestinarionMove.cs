using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinarionMove : MonoBehaviour
{
    private Vector3 target = new Vector3(8, 1.5f, 0);

    private void Update()
    {
        // LerpMove();
        SLerpMove();
    }

    void MoveTowardsMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, 2f);
    }

    void SmoothMove()
    {
        Vector3 velo = Vector3.zero;
        // 속도 추가 값인데 이것을 변경하면 목표 위치로 가지 않는다.
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velo, 0.1f);

    }

    void LerpMove()
    {
        transform.position = Vector3.Lerp(transform.position, target, 1f * Time.deltaTime);
    }

    void SLerpMove()
    {
        transform.position = Vector3.Slerp(transform.position, target, 1f * Time.deltaTime);
    }
}
