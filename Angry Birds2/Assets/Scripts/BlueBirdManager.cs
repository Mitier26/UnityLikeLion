using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBirdManager : MonoBehaviour
{
    public static BlueBirdManager Instance;
    
    public GameObject[] blueBirds;
    public Queue<GameObject> blueBirdQueue = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        foreach (GameObject blueBird in blueBirds)
        {
            blueBird.SetActive(false);
            blueBirdQueue.Enqueue(blueBird);
        }
    }

    public GameObject GetBlueBird()
    {
        if (blueBirdQueue.Count > 0)
        {
            GameObject blueBird = blueBirdQueue.Dequeue();
            blueBird.SetActive(true);
            return blueBird;
        }
        return null;
    }

    public void ReturnBlueBird(GameObject blueBird)
    {
        blueBird.SetActive(false);
        blueBirdQueue.Enqueue(blueBird);
    }
}
