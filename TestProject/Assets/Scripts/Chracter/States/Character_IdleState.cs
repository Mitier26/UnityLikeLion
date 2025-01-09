using UnityEngine;

public class Character_IdleState : BaseState<CharacterController>
{
    public Character_IdleState(CharacterController controller) : base(controller) { }

    public override void Enter()
    {
        
    }

    public override void UpdateState()
    {
        // Enter와 Exit는 상태가 변할 때 작동하는 것이다.
        // 상태를 변화하는 것은 UPdata State 에서 해야 Update에서 돌면서
        // 상태를 변화 하라는 것을 감지 할 수 있다.
        // 변화를 감지하는데....여기서 다른 상태로 변해야 하는데.
        // 변화하는 동작이 없다.
        // 처음에는 if(Input.GetKeyDown) 같은 것을 사용해 만들었을 것이다.
        // 하지만 각 State 마다 Input을 만들어야 할 것이다.
        // 코드가 많이 중접된다. 
        
        if (_controller.inputHandler.IsWalking)
        {
            // 상태가 변화 했는지 알 수 있는 bool 값을 확인데 작동한다.
            // 여기를 만들기 위해서는 inputHandler 가 필요하다.
            // 그리고 walkState도 필요하다.
            _controller.SetState(_controller.walkState);
        }
    }

    public override void Exit()
    {
        
    }
}
