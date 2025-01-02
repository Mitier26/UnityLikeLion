using UnityEngine;
using UnityEngine.InputSystem;

public class WalkState : MonoBehaviour, IState
{   public StateMachine FSM { get; set; }
    public Blackboard_Default Blackboard { get; set; }

    public void InitState(IBlackboardBase blackboard)
    {
        Blackboard = blackboard as Blackboard_Default;
    }

    public void Enter()
    {
        Blackboard.animator.CrossFade("Idles", 0.1f);
        Blackboard.animator.SetFloat("Speed", 1.0f);
    }

    public void UpdateState(float deltaTime)
    {
        if (Blackboard.jumpInput.triggered && Blackboard.rigidbody.velocity.y == 0.0f)
        {
            FSM.ChangeState<JumpState>();
            return;
        }
        
        var value = Blackboard.moveInput.ReadValue<Vector2>();
        if (0 >= value.sqrMagnitude)
        {
            FSM.ChangeState<IdleState>();
            return;
        }
        
        Blackboard.rigidbody.velocity = new Vector3(value.x * Blackboard.moveSpeed, Blackboard.rigidbody.velocity.y, value.y * Blackboard.moveSpeed);
    }

    public void Exit()
    {
    }
}