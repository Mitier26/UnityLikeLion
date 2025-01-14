using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAngryState : IEnemyState
{
    private EnemyStateMachine enemy;

    public EnemyAngryState(EnemyStateMachine enemy)
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
        Vector3 direction = (enemy.enemyBlackboard.player.position - enemy.transform.position).normalized;
        
        enemy.transform.position += direction * enemy.enemyBlackboard.angrySpeed * Time.deltaTime;
    }

    public void OnTakeDamage()
    {
    }
}
