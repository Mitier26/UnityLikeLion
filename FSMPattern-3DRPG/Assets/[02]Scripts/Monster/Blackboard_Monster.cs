using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class Blackboard_Monster : MonoBehaviour ,IBlackboardBase
{
    public float moveSpeed = 3.0f;
    public float attackRange = 2.0f;
    
    [NonSerialized] public Animator animator;
    [NonSerialized] public Rigidbody rigidbody;
    [NonSerialized] public SkillController SkillController;

    [NonSerialized] public  Entity target;
        
    public new void InitBlackboard()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        SkillController = GetComponent<SkillController>();
    }   

}
