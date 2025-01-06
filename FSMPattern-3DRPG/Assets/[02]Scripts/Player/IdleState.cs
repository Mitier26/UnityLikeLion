using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[State("IdleState")]
public class IdleState : MonoBehaviour, IState, IRecevieInput
{
    public StateMachine Fsm { get; set; }
    
    public Blackboard_Player Blackboard { get; set; }
    
    private bool jumpInputTriggered = false;
    private Vector2 moveInput = Vector2.zero;
    
    public void InitState(IBlackboardBase blackboard)
    {
        Blackboard = blackboard as Blackboard_Player;
    }

    public void Enter()
    {
        PlayerController.Instance.AddInputObserver(Fsm.gameObject, this);
        PlayerController.Instance.AddInputObserver(Fsm.gameObject,  this);
        
        Blackboard.animator.CrossFade("Idles", 0.1f);
        Blackboard.animator.SetFloat("Speed", 0.0f);
    }

    public void UpdateState(float deltaTime)
    {
        if (jumpInputTriggered && Blackboard.rigidbody.velocity.y == 0.0f)
        {
            Fsm.ChangeState<JumpState>();
            return;
        }
        
        if (moveInput.sqrMagnitude > 0)
        {
            Fsm.ChangeState<WalkState>();
        }
    }

    public void Exit()
    {
        PlayerController.Instance.AddInputObserver(Fsm.gameObject,  null);
        PlayerController.Instance.AddInputObserver(Fsm.gameObject, null);
        
        jumpInputTriggered = false;
        moveInput = Vector2.zero;
    }

    public void OnTriggered(string action, bool triggerValue)
    {
        if (action == "Jump")
            jumpInputTriggered = triggerValue;
    }

    public void OnReadValue(string action, Vector2 value)
    {
        if (action == "Move")
            moveInput = value;
    }
}