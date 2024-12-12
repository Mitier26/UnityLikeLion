// RaceButton.cs
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaceButton : MonoBehaviour
{
    public TMP_Text text;
    public RectTransform rect;
    public Button clickButton;
    
    // 생성자랑 같다.
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        clickButton = GetComponent<Button>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
}