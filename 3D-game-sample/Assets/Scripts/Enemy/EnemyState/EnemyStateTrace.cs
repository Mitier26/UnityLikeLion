﻿using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateTrace : IEnemyState
{
    private EnemyController _enemyController;
    private Transform _detectPlayerTransform;
    private float _maxDetectPlayerInCircleWaitTime = 1f;
    private float _detectPlayetInCircleWaitTime = 0f;

    public void Enter(EnemyController enemyController)
    {
        _enemyController = enemyController;

        // 일정 변경에 플레이어가 진입하면 플레이어를 향해 이동
        _detectPlayerTransform = _enemyController.DetectPlayerInCircle();
        if (_detectPlayerTransform == null)
        {
            _enemyController.SetState(EnemyState.Idle);
            return;
        }

        _enemyController.Agent.isStopped = false;
        _enemyController.Agent.SetDestination(_detectPlayerTransform.position);

        _enemyController.EnemyAnimator.SetBool("Trace", true);

    }

    public void Update()
    {
        // 일정 주기로 찾은 플레이어의 위치를 갱신해서 갱신된 위치로 이동
        if (_detectPlayetInCircleWaitTime > _maxDetectPlayerInCircleWaitTime)
        {
            _enemyController.Agent.SetDestination(_detectPlayerTransform.position);
            _maxDetectPlayerInCircleWaitTime = 0f;
        }

        _detectPlayetInCircleWaitTime += Time.deltaTime;
        
        var playerDistanceSqr = (_detectPlayerTransform.position - _enemyController.transform.position).sqrMagnitude;
        
        // 트레이스 중 시야에 플레이어가 들어오면 속도 증가
        if (DetectPlayerInSight(_detectPlayerTransform) && playerDistanceSqr > 9f)
        {
            _enemyController.EnemyAnimator.SetFloat("Speed", 1f);
        }
        else
        {
            _enemyController.EnemyAnimator.SetFloat("Speed", 0f);
        }
        
        // 플레이어를 감지할 수 있는 변경을 넘어서면 다시 아이들 상태로 전환
        if(playerDistanceSqr > (_enemyController.DetectCircleRadius * _enemyController.DetectCircleRadius))
        {
            _enemyController.SetState(EnemyState.Idle);
            return;
        }
        
        // 전방에 플레이어가 있고, 공격 가능 거리이면 공격 상태로 전환
        RaycastHit hit;
        Vector3 transformPosition = _enemyController.transform.position;
        transformPosition.y = 1f;
        if (Physics.Raycast(transformPosition, _enemyController.transform.forward, out hit,
                _enemyController.MaxAttackDistance, _enemyController.TargetLayerMask))
        {
            _enemyController.SetState(EnemyState.Attack);
            return;

        }
    }

    public void Exit()
    {
        _enemyController.EnemyAnimator.SetBool("Trace", false);
        _enemyController = null;
    }

    // 일정 반경에 플레이어가 진입하면 시야에 들어왔다고 판단하는 함수
    private bool DetectPlayerInSight(Transform playerTransform)
    {
        if (playerTransform == null)
        {
            return false;
        }

        var cosTheta = Vector3.Dot(_enemyController.transform.forward, (playerTransform.position - _enemyController.transform.position).normalized);
        var angle = Mathf.Acos(cosTheta) * Mathf.Rad2Deg;

        if (angle < _enemyController.MaxDetectSightAngle)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}