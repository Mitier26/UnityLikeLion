using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSingle : SingletonMonoBehaviour<GameManagerSingle>
{
    public enum GameType{INTRO, BUILD, PLAY, OUTRO}
    public GameType e_GameType;
    
    protected override void Awake()
    {
        base.Awake();
    }
}
