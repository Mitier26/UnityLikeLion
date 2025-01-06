using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Blackboard_Player : MonoBehaviour, IBlackboardBase
{
    public float jumpForce = 3f;
    public float moveSpeed = 3f;
    
    public Animator animator;
    public Rigidbody rigidbody;
    
    public void InitBlackboard()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }
}
