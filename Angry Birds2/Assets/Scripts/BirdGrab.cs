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

    private void Start()
    {
        strapLines[0].SetPosition(0, strapStartPoints[0].position);
        strapLines[1].SetPosition(0, strapStartPoints[1].position);
    }
}
