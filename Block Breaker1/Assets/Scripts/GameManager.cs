using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Ball ball;
    
    private int blockCount;
    private int score = 0;
    private int combo = 0;
    
    public bool isGameStarted = false;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void InitGame()
    {
        isGameStarted = false;
        ball.ResetBall();
        combo = 0;
    }
    
    public void OnBlockSpwned()
    {
        blockCount++;
        
        UIManager.instance.UpdateBlockCount(blockCount);
        
    }

    public void OnBlockDestroyed()
    {
        blockCount = Mathf.Max(0, blockCount - 1);
        
        combo++;
        score += 100 * combo;
        UIManager.instance.UpdateScore(score);
        UIManager.instance.UpdateCombo(combo);
        UIManager.instance.UpdateBlockCount(blockCount);
        if (blockCount <= 0)
        {
            // game end
        }
    }
}
