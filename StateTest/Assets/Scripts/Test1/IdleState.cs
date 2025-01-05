using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState :ICubeState
{
    public void EnterState(CubeController cube)
    {
        cube.SetColor(Color.green);
        cube.SetStateText("Idle State");
    }

    public void UpdateState(CubeController cube)
    {
        
    }

    public void ExitState(CubeController cube)
    {
        
    }
}
