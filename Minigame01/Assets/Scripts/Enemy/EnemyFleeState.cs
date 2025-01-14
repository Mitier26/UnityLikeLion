using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFleeState : IEnemyState
{
    private EnemyStateMachine enemy;
    private float fleeTimer;
    private float fleeDuration = 3f;

    public EnemyFleeState(EnemyStateMachine enemy)
    {
        this.enemy = enemy;
    }
    
    public void Enter()
    {
    }

    public void Exit()
    {
    }

    public void UpdateState()
    {
        if (enemy.enemyBlackboard.player == null)
        {
            enemy.ChangeState(new EnemyIdleState(enemy));
            return;
        }

        FleeFromPlayer();
        
        fleeTimer += Time.deltaTime;

        if (fleeTimer >= fleeDuration)
        {
            enemy.ChangeState(new EnemySleepState(enemy));
            return;
        }
    }

    public void OnTakeDamage()
    {
        
    }

    private void FleeFromPlayer()
    {
        Vector3 direction = (enemy.transform.position - enemy.enemyBlackboard.player.position).normalized;

        enemy.transform.position += direction * enemy.enemyBlackboard.fleeSpeed * Time.deltaTime;

    }
}
