using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    private List<SkillInstance> skillInstances = new List<SkillInstance>();
    public List<string> skillDataNames = new List<string>();

    async void Start()
    {
        var result = await SkillFactory.CreateSkill(transform,new List<string>(skillDataNames));
        
    }

    public (float, int) GetNearSkillDistanceAndIndex()
    {
        float distance = float.MaxValue;
        int index = -1;

        for (var i = 0; i < skillInstances.Count; i++)
        {
            var skillInstance = skillInstances[i];
            if (skillInstance.CanFireSkill())
            {
                if (distance >= skillInstance.CanSkillEnableDistance())
                {
                    distance = skillInstance.CanSkillEnableDistance();
                    index = i;
                }
            }
        }
        return (distance, index);
    }

    public SkillData FireSkillByIndex(int skillIndex)
    {
        var skillInstance = skillInstances[skillIndex];
        skillInstance.FireSkill();
        return skillInstance.skillData;
    }
}
