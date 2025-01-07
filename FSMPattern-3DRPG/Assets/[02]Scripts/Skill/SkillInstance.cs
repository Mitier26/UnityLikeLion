using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkillCoolTimer))]
public class SkillInstance : MonoBehaviour
{
    public SkillData skillData;
    private SkillCoolTimer skillCoolTimer;

    private void Start()
    {
        skillCoolTimer = GetComponent<SkillCoolTimer>();
        skillCoolTimer.skillData = skillData;
    }

    public void FireSkill()
    {
        skillCoolTimer.StartCoolTimer();
    }

    public bool CanFireSkill()
    {
        return skillCoolTimer.IsReady;
    }

    public float CanSkillEnableDistance()
    {
        return skillData.skillEnableDistance;
    }

    public float RemainTime()
    {
        return skillData.GetSkillDuration;
    }
}
