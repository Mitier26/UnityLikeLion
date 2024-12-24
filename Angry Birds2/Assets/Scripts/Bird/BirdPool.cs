using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPool : MonoBehaviour
{
    public Bird birdPrefab;
    public BirdData[] birdDatas;
    public int birdCount = 3;
    private List<Bird> birdPool = new List<Bird>();

    private void Start()
    {
        foreach (BirdData data in birdDatas)
        {
            for (int i = 0; i < birdCount; i++)
            {
                Bird bird = Instantiate(birdPrefab, transform);
            }
        }
    }
}
