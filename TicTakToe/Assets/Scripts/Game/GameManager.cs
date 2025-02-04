using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private BlockController blockController;
    public enum PlayerType { None, PlayerA ,PlayerB }
    
    private PlayerType[,] _board;      // 틱택토 게임판의 정보를 담는 것

    private void Start()
    {
        // 게임 초기화
        InitGame();
        
        // 테스트 코드
        blockController.OnBlockClickedDelegate = (row, col) =>
        {
            Debug.Log("Row : " + row + ", Col : " + col);
        };
    }

    public void InitGame()
    {
        // 보드 초기화
        _board = new PlayerType[3, 3];
        
        // 블록 초기화
        blockController.InitBlocks();
    }
    
    
    
}
