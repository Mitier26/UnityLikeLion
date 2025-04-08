using UnityEngine;

public class PlayerStateIdle : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;
    
    // 해당 상태로 집입했을 때 호출되는 메서드
    public void Enter(PlayerController playerController)
    {
        _playerController = playerController;
        _playerController.Animator.SetBool("Idle", true);
    }

    // 해당 상태에 머물려 있을 때 Update 주기로 호출되는 메서드
    public void Update()
    {
        var inputVertical = Input.GetAxis("Vertical");
        var inputHorizontal = Input.GetAxis("Horizontal");
        
        // 이동
        if (inputVertical != 0 || inputHorizontal != 0)
        {
            _playerController.Rotate(inputHorizontal, inputVertical);
            _playerController.SetState(PlayerState.Move);
            return;
        }
        
        // 점프
        if (Input.GetButtonDown("Jump"))
        {
            _playerController.SetState(PlayerState.Jump);
            return;
        }
        
        // 공격
        if (Input.GetButtonDown("Fire1"))
        {
            _playerController.SetState(PlayerState.Attack);
            return;
        }
    }

    // 해당 상태에서 빠져 나갈 때 호출되는 메서드
    public void Exit()
    {
        _playerController.Animator.SetBool("Idle", false);
        _playerController = null;
    }
}
