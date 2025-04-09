// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI;
// using Random = UnityEngine.Random;
//
// public enum EnemyState { Idle, Patrol, Trace, Attack, Hit, Dead }
//
//
// [RequireComponent(typeof(Animator))]
// [RequireComponent(typeof(NavMeshAgent))]
// public class EnemyControllerOld : MonoBehaviour
// {
//     [Header("Enemy State")] 
//     [SerializeField] private int attackPower = 1;
//     [SerializeField] private int maxHealth = 100;
//
//     [Header("Enemy AI")] 
//     [SerializeField] private LayerMask targetLayer;
//     [SerializeField] private float detectCircleRadius = 10f;
//     [SerializeField] private float maxPatrolWaitTime = 3f;
//     [SerializeField] private float detectSightAngle = 30f;
//     
//
//     private Animator _enemyAnimator;
//     private NavMeshAgent _navMeshAgent;
//
//     private int _currentHealth;
//     
//     // 적의 현재 상태
//     private EnemyState _currentState;
//     public EnemyState CurrentState => _currentState;
//     
//     private Coroutine _updateDestinationCoroutine;
//     
//     private float _detectCircleRadius;
//     private float _patrolWaitTime;
//     
//     // -----
//     // AI
//     private Transform _playerTransform;     //  감지된 플레이어 트랜스폼
//     
//     
//
//     private void Awake()
//     {
//         _enemyAnimator = GetComponent<Animator>();
//         _navMeshAgent = GetComponent<NavMeshAgent>();
//         _navMeshAgent.updatePosition = true;
//         _navMeshAgent.updateRotation = true;
//     }
//
//     private void Start()
//     {
//         _currentHealth = maxHealth;
//         _detectCircleRadius = detectCircleRadius * detectCircleRadius;
//         _patrolWaitTime = 0;
//         
//         SetState(EnemyState.Idle);
//     }
//
//     private void Update()
//     {
//         switch (_currentState)
//         {
//             case EnemyState.Idle:
//                 {
//                     // 플레이어 감지
//                     var detectPlayer = DetectPlayerInCircle();
//                     
//                     if(detectPlayer)
//                     {
//                         _playerTransform = detectPlayer;
//                         SetState(EnemyState.Trace);
//                         break;
//                     }
//
//                     // 정찰 여부 판단
//                     if (_patrolWaitTime > maxPatrolWaitTime && Random.Range(0, 100) < 30)
//                     {
//                         // 정찰 하기로 결정
//                         SetState(EnemyState.Patrol);
//                         break;
//                     }
//                     _patrolWaitTime += Time.deltaTime;
//                     
//                     break;
//                 }
//             case EnemyState.Patrol:
//                 {
//                     var detectPlayer = DetectPlayerInCircle();
//                     
//                     if(detectPlayer)
//                     {
//                         _playerTransform = detectPlayer;
//                         SetState(EnemyState.Trace);
//                         break;
//                     }
//                     
//                     // 패트롤 위치에 도착하면 Idle 상태로 변경
//                     if (!_navMeshAgent.pathPending || _navMeshAgent.remainingDistance < 0.1f)
//                     {
//                         SetState(EnemyState.Idle);
//                         break;
//                     }
//                     break;
//                 }
//             case EnemyState.Trace:
//                 {
//                     // 일정 거리 이상으로 플레이어 멀어지면 Idle 상태로 변경
//                     // var playerDistance2 = (_playerTransform.position - transform.position).sqrMagnitude;
//                     var playerDistanceSqr = Vector3.Distance(transform.position, _playerTransform.position);
//                     
//                     // 트레이스 중 시야에 플레이어가 들어어면 속도 증가
//                     if (DetectPlayerInSight(_playerTransform))
//                     {
//                         _enemyAnimator.SetFloat("Speed", 1f);
//                     }
//                     else
//                     {
//                         _enemyAnimator.SetFloat("Speed", 0f);
//                     }
//                     
//                     
//                     if (playerDistanceSqr > _detectCircleRadius)
//                     {
//                         SetState(EnemyState.Idle);
//                     }
//                     break;
//                 }
//             case EnemyState.Attack:
//                 break;
//             case EnemyState.Hit:
//                 break;
//             case EnemyState.Dead:
//                 break;
//         }
//     }
//     
//     public void SetState(EnemyState newState)
//     {
//         // Enter
//         switch (newState)
//         {
//             case EnemyState.Idle:
//                 {
//                     //  찾아야 할 플레이어 정보 초기화
//                     _playerTransform = null;
//                     
//                     // Patrol 상태에서 대기 시간 초기화
//                     _patrolWaitTime = 0;
//                     
//                     // Idle 상태에서는 Agent의 이동을 중지
//                     _navMeshAgent.isStopped = true;
//                     
//                     // Idle 애니메이션 재생
//                     _enemyAnimator.SetBool("Idle", true);
//                     break;
//                 }
//             case EnemyState.Patrol:
//                 {
//                     // 랜덤으로 정찰 위치를 구하고, 있으면 해당 위치로 이동, 없으면 다시 Idle 상태로 전환
//                     var patrolPoint = FindRandomPatrolPoint();
//                     if (patrolPoint == transform.position)
//                     {
//                         SetState(EnemyState.Idle);
//                         break;
//                     }
//
//                     _navMeshAgent.isStopped = false;
//                     _navMeshAgent.SetDestination(patrolPoint);
//                     
//                     
//                     // Patrol 애니메이션 재생
//                     _enemyAnimator.SetBool("Patrol", true);
//                     break;
//                 }
//             case EnemyState.Trace:
//                 {
//                     // 감지된 플레이어를 향해 이동
//                     _navMeshAgent.isStopped = false;
//
//                     _updateDestinationCoroutine = StartCoroutine(UpdateDestination());
//                     
//                     // Trace 애니메이션 재생
//                     _enemyAnimator.SetBool("Trace", true);
//                     break;
//                 }
//             case EnemyState.Attack:
//                 break;
//             case EnemyState.Hit:
//                 break;
//             case EnemyState.Dead:
//                 break;
//         }
//         
//         // Exit
//         switch (_currentState)
//         {
//             case EnemyState.Idle:
//                 {
//                     _enemyAnimator.SetBool("Idle", false);
//                     break;
//                 }
//             case EnemyState.Patrol:
//                 {
//                     _enemyAnimator.SetBool("Patrol", false);
//                     break;
//                 }
//             case EnemyState.Trace:
//                 {
//                     if(_updateDestinationCoroutine != null)
//                     {
//                         StopCoroutine(_updateDestinationCoroutine);
//                         _updateDestinationCoroutine = null;
//                     }
//                     
//                     _enemyAnimator.SetBool("Trace", false);
//                     break;
//                 }
//             case EnemyState.Attack:
//                 break;
//             case EnemyState.Hit:
//                 break;
//             case EnemyState.Dead:
//                 break;
//         }
//         
//         _currentState = newState;
//     }
//
//     #region 적 감지
//
//     private Vector3 FindRandomPatrolPoint()
//     {
//         Vector3 randomDirection = Random.insideUnitCircle * detectCircleRadius;
//         randomDirection += transform.position;
//
//         NavMeshHit hit;
//
//         if (NavMesh.SamplePosition(randomDirection, out hit, detectCircleRadius, NavMesh.AllAreas))
//         {
//             return hit.position;
//         }
//         else
//         {
//             return transform.position;
//         }
//     }
//     
//     private IEnumerator UpdateDestination()
//     {
//         while (_playerTransform)
//         {
//             _navMeshAgent.SetDestination(_playerTransform.position);
//             yield return new WaitForSeconds(0.5f);
//         }
//     }
//
//     // 일정 변경에 플레이어가 진입하면 플레이어를 감지
//     private Transform DetectPlayerInCircle()
//     {
//         var hitColliders = Physics.OverlapSphere(transform.position, detectCircleRadius, targetLayer);
//
//         if (hitColliders.Length > 0)
//         {
//             return hitColliders[0].transform;
//         }
//         else
//         {
//             return null;
//         }
//         
//     }
//     
//     // 일정 반경에 플레이어가 진입하면 시야에 들어왔다고 판단하는 함수
//     private bool DetectPlayerInSight(Transform playerTransform)
//     {
//         if (playerTransform == null)
//         {
//             return false;
//         }
//
//         // Vector3 direction = playerTransform.position - transform.position;
//         // float angle = Vector3.Angle(direction, transform.position);
//         
//         var cosTheta = Vector3.Dot(transform.forward, (playerTransform.position - transform.position).normalized);
//         var angle = Mathf.Acos(cosTheta) * Mathf.Rad2Deg;
//
//         if (angle < detectSightAngle)
//         {
//             return true;
//         }
//         else
//         {
//             return false;
//         }
//         
//
//     }
//
//     #endregion
//
//     #region 동작 처리
//
//     private void OnAnimatorMove()
//     {
//         var position = _enemyAnimator.rootPosition;
//
//         position.y = _navMeshAgent.nextPosition.y;
//         _navMeshAgent.nextPosition = position;
//         transform.position = position;
//     }
//
//     #endregion
//
//     #region 디버그
//
//     private void OnDrawGizmos()
//     {
//         // 감지 범위
//         Gizmos.color = Color.yellow;
//         Gizmos.DrawWireSphere(transform.position, detectCircleRadius);
//         
//         // 시야각
//         Gizmos.color = Color.red;
//         Vector3 rightDirection = Quaternion.Euler(0, detectSightAngle, 0) * transform.forward;
//         Vector3 leftDirection = Quaternion.Euler(0, -detectSightAngle, 0) * transform.forward;
//         Gizmos.DrawLine(transform.position, rightDirection * detectCircleRadius);
//         Gizmos.DrawLine(transform.position, leftDirection * detectCircleRadius);
//     }
//
//     #endregion
// }
