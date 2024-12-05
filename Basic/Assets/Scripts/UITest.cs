using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
    public RectTransform buttonRect;
    public Vector2 targetPosition;
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        buttonRect.anchoredPosition += new Vector2(x, y);
    }
}
