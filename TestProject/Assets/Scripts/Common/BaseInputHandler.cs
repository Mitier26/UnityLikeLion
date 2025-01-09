using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 입력에 관한 것은 이것을 상속 받은 것에서 한다.
// 이것은 기본적인 것 반환할 값, 바닥 체크만 하고 있다.
// 그러면 키를 입력 받기 위해서는 BaseInputHandler를 상속 받는 것이 있어야한다.

public class BaseInputHandler : MonoBehaviour
{
    public Vector2 MovementInput { get; protected set; }
    
    public bool IsJumping { get; protected set; }
    public bool IsRunning { get; protected set; }
    public float Horizontal { get; protected set; }
    public float Vertical { get; protected set; }
    public bool IsWalking { get; protected set; }
    public bool IsGrounded { get; protected set; }

    [SerializeField] public float groundCheckDistance = 0.1f;
    [SerializeField] public LayerMask groundLayer;

    [NonSerialized] public Action OnAction;

    protected virtual void Update()
    {
        IsGrounded = CheckIfGrounded();
        OnAction?.Invoke();
    }

    private bool CheckIfGrounded()
    {
        bool ret = false;

        if (Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer))
        {
            ret = true;
        }
        if(Physics.Raycast(transform.position+ new Vector3(0.5f, 0, 0), Vector3.down, groundCheckDistance, groundLayer))
        {
            ret = true;
        }

        if(Physics.Raycast(transform.position+ new Vector3(-0.5f, 0, 0), Vector3.down, groundCheckDistance, groundLayer))
        {
            ret = true;
        }

        return ret;
    }
}
