using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int enemyCount = 30;
    public float enemyMoveTime = 30f;
    public float currentTime = 0f;
    public bool isplaying = false;

    public TMP_Text timerText;
    public TMP_Text enemyCountText;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        timerText.text = currentTime.ToString("F1");
        enemyCountText.text = enemyCount.ToString();
    }

    private void Update()
    {
        if (!isplaying) return;
        
        currentTime += Time.deltaTime;
        timerText.text = currentTime.ToString("F1");
    }

    public void DiscountEnemy()
    {
        enemyCount--;
        enemyCountText.text = enemyCount.ToString();
        if (enemyCount <= 0)
        {
            
        }
    }

    public void PlayGame()
    {
        isplaying = true;
        
    }
}
