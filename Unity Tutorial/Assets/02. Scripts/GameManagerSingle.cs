using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSingle : SingletonMonoBehaviour<GameManagerSingle>
{
    public enum GameType{INTRO, BUILD, PLAY, OUTRO}
    public GameType e_GameType;

    #region Manager

    private BoardManager _boardManager;
    public BoardManager boardManager
    {
        get
        {
            if (_boardManager == null)
                _boardManager = FindObjectOfType<BoardManager>(true);
            
            return _boardManager;
        }
    }
    
    private UIManager _uiManager;

    public UIManager uIManager
    {
        get
        {
            if(_uiManager == null)
                _uiManager = FindObjectOfType<UIManager>(true);
            
            return _uiManager;
        }
    }
    
    #endregion
    
    protected override void Awake()
    {
        base.Awake();
    }

    private Action gameManagerAction;
    private void Start()
    {
        boardManager.CreateBorad();
        
        // uIManager.FadeEvent();
        // uIManager.FadeEvent(gameManagerAction);
        // uIManager.FadeEvent(gameManagerAction, 2, 2);
    }

    private void Update()
    {
        switch (e_GameType)
        {
            case GameType.INTRO:
                break;
            case GameType.BUILD:
                break;
            case GameType.PLAY:
                break;
            case GameType.OUTRO:
                break;
        }
    }

    public void OnChangeType(GameType gameType)
    {
        if (e_GameType != gameType) 
            e_GameType = gameType;
    }
}
