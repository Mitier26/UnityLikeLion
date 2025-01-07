using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[State("ChaseState")]
public class ChaseState_Monster : CommonState_Monster
{
    public override void Enter()
    {
       Blackboard.animator.Play("Idles");
       Blackboard.animator.SetFloat("Speed", 1f);
    }

    public override void UpdateState(float deltaTime)
    {
        if (Blackboard.target == null)
        {
            Fsm.ChangeState(StateTypesClasses.StateTypes.IdleState);
            return;
        }

        var (skillDistance , skillIndex) = Blackboard.SkillController.GetNearSkillDistanceAndIndex();

        if (0 < skillIndex)
        {
            Fsm.ChangeState(StateTypesClasses.StateTypes.IdleState);
            return;
        }

        float attackRangeSqr = skillDistance * skillDistance;

        if (Vector3.SqrMagnitude(Blackboard.target.transform.position - Fsm.transform.position) > attackRangeSqr)
        {
            Vector3 newPos = Vector3.MoveTowards(Fsm.transform.position, Blackboard.target.transform.position, Blackboard.moveSpeed * deltaTime);
            Blackboard.rigidbody.MovePosition(newPos);
        }
        
    }

    public override void Exit()
    {
        
    }

}
