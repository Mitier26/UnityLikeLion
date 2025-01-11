using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController2_1 : MonoBehaviour
{
    public ICubeState2 currentState2;
    
    // public ICubeState2 idleState2 = new IdleState2();
    // public ICubeState2 moveState2 = new MoveState2();
    // public ICubeState2 scaleState2 = new ScaleState2();
    
    // 이렇게 하면 주체를 변경할 수 없다.
    // 각 상태에서 누구꺼 인지 알아야함
    
    public ICubeState2 idleState2 = new IdleState2();
    public ICubeState2 moveState2 = new MoveState2();
    public ICubeState2 scaleState2 = new ScaleState2();

    private void Start()
    {
        currentState2 = idleState2;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeState2(idleState2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeState2(moveState2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeState2(scaleState2);
        }
        
        currentState2.UpdateState(this);
    }

    void ChangeState2(ICubeState2 newState)
    {
        currentState2.Exit(this);
        currentState2 = newState;
        currentState2.Enter(this);
    }
}
