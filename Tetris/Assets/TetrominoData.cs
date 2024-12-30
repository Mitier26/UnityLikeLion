using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TetrominoType
{
    None,
    I,
    O,
    Max,
}

public class TetrominoData : MonoBehaviour
{
    [Header("Tetromino Data")]
    // 블럭의 타입을 정의
    [SerializeField] private TetrominoType tetrominoType = TetrominoType.None;
    // 블럭이 떨어지는 시간
    [SerializeField] private float dropTime = 1.0f;
    
    // 블럭이 이동할 수 있는 한계점
    [SerializeField] private float leftLimitPosition = -4.0f;
    [SerializeField] private float rightLimitPosition = 4.0f;
    [SerializeField] private float bottomLimitPosition = -9.0f;
    

    private bool canDrop = true;
    
    private List<Transform> blocks = new List<Transform>();
    
    // 현재 시간
    private float currentDropTime = 0.0f;


    private void Start()
    {
        currentDropTime = dropTime;
        
        // 자식들을 찾아 리스트를 만든다.
        blocks = GetComponentsInChildren<Transform>().ToList();
    }

    private void Update()
    {
        if (!canDrop) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += Vector3.left;

            if (CheckBlocksCollision())
            {
                transform.position -= Vector3.left;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += Vector3.right;

            if (CheckBlocksCollision())
            {
                transform.position -= Vector3.right;
            }
        }
        
        currentDropTime -= Time.deltaTime;

        if (currentDropTime < 0.0f)
        {
            transform.position += Vector3.down;
            if (CheckBlockFinishMove())
            {
                transform.position -= Vector3.down;
                canDrop = false;
                GameObject tetrominoPrefab = Resources.Load<GameObject>($"Prefabs/Tetromino_O");
                Instantiate(tetrominoPrefab, new Vector2(0, 9), Quaternion.identity);
            }
            currentDropTime = dropTime;
        }
    }

    bool CheckBlocksCollision()
    {
        foreach (Transform block in blocks)
        {
            if (block.position.x < leftLimitPosition)
            {
                return true;
            }

            if (block.position.x > rightLimitPosition)
            {
                return true;
            }
        }
        return false;
    }

    bool CheckBlockFinishMove()
    {
        foreach (Transform block in blocks)
        {
            if (block.position.y < bottomLimitPosition)
            {
                return true;
            }
        }
        return false;
    }
}
