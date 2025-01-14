using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    private EnemyStateMachine enemy;

    public EnemyPatrolState(EnemyStateMachine enemy)
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
        
    }
}
