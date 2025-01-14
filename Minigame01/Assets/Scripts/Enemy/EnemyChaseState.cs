using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : IEnemyState
{
    private EnemyStateMachine enemy;

    public EnemyChaseState(EnemyStateMachine enemy)
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
        
        ChasePlayer();

        if (enemy.enemyBlackboard.health <= 30f)
        {
            if (Random.value > 0.5f)
            {
                enemy.ChangeState(new EnemyFleeState(enemy));
            }
            else
            {
                enemy.ChangeState(new EnemyAngryState(enemy));
            }
            
        }
    }

    public void OnTakeDamage()
    {
        
        
    }

    private void ChasePlayer()
    {
        Vector3 direction = (enemy.enemyBlackboard.player.position - enemy.transform.position).normalized;
        
        enemy.transform.position += direction * enemy.enemyBlackboard.normalSpeed * Time.deltaTime;
    }
}
