using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdGrab : MonoBehaviour
{
    public Bird bird;
    public SpriteRenderer spriteRenderer;
    public Transform[] strapStartPoints;
    public LineRenderer[] strapLines;
    public Transform centerPoint;
    public float maxDistance;
    public float power;
    public float height;

    public Bird readyBird;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        strapLines[0].SetPosition(0, strapStartPoints[0].position);
        strapLines[1].SetPosition(0, strapStartPoints[1].position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bird") && readyBird == null)
        {
            readyBird = other.GetComponent<Bird>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bird") && readyBird != null)
        {
            readyBird = null;
        }
    }

    public void AttachBird()
    {
        if (readyBird == null) return;

        if (bird != null)
        {
            bird.birdState = BirdState.Dragable;
            bird.ReturnPosition(bird);
            bird = readyBird;
            bird.transform.position = centerPoint.position;
            bird.birdState = BirdState.Attached;
        }
        else
        {
            bird = readyBird;
            bird.transform.position = centerPoint.position;
            bird.birdState = BirdState.Attached;
            readyBird = null;
        }
    }
}
