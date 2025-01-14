using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private EnemyStateMachine enemy;
    
    private float idleTimer;
    private float idleDuration;

    public EnemyIdleState(EnemyStateMachine enemy)
    {
        this.enemy = enemy;
    }
    
    public void Enter()
    {
        idleTimer = 0;
    }

    public void Exit()
    {
    }

    public void UpdateState()
    {
        idleTimer += Time.deltaTime;
        
        if (enemy.enemyBlackboard.playerLayer != null)
        {
            enemy.ChangeState(new EnemyChaseState(enemy));
            return;
        }

        if (idleTimer >= idleDuration)
        {
            enemy.ChangeState(new EnemyMoveState(enemy));
            return;
        }
    }

    public void OnTakeDamage()
    {
        Debug.unityLogger.Log("OnTakeDamage");
        enemy.ChangeState(new EnemyChaseState(enemy));
    }
}
