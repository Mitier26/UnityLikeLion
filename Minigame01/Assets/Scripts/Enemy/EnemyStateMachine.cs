using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    // 현재의 상태를 저장하고, 현재 상태를 반영하기 위한 것
    public IEnemyState currentState;

    public EnemyBlackboard enemyBlackboard = new EnemyBlackboard();

    // 상태를 변경하는 것, 다른 곳에서 사용할 수 있게 public 으로 만듬
    public void ChangeState(IEnemyState newState)
    {
        // 다른 상태로 변경하기 전에 기존의 상태를 종료해야 한다.
        if (currentState != null)
        {
            currentState.Exit();
        }
        
        currentState = newState;
        
        // 새로운 상태를 저장하고 실행한다.
        if (currentState != null)
        {
            currentState.Enter();
        }
    }

    private void Start()
    {
        ChangeState(new EnemyIdleState(this));
    }

    // 업데이트를 돌리기 위해
    private void Update()
    {
        if (!GameManager.instance.isplaying) return;
        
        currentState?.UpdateState();
        
        DetectPlayer();
    }
    
    // 공통으로 사용할 것, 공통으로 사용 할 것은 EnemyStateMachine에 !!!!
    public void DetectPlayer()
    {
        // 수동으로 설정된 경우 탐지 로직 비활성화
        if (enemyBlackboard.isPlayer) return;

        // 탐지 범위 내에서 플레이어 검색
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, enemyBlackboard.detectRange, enemyBlackboard.playerLayer);

        if (hitCollider != null)
        {
            enemyBlackboard.player = hitCollider.transform;
            return;
        }

        // 탐지된 플레이어가 없으면 null로 설정
        if (enemyBlackboard.player != null)
        {
            float distance = Vector3.Distance(transform.position, enemyBlackboard.player.position);
            if (distance > enemyBlackboard.detectRange)
            {
                enemyBlackboard.player = null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyBlackboard.detectRange);
    }

    public void TakeDamage(float damage, Transform attacker)
    {
        enemyBlackboard.player = attacker;
        enemyBlackboard.health -= damage;
        enemyBlackboard.isPlayer = true;

        if (enemyBlackboard.health <= 0)
        {
            GameManager.instance.DiscountEnemy();
            Destroy(gameObject);
            return;
        }
        
        currentState?.OnTakeDamage();
    }
}
