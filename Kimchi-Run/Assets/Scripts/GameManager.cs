using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Intro,
    Playing,
    Dead
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State = GameState.Intro;

    public float PlayerStartTime;
    
    public int lives = 3;
    
    [Header("References")] 
    public GameObject IntroUI;

    public GameObject DeadUI;
    public GameObject BuildingSpawner;
    public GameObject EnemySpawner;
    public GameObject FoodSpawner;
    public GameObject GoldenSpawner;

    public Player PlayerScript;

    public TMP_Text scoreText;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        IntroUI.SetActive(true);
    }

    float CalculateScore()
    {
        return Time.time - PlayerStartTime;
    }

    void SaveHighScore()
    {
        int score = Mathf.FloorToInt(CalculateScore());
        int currentHighScore = PlayerPrefs.GetInt("highScore");
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
        }
    }

    int GetHighScore()
    {
        return PlayerPrefs.GetInt("highScore");
    }

    public float CalculateGameSpeed()
    {
        if (State != GameState.Playing)
        {
            return 5f;
        }

        float speed = 8f + (0.5f * Mathf.Floor(CalculateScore() / 10f));
        return Mathf.Min(speed, 30f);
    }
    
    private void Update()
    {
        if (State == GameState.Playing)
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(CalculateScore());
        }
        else if (State == GameState.Dead)
        {
            scoreText.text = "High Score : " + GetHighScore();
        }
        
        if (State == GameState.Intro && Input.GetKeyDown(KeyCode.Space))
        {
            State = GameState.Playing;
            IntroUI.SetActive(false);
            BuildingSpawner.SetActive(true);
            EnemySpawner.SetActive(true);
            FoodSpawner.SetActive(true);
            GoldenSpawner.SetActive(true);
            PlayerStartTime = Time.time;
        }

        if (State == GameState.Playing && lives == 0)
        {
            PlayerScript.KillPlayer();
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            GoldenSpawner.SetActive(false);
            DeadUI.SetActive(true);
            State = GameState.Dead;
        }

        if (State == GameState.Dead && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("main");
        }
    }
}
