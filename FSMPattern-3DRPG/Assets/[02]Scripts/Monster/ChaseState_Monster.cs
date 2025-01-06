using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[State("ChaseState")]
public class ChaseState_Monster : CommonState_Monster
{
    public override void Enter()
    {
        Debug.Log("ChaseState_Monster");
    }

    public override void UpdateState(float deltaTime)
    {
        if (Blackboard.target == null)
        {
            Fsm.ChangeState(StateTypesClasses.StateTypes.IdleState);
            return;
        }
        
        // 숙제
        // 타겟이 있다면 어택레인지까지 쫓아가서 스킬스테이트로 바꾸고 스킬을 쓰고 아이들스테이트로 돌아가기
        
        Vector3 direction = Blackboard.target.transform.position - transform.position;
        Debug.Log(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Blackboard.target.transform.rotation, 360.0f * deltaTime);
    }

    public override void Exit()
    {
        
    }

}
