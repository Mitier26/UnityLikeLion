using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState2 : ICubeState2
{
    public void Enter(CubeController2_1 controller)
    {
        Debug.Log("MoveState2.Enter");
    }

    public void UpdateState(CubeController2_1 controller)
    {
        // 여기서 본체를 움직여야함.
    }

    public void Exit(CubeController2_1 controller)
    {
    }
}
