using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BirdGrab : MonoBehaviour
{
    public Bird bird;
    public SpriteRenderer spriteRenderer;
    public Transform[] strapStartPoints;
    public LineRenderer[] strapLines;
    public Transform centerPoint;
    public float maxDistance = 100f;
    public float power = 10f;
    public float height = 1f;
    public bool isAttached = false;

    public Bird readyBird;

    public LineRenderer trajectoryLine;
    public int trajectorySegments = 20;
    public float maxTrajectoryDistance = 5f;
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        strapLines[0].SetPosition(0, strapStartPoints[0].position);
        strapLines[1].SetPosition(0, strapStartPoints[1].position);
        strapLines[0].enabled = false;
        strapLines[1].enabled = false;
    }


    private void OnMouseDrag()
    {
        if (bird == null) return;
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = mousePosition - transform.position;

        if (direction.magnitude > maxDistance)
        {
            direction = direction.normalized * maxDistance;
        }
        bird.transform.position = centerPoint.position + direction;
        
        strapLines[0].SetPosition(1, bird.transform.position);
        strapLines[1].SetPosition(1, bird.transform.position);
        
        UpdateTrajectory();
    }

    private void OnMouseUp()
    {
        Lanch();
    }

    private void Lanch()
    {
        if (bird == null) return;
        
        isAttached = false;

        Vector3 launchVelocity = (centerPoint.position - bird.transform.position) * power;
        
        bird.LaunchBird(launchVelocity);

        strapLines[0].enabled = false;
        strapLines[1].enabled = false;
        trajectoryLine.enabled = false;
        
        bird = null;
        trajectoryLine.positionCount = 0;
    }
    
    private void UpdateTrajectory()
    {
        trajectoryLine.enabled = true;
        Vector3 velocity = (centerPoint.position - bird.transform.position) * power;
        Vector3 currentPosition = bird.transform.position;

        trajectoryLine.positionCount = trajectorySegments;
        for (int i = 0; i < trajectorySegments; i++)
        {
            float time = i * (maxTrajectoryDistance / trajectorySegments);
            Vector3 point = currentPosition + velocity * time + (Vector3)(0.5f * Physics2D.gravity * time * time);
            trajectoryLine.SetPosition(i, point);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bird") && readyBird == null && other.GetComponent<Bird>().birdState == BirdState.Dragable)
        {
            readyBird = other.GetComponent<Bird>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bird") && readyBird != null && other.GetComponent<Bird>().birdState == BirdState.Dragable)
        {
            readyBird = null;
        }
    }

    public void AttachBird()
    {
        if (readyBird == null) return;

        if (bird != null)
        {
            bird.ReturnPosition();
        }
        
        bird = readyBird;
        bird.AttachBird(centerPoint.position);
        readyBird = null;
        
        isAttached = true;
        strapLines[0].SetPosition(1, transform.position);
        strapLines[1].SetPosition(1, transform.position);
        strapLines[0].enabled = true;
        strapLines[1].enabled = true;
    }
}
