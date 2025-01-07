using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[State("IdleState")]
public class IdleState_Monster : CommonState_Monster
{
    public void Enter()
    {
        Blackboard.animator.Play("Idles");
        Blackboard.animator.SetFloat("Speed", 0f);
    }

    public void UpdateState(float deltaTime)
    {
        if (Blackboard.target != null)
        {
            Fsm.ChangeState(StateTypesClasses.StateTypes.ChaseState);
        }
    }

    public void Exit()
    {
        
    }
}
