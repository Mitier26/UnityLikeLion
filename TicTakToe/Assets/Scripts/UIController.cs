using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject QuitPanel;
    
    public void ClosePanel()
    {
        QuitPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
