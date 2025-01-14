using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySapwner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 20;
    public Transform enemyParent;
    
    private void Start()
    {
        int spawnedBoxes = 0;

        while (spawnedBoxes < enemyCount)
        {
            float randomX = Random.Range(-40f, 40f);
            float randomY = Random.Range(-40f, 40f);

            if (Mathf.Abs(randomX) <= 5f && Mathf.Abs(randomY) <= 5f)
            {
                continue;
            }

            GameObject enemy = Instantiate(enemyPrefab, enemyParent);
            enemy.transform.position = new Vector2(randomX, randomY);
            spawnedBoxes++;
        }
    }
}
