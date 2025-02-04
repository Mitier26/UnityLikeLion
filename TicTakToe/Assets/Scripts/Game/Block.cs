using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private Sprite oSprite;
    [SerializeField] private Sprite xSprite;
    [SerializeField] private SpriteRenderer markerSpriteRenderer;

    public enum MarkerType { None, O, X }

    // 델리게이트의 형태, 중요한 것은 반환값, 매개변수
    // 대신 일을 할 것을 만든다.
    public delegate void OnBlockClicked(int index);
    public OnBlockClicked onBlockClicked;
    // event로 사용하고 싶다면 event 추가
    // blockClicked 에는 함수가 들어 있다.
    
    // 몇 번 버튼 인지 확인 할 변수
    private int _blockIndex;

    /// <summary>
    /// Block 초기화 함수
    /// </summary>
    /// <param name="blockIndex">Block 인덱스</param>
    /// <param name="onBlockClicked">Block 터치 이벤트</param>
    public void InitMarker(int blockIndex, OnBlockClicked onBlockClicked)
    {
        _blockIndex = blockIndex;
        SetMarker(MarkerType.None);
        this.onBlockClicked = onBlockClicked;
    }
    
    /// <summary>
    /// 어떤 마커를 표시할지 전달하는 함수
    /// </summary>
    /// <param name="markerType">마커 타입</param>
    public void SetMarker(MarkerType markerType)
    {
        switch (markerType)
        {
            case MarkerType.O:
                markerSpriteRenderer.sprite = oSprite;
                break;
            case MarkerType.X:
                markerSpriteRenderer.sprite = xSprite;
                break;
            case MarkerType.None:
                markerSpriteRenderer.sprite = null;
                break;
        }
    }

    private void OnMouseUpAsButton()
    {
        // 해당 버튼을 클릭 된 상태에서 up 했을 때만 작동
        onBlockClicked?.Invoke(_blockIndex);
        // 몇 번이 클릭 되었는지 알아야 한다.
    }
}