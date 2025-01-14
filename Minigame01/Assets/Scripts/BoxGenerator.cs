using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoxGenerator : MonoBehaviour
{
    public GameObject[] boxes;
    public int boxCount = 20;
    public Transform boxParent;
    
    private void Start()
    {
        int spawnedBoxes = 0;

        while (spawnedBoxes < boxCount)
        {
            float randomX = Random.Range(-40f, 40f);
            float randomY = Random.Range(-40f, 40f);

            if (Mathf.Abs(randomX) <= 5f && Mathf.Abs(randomY) <= 5f)
            {
                continue;
            }

            GameObject box = Instantiate(boxes[Random.Range(0, boxes.Length)], boxParent);
            box.transform.position = new Vector2(randomX, randomY);
            spawnedBoxes++;
        }
    }
}
