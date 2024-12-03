using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    
    private float score = 0f;

    private bool isDeath = false;
    
    private float timer = 0f;
    public TMP_Text timeText;
    public TMP_Text scoreText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ShowScore();
    }

    private void LateUpdate()
    {
        Timer();
    }

    public void AddScore()
    {
        score += 6.7f;
        ShowScore();
    }

    private void ShowScore()
    {
        scoreText.text = $"점수 : {score}";
    }
    private void Timer()
    {
        timer += Time.deltaTime;

        timeText.text = timer.ToString("F1") + " sec";
    }

    public void PlayerDeath()
    {
        isDeath = true;
    }
}
