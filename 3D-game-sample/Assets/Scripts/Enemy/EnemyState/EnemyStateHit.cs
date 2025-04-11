﻿using UnityEngine;

public class EnemyStateHit : IEnemyState
{
    private EnemyController _enemyController;
    private PlayerController _attacker;
    private Vector3 _KnockBackTargetPosition;
    
    public void Enter(EnemyController enemyController)
    {
        _enemyController = enemyController;
        _enemyController.EnemyAnimator.SetTrigger("Hit");
        
        Vector3 knockbackForce = (_enemyController.transform.position - _attacker.transform.position).normalized;
        _KnockBackTargetPosition = enemyController.transform.position + knockbackForce;
        
        _enemyController.EnemyRenderer.material.color = Color.red;
    }

    public void Update()
    {
        if (_attacker != null)
        {
            _enemyController.transform.position = Vector3.Lerp(_enemyController.transform.position,
                _KnockBackTargetPosition, Time.deltaTime * 5f);
        }
        
        _enemyController.EnemyRenderer.material.color = Color.Lerp(_enemyController.EnemyRenderer.material.color, Color.white, Time.deltaTime * 5f);
    }

    public void Exit()
    {
        _enemyController.EnemyRenderer.material.color = Color.white;
        _enemyController = null;
    }
    
    public void SetAttacker(PlayerController attacker)
    {
        _attacker = attacker;
    }
}