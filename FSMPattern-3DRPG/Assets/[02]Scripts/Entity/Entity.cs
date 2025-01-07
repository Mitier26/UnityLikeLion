using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[
    RequireComponent(typeof(Rigidbody)),
    RequireComponent(typeof(CapsuleCollider)),
    RequireComponent(typeof(StateMachine)),
    RequireComponent(typeof(Animator)),
    RequireComponent(typeof(CustomTag)),
    RequireComponent(typeof(SkillController)),
]
public abstract class Entity : MonoBehaviour
{
    protected StateMachine stateMachine;
    protected Rigidbody rigidbody;

    protected virtual StaterType EntityStaterType => StaterType.None;

    private void Awake()
    {
        stateMachine = GetComponent<StateMachine>();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    protected virtual void Start()
    {
        stateMachine.Run(EntityStaterType);
    }

    private void Update()
    {
        stateMachine.UpdateState();
    }
}
