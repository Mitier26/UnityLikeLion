using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanelController : PanelController
{
    public void OnClickSingPlayButton()
    {
        GameManager.Instance.StartGame();
        Hide();
    }

    public void OnClickDualPlayButton()
    {
        
    }

    public void OnClickSettingsButton()
    {
        
    }
}
