using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    StateMachine _stateMachine;
    
    [NonSerialized] public Character_IdleState idleState;
    [NonSerialized] public Character_WalkState walkState;
    [NonSerialized] public Character_RunState runState;
    
    List<BaseAction> _actions;

    public Rigidbody rb;
    public Animator animator;
    
    public CharacterInputHandler inputHandler;

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

        idleState = new Character_IdleState(this);
        walkState = new Character_WalkState(this);
        runState = new Character_RunState(this);
    }

    public void SetInputHandler(BaseInputHandler inputHandler)
    {
        if (this.inputHandler != null)
        {
            foreach (var action in _actions)
            {
                action.UnRegistAction();
            }
        }
        
        this.inputHandler = inputHandler as CharacterInputHandler;

        if (inputHandler != null)
        {
            foreach (BaseAction action in _actions)
            {
                if (action.RegistAction())
                {
                    
                }
                else
                {
                    
                }
            }
        }
    }

    public void SetState(IState newState)
    {
        _stateMachine.ChangeState(newState);
    }
}
