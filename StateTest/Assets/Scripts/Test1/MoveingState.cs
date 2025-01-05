using UnityEngine;

public class MoveingState : ICubeState
{
    private Vector3 startPosition;
    
    public void EnterState(CubeController cube)
    {
        startPosition = cube.transform.position;
        
        cube.SetColor(Color.blue);
        cube.SetStateText("Moving State");
    }

    public void UpdateState(CubeController cube)
    {
        // Mathf.PingPong을 사용하여 Cube가 왔다 갔다 움직이도록 구현
        float offset = Mathf.PingPong(Time.time * 2, 2) - 1; // -1에서 1로 이동
        cube.transform.position = startPosition + Vector3.right * offset;
    }

    public void ExitState(CubeController cube)
    {
        
    }
}