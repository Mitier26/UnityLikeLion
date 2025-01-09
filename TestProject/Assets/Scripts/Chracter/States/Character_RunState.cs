using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_RunState : BaseState<CharacterController>
{
    public Character_RunState(CharacterController controller) : base(controller) { }
    
    public override void Enter()
    {
    }

    public override void UpdateState()
    {
        // 여기는 Run 상태에서 변하는 것이 필요하다.

        if (!_controller.inputHandler.IsRunning)
        {
            _controller.SetState(_controller.walkState);
        }
    }

    public override void Exit()
    {
    }
}
