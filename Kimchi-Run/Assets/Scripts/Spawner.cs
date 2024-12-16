using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("Setting")] 
    public float minSpawnDelay;
    public float maxSpawnDelay;
    
    [Header("References")] public GameObject[] gameObjects;

    private void OnEnable()
    {
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Spawn()
    {
        GameObject randomObject = gameObjects[Random.Range(0, gameObjects.Length)];
        Instantiate(randomObject, transform.position, quaternion.identity);
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }
}
