using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarController : MonoBehaviour
{
    [SerializeField] private Image _hpGauge;
    [SerializeField] private bool isWorldCanvas;

    private void Update()
    {
        if (isWorldCanvas)
        {
            var cameraTransform = Camera.main.transform;
            transform.rotation = cameraTransform.rotation;
        }
        
    }

    public void SetHP(float hp)
    {
        _hpGauge.fillAmount = hp;
    }
}
