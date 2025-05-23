﻿using UnityEngine;


public class PlayerStateAttack : MonoBehaviour, IPlayerState
{
    private static readonly int Attack = Animator.StringToHash("Attack");
    private PlayerController _playerController;
    public bool IsAttacking { get; set; }
    
    // 해당 상태로 집입했을 때 호출되는 메서드
    public void Enter(PlayerController playerController)
    {
        _playerController = playerController;
        _playerController.Animator.SetTrigger(Attack);
    }

    // 해당 상태에 머물려 있을 때 Update 주기로 호출되는 메서드
    public void Update()
    {
        if(Input.GetButtonDown("Fire1") && _playerController.IsGrounded && !IsAttacking)
        {
            _playerController.Animator.SetTrigger(Attack);
            return;
        }
    }

    // 해당 상태에서 빠져 나갈 때 호출되는 메서드
    public void Exit()
    {
        _playerController = null;
    }
}
