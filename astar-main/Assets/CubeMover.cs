using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class CubeMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private LayerMask groundLayer;
    private GridPathfinding pathfinding;
    private List<Vector3> currentPath;
    private bool isMoving;

    private void Start()
    {
        pathfinding = FindObjectOfType<GridPathfinding>();
        if (pathfinding == null)
        {
            Debug.LogError("GridPathfinding 컴포넌트를 찾을 수 없습니다!");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                List<Vector3> newPath = pathfinding.FindPath(transform.position, hit.point);
                Debug.Log(hit.point);
                if (newPath != null && newPath.Count > 0)
                {
                    StopAllCoroutines();
                    currentPath = newPath;
                    StartCoroutine(FollowPath());
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                pathfinding.ToggleNode(hit.point);
                var startPos = transform.position;
                var endPos = startPos;
                if (currentPath != null && currentPath.Count > 0)
                {
                    endPos = currentPath.Last();
                }
                List<Vector3> newPath = pathfinding.FindPath(startPos, endPos);
                Debug.Log(hit.point);
                if (newPath != null && newPath.Count > 0)
                {
                    StopAllCoroutines();
                    currentPath = newPath;
                    StartCoroutine(FollowPath());
                }
            }
        }
    }

    private IEnumerator FollowPath()
    {
        isMoving = true;
        int pathIndex = 0;

        while (pathIndex < currentPath.Count)
        {
            Vector3 targetPosition = currentPath[pathIndex];
            targetPosition.y = transform.position.y; // Y축 높이 유지

            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetPosition,
                    moveSpeed * Time.deltaTime
                );
                yield return null;
            }

            pathIndex++;
        }

        isMoving = false;
    }

    // 현재 이동 중인지 확인하는 프로퍼티
    public bool IsMoving => isMoving;
}