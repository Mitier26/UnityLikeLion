using UnityEngine;

public class ScalingState : ICubeState
{
    public void EnterState(CubeController cube)
    {
        cube.SetColor(Color.red);
        cube.SetStateText("Scaling State");
    }

    public void UpdateState(CubeController cube)
    {
        cube.transform.localScale = Vector3.one * (1 + Mathf.PingPong(Time.time, 1));
    }

    public void ExitState(CubeController cube)
    {
        
    }
}