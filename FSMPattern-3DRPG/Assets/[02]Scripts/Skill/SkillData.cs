using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SkillData", menuName = "Game/Skill Data")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public string skillIcon;
    public string skillAnimation;
    [FormerlySerializedAs("skillDuration")] public float GetSkillDuration;
    
    public float skillCoolTime;
    public float skillEnableDistance;
    
}
