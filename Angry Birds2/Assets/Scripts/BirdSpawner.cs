using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;
using System;
using System.Collections.Generic;

public class BirdSpawner : MonoBehaviour
{
    public BirdPool birdPool;
    public Transform[] spawnPoints;
    private List<Bird> activeBirds = new List<Bird>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RemoveBirds();
            SpawnBirds();
        }
    }

    private void SpawnBirds()
    {
        HashSet<int> birdIds = new HashSet<int>();

        while (birdIds.Count < spawnPoints.Length)
        {   
            birdIds.Add(Random.Range(0, birdPool.poolSize));
        }

        int index = 0;
        
        foreach (int birdId in birdIds)
        {
            Bird bird = birdPool.GetBird(birdId);
            bird.transform.position = transform.position;
            bird.gameObject.SetActive(true);
            
            activeBirds.Add(bird);

            bird.transform.DOMove(spawnPoints[index].position, 1f).SetEase(Ease.OutBounce)
                .OnComplete(() => bird.Dragable());
            index++;
        }
    }

    private void RemoveBirds()
    {
        if (activeBirds == null) return;
        
        foreach (var bird in activeBirds)
        {
            birdPool.ReturnBird(bird);
        }
        
        activeBirds.Clear();
    }
}