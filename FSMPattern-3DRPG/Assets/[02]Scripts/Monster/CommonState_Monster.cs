using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CommonState_Monster : MonoBehaviour, IState
{
    public StateMachine Fsm { get; set; }
    public Blackboard_Monster Blackboard { get; set; }
    
    public void InitState(IBlackboardBase blackboard)
    {
        Blackboard = blackboard as Blackboard_Monster;
    }

    public virtual void Enter()
    {
        
    }

    public virtual void UpdateState(float deltaTime)
    {
        
    }

    public virtual void Exit()
    {
        
    }
}
