using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.PlayerLoop;

public class LineStudy : MonoBehaviour
{
    private LineRenderer lineRenderer;
    
    public Transform points;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // lineRenderer.positionCount = 3;

        // for (int i = 0; i < 3; i++)
        // {
        //     lineRenderer.SetPosition(i, new Vector3(i*5, 0, 0));
        // }

        // points = transform;
        //
        // for (int i = 0; i < transform.childCount; i++)
        // {
        //     lineRenderer.SetPosition(i, points.GetChild(i).position);
        // }
        lineRenderer.positionCount = 5; // 점 개수 설정

        // 각 인덱스에 점 좌표 설정
        lineRenderer.SetPosition(0, new Vector3(0, 0, 0)); // 시작 점
        lineRenderer.SetPosition(1, new Vector3(1, 1, 0));
        lineRenderer.SetPosition(2, new Vector3(2, 1, 0));
        lineRenderer.SetPosition(3, new Vector3(3, 0, 0));
        lineRenderer.SetPosition(4, new Vector3(4, -1, 0)); // 끝 점
        
    }

    void Update()
    {
        // for (int i = 0; i < transform.childCount; i++)
        // {
        //     lineRenderer.SetPosition(i, points.GetChild(i).position);
        // }
        
        float time = Time.time;
        lineRenderer.SetPosition(2, new Vector3(2, Mathf.Sin(time), 0)); // 중간 점을 시간에 따라 이동
    }
    
    
    
}
