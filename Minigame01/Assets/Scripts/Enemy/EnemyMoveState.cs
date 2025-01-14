using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : IEnemyState
{
    private EnemyStateMachine enemy;

    public EnemyMoveState(EnemyStateMachine enemy)
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
    }

    public void OnTakeDamage()
    {
        enemy.ChangeState(new EnemyChaseState(enemy));
    }
}
