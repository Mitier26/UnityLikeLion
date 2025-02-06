using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class PanelController : MonoBehaviour
{
    [SerializeField] private RectTransform panelRectTransform;
    
    private CanvasGroup _backgroundCanvasGroup;
    
    public delegate void PanelControllerHideDelegate();

    private void Awake()
    {
        _backgroundCanvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Panel 표시 함수
    /// </summary>
    public void Show()
    {
        _backgroundCanvasGroup.alpha = 0;
        panelRectTransform.localScale = Vector3.zero;

        _backgroundCanvasGroup.DOFade(1f, 0.5f).SetEase(Ease.OutBack);
        panelRectTransform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
    }

    /// <summary>
    /// Panel 숨기기 함수
    /// </summary>
    public void Hide(PanelControllerHideDelegate hideDelegate = null)
    {
        _backgroundCanvasGroup.alpha = 1;
        panelRectTransform.localScale = Vector3.one;
        
        _backgroundCanvasGroup.DOFade(0f, 0.3f).SetEase(Ease.InBack);
        panelRectTransform.DOScale(0f, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            hideDelegate?.Invoke();
            Destroy(gameObject);
        });
    }
}
