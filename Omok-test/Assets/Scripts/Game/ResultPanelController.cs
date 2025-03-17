using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanelController : PanelController
{
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    [SerializeField] private List<RectTransform> _lowBlocks;
    [SerializeField] private List<RectTransform> _highBlocks;

    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _retryButton;

    private int _currentGradeScore = 0;
    public int highGradeScore = 30;
    public int lowGradeScore = -30;
    public int changePoint = 10;
    private int _blockCount = 3;
    private bool _isFirstTime = true;

    private void Start()
    {
        _confirmButton.onClick.AddListener(() => HidePanel());
        _retryButton.onClick.AddListener(() => RetryGame());

        _descriptionText.alpha = 0;
        _confirmButton.transform.localScale = Vector3.zero;
        _retryButton.transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ShowResult(_currentGradeScore - changePoint);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ShowResult(_currentGradeScore + changePoint);
        }
    }

    public void ShowResult(int newScore)
    {
        int prevScore = _currentGradeScore;
        _currentGradeScore = Mathf.Clamp(newScore, lowGradeScore, highGradeScore);

        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            Show();
        }

        bool isWin = (newScore - prevScore) > 0;

        _titleText.text = isWin ?
            $"게임에서 승리했습니다.\n{changePoint} 포인트를 받았습니다." :
            $"게임에서 패배했습니다.\n{changePoint} 포인트 감소했습니다.";

        if (_isFirstTime)
        {
            _isFirstTime = false;
            SetInitialBlockState();
        }
        else
        {
            StartCoroutine(AnimateBlockStepByStep(prevScore, _currentGradeScore));
        }
    }

    private void SetInitialBlockState()
    {
        int filledBlocks = _currentGradeScore / changePoint;

        for (int i = 0; i < _blockCount; i++)
        {
            _lowBlocks[i].localScale = (i < Math.Abs(filledBlocks) && filledBlocks < 0) ? Vector3.one : Vector3.zero;
            _highBlocks[i].localScale = (i < filledBlocks && filledBlocks > 0) ? Vector3.one : Vector3.zero;
        }
    }

    private IEnumerator AnimateBlockStepByStep(int fromScore, int toScore)
    {
        int step = (toScore > fromScore) ? changePoint : -changePoint;

        while (fromScore != toScore)
        {
            int nextScore = fromScore + step;
            if ((step > 0 && nextScore > toScore) || (step < 0 && nextScore < toScore))
            {
                nextScore = toScore;
            }

            AnimateBlock(fromScore, nextScore);
            yield return new WaitForSeconds(0.3f);
            fromScore = nextScore;
        }

        ShowDescriptionAndButtons();
    }

    private void AnimateBlock(int fromScore, int toScore)
    {
        int fromBlockIndex = fromScore / changePoint;
        int toBlockIndex = toScore / changePoint;

        if (toScore > 0)
        {
            if (toBlockIndex > fromBlockIndex)
            {
                int index = toBlockIndex - 1;
                if (index >= 0 && index < _highBlocks.Count)
                {
                    _highBlocks[index].localScale = new Vector3(0, 1, 1);
                    _highBlocks[index].DOScaleX(1, 0.3f).SetEase(Ease.OutBack);
                }
            }
            else if (toBlockIndex < fromBlockIndex)
            {
                int index = fromBlockIndex - 1;
                if (index >= 0 && index < _highBlocks.Count)
                {
                    _highBlocks[index].DOScaleX(0, 0.3f).SetEase(Ease.InBack);
                }
            }
        }
        else if (toScore < 0)
        {
            if (toBlockIndex < fromBlockIndex)
            {
                int index = Math.Abs(toBlockIndex) - 1;
                if (index >= 0 && index < _lowBlocks.Count)
                {
                    _lowBlocks[index].localScale = new Vector3(0, 1, 1);
                    _lowBlocks[index].DOScaleX(1, 0.3f).SetEase(Ease.OutBack);
                }
            }
            else if (toBlockIndex > fromBlockIndex)
            {
                int index = Math.Abs(fromBlockIndex) - 1;
                if (index >= 0 && index < _lowBlocks.Count)
                {
                    _lowBlocks[index].DOScaleX(0, 0.3f).SetEase(Ease.InBack);
                }
            }
        }


        if (toScore == 0)
        {
            if (fromScore > 0)
            {
                int index = fromBlockIndex - 1;
                if (index >= 0 && index < _highBlocks.Count)
                {
                    _highBlocks[index].DOScaleX(0, 0.3f).SetEase(Ease.InBack);
                }
            }
            else if (fromScore < 0)
            {
                int index = Math.Abs(fromBlockIndex) - 1;
                if (index >= 0 && index < _lowBlocks.Count)
                {
                    _lowBlocks[index].DOScaleX(0, 0.3f).SetEase(Ease.InBack);
                }
            }
        }
    }


    private void ShowDescriptionAndButtons()
    {
        _descriptionText.DOFade(1, 0.5f);
        _confirmButton.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
        _retryButton.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
    }

    private void HidePanel()
    {
        Hide(() => Destroy(gameObject));
    }

    private void RetryGame()
    {
        Debug.Log("재도전 버튼 클릭됨");
    }
}
