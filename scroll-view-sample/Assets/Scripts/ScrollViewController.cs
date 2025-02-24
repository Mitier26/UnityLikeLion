using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
[RequireComponent(typeof(RectTransform))]
public class ScrollViewController : MonoBehaviour
{

    [SerializeField] private GameObject cellPrefab;
    
    private ScrollRect _scrollRect;
    private RectTransform _rectTransform;
    
    private List<Item> _items = new List<Item>();

    private int topIndex = 1;
    
    
    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        InitData();
    }

    private void ReloadData()
    {
        // 다시 추가 데이터를 가지고? 옴
       
    }

    private void InitData()
    {
        for (int i = 0; i < 10; i++)
        {
            var cellObj = Instantiate(cellPrefab, _scrollRect.content);
            Item item = new Item();
            item.imageFileName = "lion";
            item.title = "Title " + (topIndex + i);
            item.subtitle = "Subtitle " + (topIndex + i);
            cellObj.GetComponent<Cell>().SetItem(item);
            _items.Add(item);
        }
    }

    #region Scroll Rect Events
    
    public void OnValueChanged(Vector2 value)
    {
        var x = _scrollRect.content.anchoredPosition.x;
        var y = _scrollRect.content.anchoredPosition.y;
        var w = _rectTransform.rect.width;
        var h = _rectTransform.rect.height;

        float cellHeight = cellPrefab.GetComponent<RectTransform>().rect.height;
        float spacing = _scrollRect.content.GetComponent<VerticalLayoutGroup>().spacing;
        
        float step = cellHeight + spacing;

        int passedCells = Mathf.FloorToInt(y / step);
        
    }
    
    #endregion
}
