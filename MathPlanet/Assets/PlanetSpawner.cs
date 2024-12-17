using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public GameObject planetPrefab;
    public int count = 5;
    List<GameObject> planets = new List<GameObject>();

    private void Start()
    {
        Init();
    }

    void Init()
    {
        count = Random.Range(3, 20);
        for (int i = 0; i < count; i++)
        {
            GameObject newPlanet = Instantiate(planetPrefab);
            planets.Add(newPlanet);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (GameObject planet in planets) 
            { 
                Destroy(planet);
            }
            planets.Clear();
            Init();
        }
    }
}
