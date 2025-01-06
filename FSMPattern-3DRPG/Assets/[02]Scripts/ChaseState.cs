using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[State("Chase")]
public class ChaseState : MonoBehaviour, IState
{
    public StateMachine Fsm { get; set; }
    public Blackboard_Player Blackboard { get; set; }

    public void InitState(IBlackboardBase blackboard)
    {
    }

    public void Enter()
    {
    }

    public void UpdateState(float deltaTime)
    {
        // 추적 및 공격 범위 체크 로직
        if (IsInAttackRange())
        {
            // Fsm.ChangeState();
        }
        else if (IsTargetLost())
        {
            Fsm.ChangeState(StateTypesClasses.StateTypes.IdleState);
        }
    }

    public void Exit()
    {
        
    }
    
    private bool IsInAttackRange()
    {
        // 공격 범위 체크 로직 구현
        return false;
    }

    private bool checkCoolTime()
    {
        return false;
    }
    
    private bool IsTargetLost()
    {
        // 타겟 감지 거리 이탈 체크 로직 구현
        return false;
    }
}