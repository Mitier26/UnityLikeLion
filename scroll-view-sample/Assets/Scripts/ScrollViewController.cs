using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
[RequireComponent(typeof(RectTransform))]
public class ScrollViewController : MonoBehaviour
{
    [SerializeField] private float cellHeight;
    
    private ScrollRect _scrollRect;
    private RectTransform _rectTransform;
    
    private List<Item> _items;                     // Cell에 표시할 Item 정보
    private LinkedList<Cell> _visibleCells;        // 화면에 표시되고 있는 Cell 정보

    private float _lastYValue = 1;
    
    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        LoadData();
    }

    /// <summary>
    /// 현재 보여질 Cell 인덱스를 반환하는 메서드
    /// </summary>
    /// <returns>startIndex : 가장 위에 표시될 Cell 인덱스, endIndex : 가장 아래에 표시될 Cell 인덱스</returns>
    private (int startIndex, int endIndex) GetVisibleIndexRange()
    {
        var visibleRect = new Rect(
            _scrollRect.content.anchoredPosition.x, 
            _scrollRect.content.anchoredPosition.y,
            _rectTransform.rect.width, 
            _rectTransform.rect.height);

        // 스크롤 위치에 따른 시작 인덱스 계산
        var startIndex = Mathf.FloorToInt(visibleRect.y / cellHeight);

        // 화면에 보이게 될 Cell 개수 계산
        int visibleCount = Mathf.CeilToInt(visibleRect.height / cellHeight);
        
        // 버퍼 추가
        startIndex = Mathf.Max(0, startIndex - 1);
        
        visibleCount += 2;
        
        return (startIndex, startIndex + visibleCount - 1);
    }

    /// <summary>
    /// 특정 인덱스가 화면에 보여야 하는지 여부를 판단하는 메서드
    /// </summary>
    /// <param name="index">특정 인덱스</param>
    /// <returns>true, false</returns>
    private bool IsVisibleIndex(int index)
    {
        var (startIndex, endIndex) = GetVisibleIndexRange();
        endIndex = Mathf.Min(endIndex, _items.Count - 1);
        
        return startIndex <= index && index <= endIndex;
    }
    
    private void ReloadData()
    {
        // _visibleCells 초기화
        _visibleCells = new LinkedList<Cell>();
        
        // Content의높이를 _items의 데이터의 수만큼 계산해서 높이를 지정
        var contentSizeDelta = _scrollRect.content.sizeDelta;
        contentSizeDelta.y = _items.Count * cellHeight;
        _scrollRect.content.sizeDelta = contentSizeDelta;
        
        // 화면에 보이는 영역에 Cell 추가
        var (startIndex, endIndex) = GetVisibleIndexRange();
        var maxEndIndex = Mathf.Min(endIndex, _items.Count - 1);

        // startIndex부터 maxEndIndex까지 Cell을 생성
        for (int i = startIndex; i < maxEndIndex; i++)
        {
            // Cell 만들기
            var cellObject = ObjectPool.Instance.GetObject();
            var cell = cellObject.GetComponent<Cell>();
            cell.SetItem(_items[i], i);
            cell.transform.localPosition = new Vector3(0, -i * cellHeight, 0);

            _visibleCells.AddLast(cell);
        }

    }

    private void LoadData()
    {
        _items = new List<Item>
        {
            new Item { imageFileName = "lion", title = "Title 1", subtitle = "Subtitle 1" },
            new Item { imageFileName = "lion", title = "Title 2", subtitle = "Subtitle 2" },
            new Item { imageFileName = "lion", title = "Title 3", subtitle = "Subtitle 3" },
            new Item { imageFileName = "lion", title = "Title 4", subtitle = "Subtitle 4" },
            new Item { imageFileName = "lion", title = "Title 5", subtitle = "Subtitle 5" },
            new Item { imageFileName = "lion", title = "Title 6", subtitle = "Subtitle 6" },
            new Item { imageFileName = "lion", title = "Title 7", subtitle = "Subtitle 7" },
            new Item { imageFileName = "lion", title = "Title 8", subtitle = "Subtitle 8" },
            new Item { imageFileName = "lion", title = "Title 9", subtitle = "Subtitle 9" },
            new Item { imageFileName = "lion", title = "Title 10", subtitle = "Subtitle 10" },
            new Item { imageFileName = "lion", title = "Title 11", subtitle = "Subtitle 11" },
            new Item { imageFileName = "lion", title = "Title 12", subtitle = "Subtitle 12" },
            new Item { imageFileName = "lion", title = "Title 13", subtitle = "Subtitle 13" },
            new Item { imageFileName = "lion", title = "Title 14", subtitle = "Subtitle 14" },
            new Item { imageFileName = "lion", title = "Title 15", subtitle = "Subtitle 15" },
            new Item { imageFileName = "lion", title = "Title 16", subtitle = "Subtitle 16" },
            new Item { imageFileName = "lion", title = "Title 17", subtitle = "Subtitle 17" },
            new Item { imageFileName = "lion", title = "Title 18", subtitle = "Subtitle 18" },
            new Item { imageFileName = "lion", title = "Title 19", subtitle = "Subtitle 19" },
            new Item { imageFileName = "lion", title = "Title 20", subtitle = "Subtitle 20" },
            new Item {imageFileName = "lion", title = "Title 21", subtitle = "Subtitle 21"},
            new Item {imageFileName = "lion", title = "Title 22", subtitle = "Subtitle 22"},
            new Item {imageFileName = "lion", title = "Title 23", subtitle = "Subtitle 23"},
            new Item {imageFileName = "lion", title = "Title 24", subtitle = "Subtitle 24"},
            new Item {imageFileName = "lion", title = "Title 25", subtitle = "Subtitle 25"},
            new Item {imageFileName = "lion", title = "Title 26", subtitle = "Subtitle 26"},
        };
        
        ReloadData();
    }
    
    #region Scroll Rect Events
    
    public void OnValueChanged(Vector2 value)
    {
        if (_lastYValue < value.y)
        {
            var firstCell = _visibleCells.First.Value;
            var newFirstCellIndex = firstCell.Index - 1;
            
            // 상단에 새로운 Cell이 필요한지 확인 후 필요하면 추가
            if (IsVisibleIndex(newFirstCellIndex))
            {
                var cell = ObjectPool.Instance.GetObject().GetComponent<Cell>();
                cell.SetItem(_items[newFirstCellIndex], newFirstCellIndex);
                cell.transform.localPosition = new Vector3(0, -newFirstCellIndex * cellHeight, 0);
                _visibleCells.AddFirst(cell);
            }
            
            // 하단에 있는 셀이 화면에서 벗어나면 제거
            var lastCell = _visibleCells.Last.Value;
            if (!IsVisibleIndex(lastCell.Index))
            {
                ObjectPool.Instance.ReturnObject(lastCell.gameObject);
                _visibleCells.RemoveLast();
            }
            
        }
        else
        {
            // 하단에 새로운 Cell이 필요한지 확인 후 필요하면 추가
            var lastCell = _visibleCells.Last.Value;
            var newLastCellIndex = lastCell.Index + 1;
            if (IsVisibleIndex(newLastCellIndex))
            {
                var cell = ObjectPool.Instance.GetObject().GetComponent<Cell>();
                cell.SetItem(_items[newLastCellIndex], newLastCellIndex);
                cell.transform.localPosition = new Vector3(0, -newLastCellIndex * cellHeight, 0);
                
                _visibleCells.AddLast(cell);
            }
            
            // 상단에 있는 셀이 화면에서 벗어나면 제거
            var firstCell = _visibleCells.First.Value;
            if (!IsVisibleIndex(firstCell.Index))
            {
                ObjectPool.Instance.ReturnObject(firstCell.gameObject);
                _visibleCells.RemoveFirst();
            }
        }
        
        _lastYValue = value.y;
    }
    
    #endregion
}
