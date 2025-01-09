using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_WalkState : BaseState<CharacterController>
{
    // Start is called before the first frame update
    public Character_WalkState(CharacterController controller) : base(controller) { }
    
    public override void Enter()
    {
    }

    public override void UpdateState()
    {
        if (!_controller.inputHandler.IsWalking)
        {
            _controller.SetState(_controller.idleState);
        }
        else if (_controller.inputHandler.IsRunning)
        {
            _controller.SetState(_controller.runState);
        }
    }

    public override void Exit()
    {
    }
}
