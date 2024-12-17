using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitDrawer : MonoBehaviour
{
    [SerializeField] private float radiusX = 7f; // X축 반지름
    [SerializeField] private float radiusZ = 3f; // Z축 반지름
    [SerializeField] private int segments = 100; // 궤도 선의 세그먼트 수
    [SerializeField] private float speed = 2f;
    [SerializeField] private float tiltAngle = 30f; // 기울기 각도

    private Vector3 centerPoint;
    private LineRenderer lineRenderer;


    void Start()
    {
        Init();                 
    }

    void Init()
    {
        centerPoint = transform.position;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        lineRenderer.useWorldSpace = true;

        radiusX = Random.Range(1f, 20f);
        radiusZ = Random.Range(1f, 20f);
        speed = Random.Range(1f, 3f);
        tiltAngle = Random.Range(-90f, 90f);

        Material material = GetComponent<MeshRenderer>().material;

        material.EnableKeyword("_EMISSION");

        Color randomEmissionColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        //material.SetColor("_EmissionColor", randomEmissionColor);
        material.SetColor("_EmissionColor", randomEmissionColor);


        DrawOrbit();
    }

    void Update()
    {
        float angle = Time.time * speed;
        float x = centerPoint.x + radiusX * Mathf.Cos(angle);
        float z = centerPoint.z + radiusZ * Mathf.Sin(angle);
        float y = centerPoint.y + Mathf.Sin(angle) * Mathf.Tan(tiltAngle * Mathf.Deg2Rad);

        transform.position = new Vector3(x, y, z);
    }

    void DrawOrbit()
    {
        for (int i = 0; i <= segments; i++)
        {
            float angle = i * 2 * Mathf.PI / segments;
            float x = centerPoint.x + radiusX * Mathf.Cos(angle);
            float z = centerPoint.z + radiusZ * Mathf.Sin(angle);
            float y = centerPoint.y + Mathf.Sin(angle) * Mathf.Tan(tiltAngle * Mathf.Deg2Rad);

            lineRenderer.SetPosition(i, new Vector3(x, y, z));
        }
    }
}
