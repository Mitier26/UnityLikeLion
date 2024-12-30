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

    public void ChangeGameState(GameState newState)
    {
        if (gameState == newState) return;

        gameState = newState;
        Debug.Log($"GameManager: GameState changed to {newState}");

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
    }

    private void CheckAllBlockStopped()
    {
        if (gameState != GameState.Playing) return;

        bool allBlocksStopped = true;
        bool birdStopped = bird == null || (!bird.isMoving && bird.birdState == BirdState.Flying);

        foreach (Block block in blocks)
        {
            if (block.isMoving)
            {
                allBlocksStopped = false;
                break;
            }
        }

        if (allBlocksStopped && birdStopped)
        {
            ChangeGameState(GameState.Ended);
        }
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
