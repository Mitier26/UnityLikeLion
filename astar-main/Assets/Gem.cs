using UnityEngine;
using System.Collections.Generic;
using System.Linq;

// 보석 타입 enum
public enum GemType
{
    Red,
    Blue,
    Green,
    Yellow,
    Purple,
    Empty
}

// 개별 보석을 관리하는 클래스
public class Gem : MonoBehaviour
{
    public GemType type;
    public int row;
    public int col;
    private Match3Game gameManager;
    private Vector2 targetPosition;
    private bool isMoving = false;

    [SerializeField] private float moveSpeed = 10f;

    private void Update()
    {
        if (isMoving)
        {
            Vector2 currentPosition = transform.position;
            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
            
            if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
                gameManager.OnGemFinishedMoving();
            }
        }
    }

    public void Initialize(Match3Game manager, GemType gemType, int r, int c)
    {
        gameManager = manager;
        type = gemType;
        row = r;
        col = c;
        UpdatePosition();
    }

    public void MoveTo(int newRow, int newCol)
    {
        row = newRow;
        col = newCol;
        targetPosition = gameManager.GetWorldPosition(row, col);
        isMoving = true;
    }

    private void UpdatePosition()
    {
        transform.position = gameManager.GetWorldPosition(row, col);
    }

    private void OnMouseDown()
    {
        gameManager.SelectGem(this);
    }
}