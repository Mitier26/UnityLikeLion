using UnityEngine;

public class PlayerStateJump : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;

    // 해당 상태로 집입했을 때 호출되는 메서드
    public void Enter(PlayerController playerController)
    {
        _playerController = playerController;
        _playerController.Animator.SetBool("Jump", true);
        _playerController.Jump();
    }

    // 해당 상태에 머물려 있을 때 Update 주기로 호출되는 메서드
    public void Update()
    {
        var distanceToGround = _playerController.GetDistanceToGround();

        if (distanceToGround < 0.1f)
        {
            _playerController.SetState(PlayerState.Idle);
        }
        else
        {
            _playerController.Animator.SetFloat("GroundDistance", distanceToGround);
        }
    }

    // 해당 상태에서 빠져 나갈 때 호출되는 메서드
    public void Exit()
    {
        _playerController.Animator.SetBool("Jump", false);
        _playerController = null;
    }
}
