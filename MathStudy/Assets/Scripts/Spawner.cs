using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spanwer : MonoBehaviour
{
    public GameObject ball;
    public Transform ballInitTransform;
    public LineRenderer lineRenderer;
    
    void Start()
    {
        SpawnBall(ballInitTransform.position);
    }
    
    public void SpawnBall(Vector3 position)
    {   
        GameObject go = Instantiate(ball, position, Quaternion.identity);
        go.GetComponent<DraggableObject>().lineRenderer = lineRenderer;
        go.GetComponent<DraggableObject>().OnDestroyed += () => SpawnBall(position);
        go.GetComponent<DraggableObject>().OnFly += () => lineRenderer.SetPosition(1, position);
        
        Destroy(go, 3f);
    }
}