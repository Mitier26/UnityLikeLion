using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState2 : ICubeState2
{
    public void Enter(CubeController2_1 controller)
    {
        Debug.Log("IdleState2.Enter");
    }

    public void UpdateState(CubeController2_1 controller)
    {
    }

    public void Exit(CubeController2_1 controller)
    {
    }
}
