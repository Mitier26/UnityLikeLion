using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public enum EnemyState { None, Idle, Patrol, Trace, Attack, Hit, Dead }


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    [Header("Basic Info")] 
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int attackPower = 10;
    [SerializeField] private int defensePower = 5;
    

    [Header("AI")] 
    [SerializeField] private float detectCircleRadius = 10f;
    public float DetectCircleRadius => detectCircleRadius;
    [SerializeField] private LayerMask targetLayerMask;
    public LayerMask TargetLayerMask => targetLayerMask;
    [SerializeField] private float maxDetectSightAngle = 30f;
    public float MaxDetectSightAngle => maxDetectSightAngle;
    [SerializeField] private float maxPatrolWaitTime = 3f;
    public float MaxPatrolWaitTime => maxPatrolWaitTime;
    [SerializeField] private float maxAttackDistance = 2f;
    public float MaxAttackDistance => maxAttackDistance;
    
    public Renderer EnemyRenderer => enemyRenderer;
    [SerializeField] private Renderer enemyRenderer;

    [SerializeField] private HpBarController _hpBarController;
    
    // -----
    // 상태
    private EnemyStateIdle _enemyStateIdle;
    private EnemyStatePatrol _enemyStatePatrol;
    private EnemyStateTrace _enemyStateTrace;
    private EnemyStateAttack _enemyStateAttack;
    private EnemyStateHit _enemyStateHit;
    private EnemyStateDead _enemyStateDead;

    public EnemyState CurrentState { get; private set; }
    private Dictionary<EnemyState, IEnemyState> _enemyStates;

    // -----
    // 컴포넌트
    public Animator EnemyAnimator { get; private set; }
    public NavMeshAgent Agent { get; private set; }

    // -----
    // 일반 멤버
    private int _currentHealth;

    private void Awake()
    {
        EnemyAnimator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        Agent.updatePosition = true;
        Agent.updateRotation = true;
    }

    private void Start()
    {
        // 상태 객체 초기화
        _enemyStateIdle = new EnemyStateIdle();
        _enemyStatePatrol = new EnemyStatePatrol();
        _enemyStateTrace = new EnemyStateTrace();
        _enemyStateAttack = new EnemyStateAttack();
        _enemyStateHit = new EnemyStateHit();
        _enemyStateDead = new EnemyStateDead();
        
        _enemyStates = new Dictionary<EnemyState, IEnemyState>
        {
            { EnemyState.Idle, _enemyStateIdle },
            { EnemyState.Patrol, _enemyStatePatrol },
            { EnemyState.Trace, _enemyStateTrace },
            { EnemyState.Attack, _enemyStateAttack },
            { EnemyState.Hit, _enemyStateHit },
            { EnemyState.Dead, _enemyStateDead }
        };
        
        // HP 초기화
        _currentHealth = maxHealth;
        _hpBarController.SetHP(_currentHealth/ (float)maxHealth);
        
        // 상태 초기화
        SetState(EnemyState.Idle);
    }

    private void Update()
    {
        if (CurrentState != EnemyState.None)
        {
            _enemyStates[CurrentState].Update();
        }
    }

    public void SetState(EnemyState newState)
    {
        if(CurrentState != EnemyState.None)
        {
            _enemyStates[CurrentState].Exit();
        }

        CurrentState = newState;
        _enemyStates[CurrentState].Enter(this);
    }

    #region Hit 관련

    public void SetHit(PlayerController playerController)
    {
        var attackPower = playerController.AttackPower - defensePower;
        _currentHealth -= attackPower;
        
        _hpBarController.SetHP(_currentHealth/ (float)maxHealth);

        if (_currentHealth <= 0)
        {
            SetState(EnemyState.Dead);
        }
        else
        {
            _enemyStateHit.SetAttacker(playerController);
            SetState(EnemyState.Hit);
        }
    }

    #endregion
    
    #region 이동 관련

    private void OnAnimatorMove()
    {
        Vector3 position = EnemyAnimator.rootPosition;

        position.y = transform.position.y;

        Agent.nextPosition = position;
        transform.position = position;
    }

    public void PlayStep()
    {

    }

    public void Grunt()
    {
        
    }
    
    public void AttackBegin()
    {
        
    }
    
    public void AttackEnd()
    {
        SetState(EnemyState.Idle);
    }

    #endregion

    #region 적 감지

    // 일정 변경에 플레이어가 진입하면 플레이어를 감지
    public Transform DetectPlayerInCircle()
    {
        var hitColliders = Physics.OverlapSphere(transform.position, detectCircleRadius, targetLayerMask);

        if (hitColliders.Length > 0)
        {
            return hitColliders[0].transform;
        }
        else
        {
            return null;
        }
    }

    #endregion

    #region 디버그

    private void OnDrawGizmos()
    {
        // 감지 범위
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectCircleRadius);

        // 시야각
        Gizmos.color = Color.red;
        Vector3 rightDirection = Quaternion.Euler(0, maxDetectSightAngle, 0) * transform.forward;
        Vector3 leftDirection = Quaternion.Euler(0, -maxDetectSightAngle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, rightDirection * detectCircleRadius);
        Gizmos.DrawRay(transform.position, leftDirection * detectCircleRadius);
        Gizmos.DrawRay(transform.position, transform.forward * detectCircleRadius);
        
        // Agent 목적지 시각화
        if (Agent != null && Agent.hasPath)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(Agent.destination, 0.5f);
            Gizmos.DrawLine(transform.position, Agent.destination);
        }
    }

    #endregion

}
