using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(AudioSource))]
public class SwitchController : MonoBehaviour
{
    [SerializeField] private Image handleImage;
    [SerializeField] private AudioClip clickSound;
    
    public delegate void OnSwitchChangedDelegate(bool isOn);
    public OnSwitchChangedDelegate onSwitchChanged;

    private bool _isOn;
    
    private static readonly Color32 OnColor = new Color32(50, 230, 120, 255);
    private static readonly Color32 OffColor = new Color32(70, 90, 120, 255);

    private RectTransform _handleRectTransform;
    private Image _backgroundImage;
    private AudioSource _audioSource;

    private void Awake()
    {
        _handleRectTransform = handleImage.GetComponent<RectTransform>();
        _backgroundImage = GetComponent<Image>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _handleRectTransform.anchoredPosition = new (-14, 0);
        _backgroundImage.color = OffColor;
        _isOn = false;
    }

    private void SetOn(bool isOn)
    {
        if (isOn)
        {
            _handleRectTransform.DOAnchorPosX(14, 0.2f);
            _backgroundImage.DOBlendableColor(OnColor, 0.2f);
        }
        else
        {
            _handleRectTransform.DOAnchorPosX(-14, 0.2f);
            _backgroundImage.DOBlendableColor(OffColor, 0.2f);
        }
        
        onSwitchChanged?.Invoke(isOn);
        _isOn = isOn;
    }

    public void OnClickSwitch()
    {
        _audioSource.PlayOneShot(clickSound);
        SetOn(!_isOn);
    }
}
