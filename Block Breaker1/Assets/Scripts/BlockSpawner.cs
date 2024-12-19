using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public int rows = 5;
    public int cols = 5;
    public Vector2 startPosition;
    public float spacingX = 1f;
    public float spacingY = 1f;
    public GameObject blockPrefab;
    
    private void Start()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Vector2 spawnPosition = startPosition + new Vector2(col * spacingX, -row * spacingY);
                Instantiate(blockPrefab, spawnPosition, Quaternion.identity);
                GameManager.instance.OnBlockSpwned();
            }
        }
    }
    
    // 왼쪽 부터 생성하는 것
    void SpawnBlocksLeftToRight(Vector2 startPosition, int rows, int cols, float spacing)
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Vector2 spawnPosition = startPosition + new Vector2(col * spacing, -row * spacing);
                Instantiate(blockPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
    
    // 중앙 부터 생성하는 것
    void SpawnBlocksCenterOut(Vector2 centerPosition, int rows, int cols, float spacing)
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                float offsetX = (col - (cols / 2)) * spacing;
                Vector2 spawnPosition = centerPosition + new Vector2(offsetX, -row * spacing);
                Instantiate(blockPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
