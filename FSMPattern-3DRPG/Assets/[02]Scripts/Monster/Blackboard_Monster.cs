using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Blackboard_Monster : MonoBehaviour ,IBlackboardBase
{
    public float moveSpeed = 3.0f;
    public float attackRange = 6.0f;
    
    [NonSerialized] public Animator animator;
    [NonSerialized] public Rigidbody rigidbody;

    public Entity target;
        
    public new void InitBlackboard()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }   

}
