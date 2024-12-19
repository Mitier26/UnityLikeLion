using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TMP_Text startText;
    public TMP_Text blockText;
    public TMP_Text scoreText;
    public TMP_Text comboText;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ShowStartText()
    {
        startText.gameObject.SetActive(true);
    }

    public void HideStartText()
    {
        startText.gameObject.SetActive(false);
    }

    public void UpdateBlockCount(int count)
    {
        blockText.text = count.ToString();
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void UpdateCombo(int combo)
    {
        comboText.text = combo.ToString();
    }
}
