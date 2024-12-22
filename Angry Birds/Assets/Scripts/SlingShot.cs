using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlingShot : MonoBehaviour
{
    public LineRenderer[] lineRenderes;
    public Transform[] startLinePositions;
    public Transform idlePosition;
    public Transform center;

    public Vector3 currentPosition;

    public float maxLength;
    public float bottomBoundary;
    
    private bool isMouseDown = false;
    
    public GameObject birdPrefab;

    public float birdPositionOffset;
    
    private Rigidbody2D bird;
    Collider2D birdCollider;

    public float force;
    
    private void Start()
    {
        lineRenderes[0].SetPosition(0, startLinePositions[0].position);
        lineRenderes[1].SetPosition(0, startLinePositions[1].position);
        
        CreateBird();
    }

    void CreateBird()
    {
        bird = Instantiate(birdPrefab).GetComponent<Rigidbody2D>();
        birdCollider = bird.GetComponent<Collider2D>();
        birdCollider.enabled = false;
        
        bird.isKinematic = true;
        
        ResetStrips();
    }

    private void Update()
    {
        if (isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f;
            
            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition - center.position, maxLength);
            
            currentPosition = ClampBoundary(currentPosition);
            
            SetStrips(currentPosition);

            if (birdCollider)
            {
                birdCollider.enabled = true;
            }
        }
        else
        {
            ResetStrips();
        }
    }

    void OnMouseDown()
    {
        isMouseDown = true;
    }

    void OnMouseUp()
    {
        isMouseDown = false;
        Shoot();
    }

    void Shoot()
    {
        bird.isKinematic = false;
        Vector3 birdForce = (currentPosition - center.position) * force * -1;
        bird.velocity = birdForce;

        bird = null;
        birdCollider = null;
        
        Invoke("CreateBird", 2f);
    }

    void ResetStrips()
    {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
    }
    
    void SetStrips(Vector3 position)
    {
        lineRenderes[0].SetPosition(1, position);
        lineRenderes[1].SetPosition(1, position);

        if (bird)
        {
            Vector3 dir = position - center.position;
            bird.transform.position = position + dir.normalized * birdPositionOffset;
            bird.transform.right = -dir.normalized;
        }

    }

    Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, bottomBoundary, 1000);
        return vector;
    }

}
