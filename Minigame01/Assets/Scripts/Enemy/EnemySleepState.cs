using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySleepState : IEnemyState
{
    private EnemyStateMachine enemy;
    private float recoveryRate = 5f;

    public EnemySleepState(EnemyStateMachine enemy)
    {
        this.enemy = enemy;
    }
    
    public void Enter()
    {
        enemy.enemyBlackboard.speed = 0;
    }

    public void Exit()
    {
    }

    public void UpdateState()
    {
        RecoveryHealth();

        if (enemy.enemyBlackboard.health >= enemy.enemyBlackboard.maxHealth)
        {
            enemy.ChangeState(new EnemyIdleState(enemy));
            return;
        }
    }

    private void RecoveryHealth()
    {
        enemy.enemyBlackboard.health += recoveryRate * Time.deltaTime;
        enemy.enemyBlackboard.health = Mathf.Clamp(enemy.enemyBlackboard.health, 0f,100f);
    }

    public void OnTakeDamage()
    {
        if (Random.value < 0.5f)
        {
            enemy.ChangeState(new EnemyFleeState(enemy));
            return;
        }
        else
        {
            enemy.ChangeState(new EnemyAngryState(enemy));
            return;
        }
    }
}
