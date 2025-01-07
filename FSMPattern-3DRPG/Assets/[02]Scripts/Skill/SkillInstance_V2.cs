using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICondition
{
    bool IsTrue();
}

public abstract class Condition
{
    protected Blackboard_Monster blackboardMonster;
}

public class DistanceChecker : Condition
{
    public float distance;
    public Transform checker;
    public bool IsTrue()
    {
        if (blackboardMonster.target)
        {
            return distance * distance >= Vector3.SqrMagnitude(blackboardMonster.target.transform.position - checker.position);
        }
        
        return false;
    }
}

[RequireComponent(typeof(SkillCoolTimer))]
public class SkillInstance_V2 : MonoBehaviour
{
    public GameObject Owner;
    public SkillData skillData;
    private SkillCoolTimer_V2 skillCoolTimer;
    

    private void Start()
    {
        skillCoolTimer = GetComponent<SkillCoolTimer_V2>();
        skillCoolTimer.skillData = skillData;
        blackboardMonster = GetComponent<Blackboard_Monster>();
        
    }

}
