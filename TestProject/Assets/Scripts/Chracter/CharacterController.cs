using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    StateMachine _stateMachine;

    public Rigidbody rb;
    public Animator animator;

    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    void Awake()
    {
        
    }

    void InitComponents()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void InitStates()
    {
        _stateMachine = new StateMachine();
        
        // 상태를 관리할 상태가 필요하다.
        // Character_IdleState를 만들어야 한다.
    }
}
