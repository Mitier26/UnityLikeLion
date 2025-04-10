using UnityEngine;

public class EnemyStateAttack : IEnemyState
{
    private EnemyController _enemyController;
    public void Enter(EnemyController enemyController)
    {
        _enemyController = enemyController;
        
        _enemyController.EnemyAnimator.SetBool("Attack", true);

        _enemyController.Agent.isStopped = true;
    }

    public void Update()
    {
        
    }

    public void Exit()
    {
        _enemyController = null;
    }


}