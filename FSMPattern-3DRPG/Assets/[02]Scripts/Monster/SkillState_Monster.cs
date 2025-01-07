using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[State("SkillState")]
public class SkillState_Monster : CommonState_Monster
{
    public void Enter()
    {
        OnFireSkill();
    }

    public void UpdateState(float deltaTime)
    {
        
    }

    public void Exit()
    {
        Debug.Log("Exit");
    }

    async void OnFireSkill()
    {
        var (distance, skillIndex) = Blackboard.SkillController.GetNearSkillDistanceAndIndex();

        var skillData = Blackboard.SkillController.FireSkillByIndex(skillIndex);
        
        Blackboard.animator.Play(skillData.skillAnimation);
        
        await UniTask.Delay((int)(skillData.GetSkillDuration * 1000));
        Fsm.ChangeState(StateTypesClasses.StateTypes.IdleState);
    }
}
