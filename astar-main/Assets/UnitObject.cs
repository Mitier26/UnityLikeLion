using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class UnitObject : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float stoppingDistance = 0.1f;

    [Header("Path Settings")]
    [SerializeField] private float pathUpdateInterval = 0.5f;

    private List<Vector3> currentPath = new List<Vector3>();
    private int pathIndex = 0;
    private Vector3 targetPosition;
    private bool isMoving = false;
    
    [SerializeField] private float avoidanceRadius = 1f;
    [SerializeField] private LayerMask unitLayer;  

    public bool HasReachedDestination { get; private set; }

    public FormationManager formationManager;
    
    private void Update()
    {
        if (isMoving && currentPath.Count > 0)
        {
            FollowPath();
        }
    }

    protected virtual void OnDestinationReached()
    {
        HasReachedDestination = true;
        // FormationManager에게 도착 알림
        formationManager?.OnUnitReachedDestination(this);
    }

    // 경로 따라가기
    private void FollowPath()
    {
        if (pathIndex >= currentPath.Count) return;

        Vector3 target = currentPath[pathIndex];
        Vector3 direction = target - transform.position;
        direction.y = 0;
        float distance = direction.magnitude;

        if (distance > stoppingDistance)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
        else
        {
            pathIndex++;
            if (pathIndex >= currentPath.Count)
            {
                isMoving = false;
                OnDestinationReached();
            }
        }
    }

    // 새로운 경로 설정
    public void SetPath(List<Vector3> newPath)
    {
        Debug.Log("setPath");
        
        currentPath = newPath;
        pathIndex = 0;
        isMoving = true;
    }
}