using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public BirdSpawner birdSpawner;
    public Bird bird;
    public List<Block> blocks = new List<Block>();

    public GameState gameState;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        ChangeGameState(GameState.Ready);
    }

    private void Start()
    {
        InvokeRepeating(nameof(CheckAllBlockStopped), 0.5f, 0.5f);
    }

    public void GameStart()
    {
        birdSpawner.MakeBirds();
    }

    public void ChangeGameState(GameState newState)
    {
        if (gameState == newState) return;

        gameState = newState;

        switch (gameState)
        {
            case GameState.Ready:
                HandleReadyState();
                break;
            case GameState.Playing:
                HandlePlayingState();
                break;
            case GameState.Ended:
                HandleEndedState();
                break;
        }
    }

    private void HandleReadyState()
    {
        birdSpawner.MakeBirds();
    }

    private void HandlePlayingState()
    {
        
    }

    private void HandleEndedState()
    {
        CameraController.instance.StopFollowing();
        bird = null;
        birdSpawner.MakeBirds();
    }
    
    private float launchCooldown = 0.5f; // 발사 후 상태 체크를 지연하는 시간
    private float lastLaunchTime = -1f;
    
    private void CheckAllBlockStopped()
    {
        if (gameState != GameState.Playing || bird == null) return;

        // 발사 후 상태 체크 지연
        if (Time.time - lastLaunchTime < launchCooldown)
        {
            return;
        }

        // 모든 블록이 멈췄는지 확인
        bool allBlocksStopped = true;
        foreach (Block block in blocks)
        {
            if (block.isMoving)
            {
                allBlocksStopped = false;
                break;
            }
        }

        // 새가 멈췄는지 확인
        bool birdStopped = bird != null && bird.birdState == BirdState.Flying && !bird.isMoving;

        // 새와 블록 모두 멈춘 경우만 Ended로 전환
        if (allBlocksStopped && birdStopped)
        {
            ChangeGameState(GameState.Ended);
        }
    }

    
    public void OnBirdLaunch()
    {
        lastLaunchTime = Time.time; 
    }

    public void RegisterBird(Bird bird)
    {
        this.bird = bird;
    }

    public void UnregisterBird()
    {
        bird = null;
    }

    public void RegisterBlock(Block block)
    {
        if (!blocks.Contains(block))
        {
            blocks.Add(block);
        }
    }

    public void UnregisterBlock(Block block)
    {
        if (blocks.Contains(block))
        {
            blocks.Remove(block);
        }
    }
}
